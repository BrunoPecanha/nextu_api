using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Repository;
using UFF.Domain.Services;
using UFF.Domain.Commands;
using UFF.Domain.Commands.User;
using UFF.Domain.Dto;
using UFF.Domain.Entity;
using UFF.Service.Properties;
using System.Reflection;
using System.IO;

namespace UFF.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _costumerRepository;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper, IAuthService authService)
        {
            _costumerRepository = repository;
            _mapper = mapper;
            _authService = authService; 
        }

        public async Task<CommandResult> GetAllAsync()
        {
            var users = await _costumerRepository.GetAllAsync();

            if (users is null || users.Count() == 0)
                return new CommandResult(false, users);

            return new CommandResult(true, _mapper.Map<List<UserDto>>(users));
        }

        public async Task<CommandResult> GetByIdAsync(int id)
        {
            var user = await _costumerRepository.GetByIdAsync(id);

            if (user is null)
                return new CommandResult(false, user);            

            return new CommandResult(true, _mapper.Map<UserDto>(user));
        }      

        public async Task<CommandResult> CreateAsync(UserCreateCommand command)
        {
            try
            {             
                var costumer = new User(command);
                var hashedPassword = _authService.HashPassword(costumer, command.Password);
                costumer.UpdatePassWord(hashedPassword);

                if (!costumer.IsValid())
                    return new CommandResult(false, Resources.MissingInfo);

                await _costumerRepository.AddAsync(costumer);
                await _costumerRepository.SaveChangesAsync();

                return new CommandResult(true, costumer);
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex.Message);
            }
        }

        public async Task<CommandResult> UpdateAsync(UserEditCommand command)
        {
            try
            {
                //if (command.DeleteAccount)
                //{                    
                //}

                //if (command.ProfileImage != null)
                //{
                //    using var memoryStream = new MemoryStream();
                //    await command.ProfileImage.CopyToAsync(memoryStream);
                //    var imageBytes = memoryStream.ToArray();                    
                //}

                var costumer = await _costumerRepository.GetByIdAsync(command.Id);

                if (costumer is null)
                    return new CommandResult(false, Resources.NotFound);

                costumer.UpdateAllUserInfo(command);

                var hashedPassword = _authService.HashPassword(costumer, command.Password);
                costumer.UpdatePassWord(hashedPassword);

                _costumerRepository.Update(costumer);
                await _costumerRepository.SaveChangesAsync();

                return new CommandResult(true, _mapper.Map<UserDto>(costumer));
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex.Message);
            }
        }

        public async Task<CommandResult> DeleteAsync(int id)
        {
            try
            {
                var costumer = await _costumerRepository.GetByIdAsync(id);
                if (costumer is not null)
                {
                    costumer.Disable();
                    _costumerRepository.Update(costumer);
                    await _costumerRepository.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex.Message);
            }

            return new CommandResult(true, null);
        }
    }
}