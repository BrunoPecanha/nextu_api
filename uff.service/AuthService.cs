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

namespace UFF.Service
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IMapper _mapper;

        public AuthService(IConfiguration configuration, IMapper mapper, IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
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
            
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );

            return new CommandResult(true, new { Token = new JwtSecurityTokenHandler().WriteToken(token), User = _mapper.Map<UserDto>(user) });
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