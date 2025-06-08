using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Store;
using UFF.Domain.Dto;
using UFF.Domain.Entity;
using UFF.Domain.Enum;
using UFF.Domain.Repository;
using UFF.Domain.Services;
using UFF.Service.Properties;

namespace UFF.Service
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IEmployeeStoreRepository _employeeStoreRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public StoreService(IStoreRepository repository, IMapper mapper, IUserRepository userRepository, ICategoryRepository categoryRepository,
            IEmployeeStoreRepository employeeStoreRepository, IUnitOfWork unitOfWork, IFileService fileService)
        {
            _storeRepository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _employeeStoreRepository = employeeStoreRepository;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task<CommandResult> GetAllAsync()
        {
            var stores = await _storeRepository.GetAllAsync();

            if (stores is null || !stores.Any())
                return new CommandResult(false, stores);

            return new CommandResult(true, _mapper.Map<List<StoreDto>>(stores));
        }
        public async Task<CommandResult> GetByIdAsync(int id)
        {
            var store = await _storeRepository.GetByIdAsync(id);

            if (store is null)
                return new CommandResult(false, store);

            return new CommandResult(true, _mapper.Map<StoreDto>(store));
        }
        public async Task<CommandResult> GetByCategoryIdAsync(int id)
        {
            var store = await _storeRepository.GetByCategoryId(id);

            if (store is null)
                return new CommandResult(false, store);

            return new CommandResult(true, _mapper.Map<StoreDto[]>(store));
        }
        public async Task<CommandResult> GetByOwnerIdAsync(int id)
        {
            var stores = await _storeRepository.GetByOwnerIdAsync(id);

            if (stores is null)
                return new CommandResult(false, stores);

            return new CommandResult(true, _mapper.Map<StoreDto[]>(stores));
        }
        public async Task<CommandResult> GetByEmployeeId(int id, ProfileEnum profile)
        {
            var stores = await _storeRepository.GetByEmployeeId(id, profile);

            if (stores is null)
                return new CommandResult(false, stores);

            return new CommandResult(true, _mapper.Map<StoreDto[]>(stores));
        }
        public async Task<CommandResult> GetStoreWithProfessionalsAndWaitInfoAsync(int storeId)
        {
            var store = await _storeRepository.GetStoreWithEmployeesAndQueuesAsync(storeId);

            if (store == null)
                return new CommandResult(false, "Loja não encontrada");

            var dto = new StoreProfessionalsDto(store.Id, store.Name, store.LogoPath, store.StoreSubtitle, store.Verified);

            if (store.Queues == null || store.Queues.Count == 0)
                return new CommandResult(false, dto);

            foreach (var employeeStore in store.EmployeeStore)
            {
                var employee = employeeStore.Employee;
                if (employee == null)
                    continue;

                var employeeQueue = employee.Queues
                    .FirstOrDefault(q => q.Status == QueueStatusEnum.Open || q.Status == QueueStatusEnum.Paused);

                if (employeeQueue == null)
                    continue;

                int waitingCustomers = employeeQueue.Customers?.Count(c => c.Status == CustomerStatusEnum.Waiting) ?? 0;

                TimeSpan averageWaitingTime = TimeSpan.Zero;
                TimeSpan averageServiceTime = TimeSpan.Zero;

                if (waitingCustomers > 0)
                {
                    (averageWaitingTime, averageServiceTime) = await CalculateAverageWaitingTime(employee.Id);
                }

                dto.Professionals.Add(new ProfessionalDto
                {
                    QueueId = employeeQueue.Id,
                    Name = employee.Name,
                    Liked = true,
                    QueueName = employeeQueue.Name,
                    Status = employeeQueue.Status,
                    PauseReason = employeeQueue.Status == QueueStatusEnum.Paused ? employeeQueue.PauseReason : string.Empty,
                    Subtitle = employee.Subtitle,
                    CustomersWaiting = waitingCustomers,
                    AverageWaitingTime = averageWaitingTime,
                    AverageServiceTime = averageServiceTime,
                    ServicesProvided = employee.ServicesProvided
                });
            }

            return new CommandResult(true, dto);
        }
        public async Task<CommandResult> GetProfessionalsOfStoreAsync(int storeId)
        {
            var professionals = await _storeRepository.GetProfessionalsOfStoreAsync(storeId);

            if (professionals == null || !professionals.Any())
                return new CommandResult(false, "Não foram encontrados funcionários para essa loja");

            var dto = new List<ProfessionalDto>();

            foreach (var professional in professionals)
            {

                dto.Add(new ProfessionalDto
                {
                    Id = professional.Id,
                    Name = professional.Name
                });
            }

            return new CommandResult(true, dto);
        }
        public async Task<CommandResult> GetAllStoresUserIsInByUserId(int userId)
        {
            var stores = await _storeRepository.GetAllStoresUserIsInByUserId(userId);

            if (stores == null || !stores.Any())
                return new CommandResult(false, stores);


            return new CommandResult(true, _mapper.Map<StoreDto[]>(stores));
        }
        private async Task<(TimeSpan, TimeSpan)> CalculateAverageWaitingTime(int professionalId)
        {
            var queue = await _storeRepository.CalculateAverageWaitingTime(professionalId);

            if (queue?.Customers == null || !queue.Customers.Any())
                return (TimeSpan.Zero, TimeSpan.Zero);

            double totalWaitTime = queue.Customers
                .Sum(qc => qc.CustomerServices.Sum(s => s.Duration.TotalMinutes));

            double averageWaitTime = totalWaitTime / queue.Customers.Count;

            return (TimeSpan.FromMinutes(totalWaitTime), TimeSpan.FromMinutes(averageWaitTime));
        }
        public async Task<CommandResult> CreateAsync(StoreCreateCommand command)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var owner = await _userRepository.GetByIdAsync(command.OwnerId);
                if (owner is null)
                    return new CommandResult(false, Resources.OwnerNotFound);

                var category = await _categoryRepository.GetByIdAsync(command.CategoryId);
                if (category is null)
                    return new CommandResult(false, Resources.NotFound);

                var store = new Store(command);
                if (!store.IsValid())
                    return new CommandResult(false, Resources.MissingInfo);

                owner.ChageUserToOwner();
                store.SetOwner(owner);
                store.SetCategory(category);

                await _storeRepository.AddAsync(store);
                await _unitOfWork.SaveChangesAsync();

                var employeeXStore = new EmployeeStore(owner.Id, store.Id);
                employeeXStore.ActivateRelation();

                _userRepository.Update(owner);
                await _employeeStoreRepository.AddAsync(employeeXStore);

                await _unitOfWork.CommitAsync();

                return new CommandResult(true, store);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new CommandResult(false, ex, ex.Message);
            }
        }
        public async Task<CommandResult> UpdateAsync(StoreEditCommand command, int storeId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var store = await _storeRepository.GetByIdAsync(storeId);

                if (store is null)
                    return new CommandResult(false, Resources.NotFound);

                var category = await _categoryRepository.GetByIdAsync(command.CategoryId);

                if (category is null)
                    return new CommandResult(false, Resources.NotFound);

                if (command.Logo != null && command.Logo.Length > 0)
                {
                    var logoBytes = await _fileService.GetFileBytesAsync(command.Logo);
                    var newLogoHash = _fileService.CalculateHash(logoBytes);

                    if (store.LogoHash != newLogoHash)
                    {
                        var relativePath = await _fileService.SaveFileAsync(command.Logo, FileEnum.Logo);
                        store.UpdateLogo(relativePath, newLogoHash);
                    }
                }

                if (command.WallPaper != null && command.WallPaper.Length > 0)
                {
                    var wallPaperBytes = await _fileService.GetFileBytesAsync(command.WallPaper);
                    var newWallPaperHash = _fileService.CalculateHash(wallPaperBytes);

                    if (store.WallPaperHash != newWallPaperHash)
                    {
                        var relativePath = await _fileService.SaveFileAsync(command.WallPaper, FileEnum.WallPaper);
                        store.UpdateWallPaper(relativePath, newWallPaperHash);
                    }
                }

                store.UpdateStoreInfo(command);

                _storeRepository.Update(store);

                await _unitOfWork.CommitAsync();

                return new CommandResult(true, _mapper.Map<StoreDto>(store));
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
                var store = await _storeRepository.GetByIdAsync(id);
                if (store is not null)
                {
                    store.Disable();
                    _storeRepository.Update(store);
                    await _storeRepository.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex.Message);
            }

            return new CommandResult(true, null);
        }
        public async Task<CommandResult> GetFilteredStoresAsync(int? categoryId, string quickFilter, int? userId)
        {
            var stores = await _storeRepository.GetFilteredStoresAsync(categoryId, quickFilter, userId);

            var today = DateTime.UtcNow.Date;

            var result = stores
                .Select(s => new StoreDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Category = s.Category.Name,
                    LogoPath = s.LogoPath,
                    CreatedAt = s.RegisteringDate,

                    Votes = s.Ratings?.Count ?? 0,                  
                    Rating = s.Ratings != null && s.Ratings.Any()
                        ? s.Ratings.Average(r => r.Score) 
                        : 0,
                    IsFavorite = userId.HasValue && s.Favorites.Any(x => x.UserId == userId),
                    IsVerified = s.Verified,

                    MinorQueue = s.EmployeeStore
                        .Select(es => es.Employee.Queues
                            .FirstOrDefault(q => q.Date.Date == today)?.Customers.Count ?? 0)
                        .DefaultIfEmpty(0)
                        .Min(),

                    Liked = userId.HasValue && s.Favorites.Any(f => f.UserId == userId)
                })
                .ToArray();

            return new CommandResult(true, result);
        }

    }
}