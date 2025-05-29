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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CommandResult> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            if (categories is null || !categories.Any())
                return new CommandResult(false, categories);

            return new CommandResult(true, _mapper.Map<List<CategoryDto>>(categories));
        }

        public async Task<CommandResult> GetByIdAsync(int id)
        {
            var store = await _categoryRepository.GetByIdAsync(id);

            if (store is null)
                return new CommandResult(false, store);            

            return new CommandResult(true, _mapper.Map<CategoryDto>(store));
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
                var store = await _categoryRepository.GetByIdAsync(1);

                if (store is null)
                    return new CommandResult(false, Resources.NotFound);

                //                store.UpdateAllUserInfo(command);

                _categoryRepository.Update(store);
                await _categoryRepository.SaveChangesAsync();

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
                var store = await _categoryRepository.GetByIdAsync(id);
                if (store is not null)
                {
                    //store.Disable();
                    _categoryRepository.Update(store);
                    await _categoryRepository.SaveChangesAsync();
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