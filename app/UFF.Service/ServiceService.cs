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
using UFF.Domain.Enum;
using UFF.Domain.Entity;

namespace UFF.Service
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly IServiceCategoryRepository _category;
        private readonly IStoreRepository _store;
        private readonly IFileService _fileService;

        public ServiceService(IServiceRepository serviceRepository, IMapper mapper, IServiceCategoryRepository category, IStoreRepository store, IFileService fileService)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _category = category;
            _store = store;
            _fileService = fileService;
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
                string imagePath = string.Empty;

                if (command.ImageFile != null)
                    imagePath = await _fileService.SaveFileAsync(command.ImageFile, FileEnum.Service);

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
                string imagePath = string.Empty;

                var service = await _serviceRepository.GetByIdAsync(command.Id);

                if (service is null)
                    return new CommandResult(false, Resources.NotFound);

                service.UpdateServiceDetails(command);

                if (command.ImageFile == null)
                {
                    service.UpdateImage(null, null);
                }
                else
                {
                    var logoBytes = await _fileService.GetFileBytesAsync(command.ImageFile);
                    var newImgHash = _fileService.CalculateHash(logoBytes);

                    if (service.ImageHash != newImgHash)
                    {
                        var relativePath = await _fileService.SaveFileAsync(command.ImageFile, FileEnum.Service);
                        service.UpdateImage(relativePath, newImgHash);
                    }
                }

                var category = await _category.GetByIdAsync(command.CategoryId);

                if (category is null)
                    return new CommandResult(false, "Categoria não encontrada");

                var store = await _store.GetByIdAsync(command.StoreId);

                if (!store.IsValid())
                    return new CommandResult(false, Resources.MissingInfo);

                _serviceRepository.Update(service);
                await _serviceRepository.SaveChangesAsync();

                return new CommandResult(true, _mapper.Map<ServiceDto>(service));
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
                    store.Disable();
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