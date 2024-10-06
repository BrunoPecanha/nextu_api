using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uff.domain.Commands.User;
using uff.Domain;
using uff.Domain.Commands;
using uff.Domain.Dto;
using uff.Domain.Entity;
using uff.service.Properties;

namespace uff.Service
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
            var costumers = await _costumerRepository.GetAllAsync();

            if (costumers is null || costumers.Count() == 0)
                return new CommandResult(false, costumers);

            return new CommandResult(true, _mapper.Map<List<UserDto>>(costumers));
        }

        public async Task<CommandResult> GetByIdAsync(int id)
        {
            var costumer = await _costumerRepository.GetByIdAsync(id);

            if (costumer is null)
                return new CommandResult(false, costumer);

            return new CommandResult(true, _mapper.Map<UserDto>(costumer));
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
                var costumer = await _costumerRepository.GetByIdAsync(command.Id);

                if (costumer is null)
                    return new CommandResult(false, Resources.NotFound);

                costumer.UpdateAllInfo(command);

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
                    _costumerRepository.Remove(costumer);
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