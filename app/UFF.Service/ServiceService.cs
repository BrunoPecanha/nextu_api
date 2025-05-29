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
using UFF.Domain.Commands.Service;

namespace UFF.Service
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly IServiceCategoryRepository _category;
        private readonly IStoreRepository _store;
        private readonly ImageService _imageService;

        public ServiceService(IServiceRepository serviceRepository, IMapper mapper, IServiceCategoryRepository category, IStoreRepository store, ImageService imageService)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _category = category;
            _store = store;
            _imageService = imageService;
        }

        public async Task<CommandResult> GetAllAsync(int storeId, bool onlyActivated = true)
        {
            var services = await _serviceRepository.GetAllAsync(storeId, onlyActivated);

            if (services is null || !services.Any())
                return new CommandResult(false, services);

            return new CommandResult(true, _mapper.Map<List<ServiceDto>>(services));
        }

        public async Task<CommandResult> GetAllCategoriesAsync()
        {
            var servicesCategories = await _serviceRepository.GetAllCategoriesAsync();

            if (servicesCategories is null || !servicesCategories.Any())
                return new CommandResult(false, servicesCategories);

            return new CommandResult(true, _mapper.Map<List<ServiceCategoryDto>>(servicesCategories));
        }

        public async Task<CommandResult> GetByIdAsync(int id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);

            if (service is null)
                return new CommandResult(false, service);

            return new CommandResult(true, _mapper.Map<ServiceDto>(service));
        }

        public async Task<CommandResult> CreateAsync(ServiceCreateCommand command)
        {
            try
            {
                string saveDirectory = "C:\\Estudos\\app\\www\\assets\\images\\services";
        
                string imagePath = await _imageService.SaveImageAndGetPath(command.ImageFile, saveDirectory);

                var category = await _category.GetByIdAsync(command.CategoryId);

                if (category is null)
                    return new CommandResult(false, "Categoria não encontrada");

                var store = await _store.GetByIdAsync(command.StoreId);

                if (!store.IsValid())
                    return new CommandResult(false, Resources.MissingInfo);

                var service = new Domain.Entity.Service(command, category.Id, store.Id, imagePath);

                await _serviceRepository.AddAsync(service);
                await _serviceRepository.SaveChangesAsync();

                return new CommandResult(true, null);
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex.Message);
            }
        }

        public async Task<CommandResult> UpdateAsync(ServiceEditCommand command)
        {
            try
            {
                string _imagePath = await SaveImage(command);

                var service = await _serviceRepository.GetByIdAsync(command.Id);

                if (service is null)
                    return new CommandResult(false, Resources.NotFound);

                service.UpdateServiceDetails(command, _imagePath);

                _serviceRepository.Update(service);
                await _serviceRepository.SaveChangesAsync();

                return new CommandResult(true, _mapper.Map<ServiceDto>(service));
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex.Message);
            }
        }

        private async Task<string> SaveImage(ServiceEditCommand command)
        {
            if (command.ImageFile != null)
            {
                string saveDirectory = "C:\\Estudos\\app\\www\\assets\\images\\services";

                string imagePath = await _imageService.SaveImageAndGetPath(command.ImageFile, saveDirectory);

               return _imageService.GetImageUrl(imagePath);
            }

            return string.Empty;
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