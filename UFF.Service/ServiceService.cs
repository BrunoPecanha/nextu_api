using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Repository;
using UFF.Domain.Services;
using UFF.Domain.Commands;
using UFF.Domain.Dto;
using UFF.Service.Properties;

namespace UFF.Service
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public ServiceService(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<CommandResult> GetAllAsync(int storeId)
        {
            var services = await _serviceRepository.GetAllAsync(storeId);

            if (services is null || !services.Any())
                return new CommandResult(false, services);

            return new CommandResult(true, _mapper.Map<List<ServiceDto>>(services));
        }

        public async Task<CommandResult> GetByIdAsync(int id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);

            if (service is null)
                return new CommandResult(false, service);            

            return new CommandResult(true, _mapper.Map<ServiceDto>(service));
        }

        public async Task<CommandResult> CreateAsync(object command)
        {
            try
            {             
                //var store = new Store(command);

                //if (!store.IsValid())
                //    return new CommandResult(false, Resources.MissingInfo);

                //var owner = await _userRepository.GetByIdAsync(command.OwnerId);

                //if (owner is null)
                //    return new CommandResult(false, Resources.OwnerNotFound);

                //store.SetOwner(owner);

                //await _storeRepository.AddAsync(store);
                //await _storeRepository.SaveChangesAsync();

                return new CommandResult(true, null);
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex.Message);
            }
        }

        public async Task<CommandResult> UpdateAsync(object command)
        {
            try
            {
                var store = await _serviceRepository.GetByIdAsync(1);

                if (store is null)
                    return new CommandResult(false, Resources.NotFound);

                //                store.UpdateAllUserInfo(command);

                _serviceRepository.Update(store);
                await _serviceRepository.SaveChangesAsync();

                return new CommandResult(true, _mapper.Map<UserDto>(store));
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
                var store = await _serviceRepository.GetByIdAsync(id);
                if (store is not null)
                {
                    //store.Disable();
                    _serviceRepository.Update(store);
                    await _serviceRepository.SaveChangesAsync();
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