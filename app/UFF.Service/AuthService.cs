using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Dto;
using UFF.Domain.Entity;
using UFF.Domain.Repository;
using UFF.Domain.Services;

namespace UFF.Service
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IMapper _mapper;

        public AuthService(IConfiguration configuration, IMapper mapper, IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IRefreshTokenRepository refreshTokenRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<CommandResult> AuthSync(string userName, string password)
        {
            var user = await _userRepository.GetUserByLogin(userName);

            if (user is null || user.Status == Domain.Enum.StatusEnum.Disabled)
                return new CommandResult(false, "Usuário não encontrado");

            var logged = VerifyPassword(user, password);

            if (!logged)
                return new CommandResult(false, "Senha incorreta");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var accessToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );

            var accessTokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);

            var existingRefreshToken = await _refreshTokenRepository
                .GetByUserIdAsync(user.Id);

            RefreshToken refreshToken;

            if (existingRefreshToken != null && existingRefreshToken.ExpiryDate > DateTime.UtcNow && !existingRefreshToken.IsRevoked)
            {
                refreshToken = existingRefreshToken;
            }
            else
            {
                refreshToken = new RefreshToken(user);
                await _refreshTokenRepository.AddAsync(refreshToken);
            }

            await _refreshTokenRepository.SaveChangesAsync();

            return new CommandResult(true, new
            {
                Token = accessTokenString,
                RefreshToken = refreshToken.Token,
                User = _mapper.Map<UserDto>(user)
            });
        }

        public async Task<CommandResult> RefreshTokenAsync(string refreshToken)
        {
            var storedToken = await _refreshTokenRepository.GetRefreshToken(refreshToken);

            if (storedToken == null)
                return new CommandResult(false, "Refresh token inválido.");

            if (storedToken.IsRevoked)
                return new CommandResult(false, "Refresh token revogado.");

            if (storedToken.ExpiryDate <= DateTime.UtcNow)
                return new CommandResult(false, "Refresh token expirado.");

            var user = await _userRepository.GetByIdAsync(storedToken.UserId);

            if (user is null || user.Status == Domain.Enum.StatusEnum.Disabled)
                return new CommandResult(false, "Usuário não encontrado ou desativado.");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var accessToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );

            var accessTokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);

            var newRefreshToken = new RefreshToken(user);

            await _refreshTokenRepository.RevokeRefreshToken(storedToken);
            await _refreshTokenRepository.AddAsync(newRefreshToken);
            await _refreshTokenRepository.SaveChangesAsync();

            return new CommandResult(true, new
            {
                Token = accessTokenString,
                RefreshToken = newRefreshToken.Token,
                User = _mapper.Map<UserDto>(user)
            });
        }

        public async Task<CommandResult> RevokeRefreshTokenAsync(string refreshToken)
        {
            var storedToken = await _refreshTokenRepository.GetRefreshToken(refreshToken);

            if (storedToken == null)
                return new CommandResult(false, "Refresh token não encontrado.");

            if (storedToken.IsRevoked)
                return new CommandResult(false, "Refresh token já revogado.");

            storedToken.RevokeToken();
            await _refreshTokenRepository.UpdateRefreshToken(storedToken);
            await _refreshTokenRepository.SaveChangesAsync();

            return new CommandResult(true, "Refresh token revogado com sucesso.");
        }

        public string HashPassword(User user, string password)
           => _passwordHasher.HashPassword(user, password);

        public bool VerifyPassword(User user, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}