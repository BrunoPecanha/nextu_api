using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Repository;
using UFF.Domain.Services;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Store;
using UFF.Domain.Dto;
using UFF.Domain.Entity;
using UFF.Service.Properties;

namespace UFF.Service
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public StoreService(IStoreRepository repository, IMapper mapper, IUserRepository userRepository, ICategoryRepository categoryRepository)
        {
            _storeRepository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
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
        public async Task<CommandResult> GetByEmployeeId(int id)
        {
            var stores = await _storeRepository.GetByEmployeeId(id);

            if (stores is null)
                return new CommandResult(false, stores);

            return new CommandResult(true, _mapper.Map<StoreDto[]>(stores));
        }
        public async Task<CommandResult> GetStoreWithProfessionalsAndWaitInfoAsync(int storeId)
        {
            var store = await _storeRepository.GetStoreWithEmployeesAndQueuesAsync(storeId);

            if (store == null)
                return new CommandResult(false, "Loja não encontrada");

            var dto = new StoreProfessionalsDto(store.Name, store.LogoPath, store.StoreSubtitle);

            if (store.Queues.Count <= 0)
                return new CommandResult(false, dto);

            foreach (var employeeStore in store.EmployeeStore)
            {
                var employeeTodayQueues = store.Queues
                    .FirstOrDefault(q => q.EmployeeId == employeeStore.EmployeeId);

                int waitingCustomers = employeeTodayQueues.QueueCustomers
                    .Count(qc => qc.Customer.Status == Domain.Enum.CustomerStatusEnum.Waiting);

                if (waitingCustomers > 0)
                {                    
                    var (averageWaitingTime, averageServiceTime) = await CalculateAverageWaitingTime(employeeStore.Employee.Id);

                    dto.Professionals.Add(new ProfessionalDto
                    {
                        QueueId = employeeTodayQueues.Id,
                        Name = employeeStore.Employee.Name,
                        Liked = true,
                        Subtitle = employeeStore.Employee.Subtitle,
                        CustomersWaiting = waitingCustomers,
                        AverageWaitingTime = averageWaitingTime,  
                        AverageServiceTime = averageServiceTime,
                        ServicesProvided = employeeStore.Employee.ServicesProvided
                    });
                }
            }

            return new CommandResult(true, dto);
        }
        private async Task<(TimeSpan, TimeSpan)> CalculateAverageWaitingTime(int professionalId)
        {
            var queue = await _storeRepository.CalculateAverageWaitingTime(professionalId);

            if (queue?.QueueCustomers == null || !queue.QueueCustomers.Any())
                return (TimeSpan.Zero, TimeSpan.Zero);

            double totalWaitTime = queue.QueueCustomers
                .Sum(qc => qc.Customer.CustomerServices.Sum(s => s.Duration.TotalMinutes));

            double averageWaitTime = totalWaitTime / queue.QueueCustomers.Count;

            return (TimeSpan.FromMinutes(totalWaitTime), TimeSpan.FromMinutes(averageWaitTime));
        }
        public async Task LikeProfessional(LikeStoreProfessionalCommand command)
        {
            throw new NotImplementedException();
        }
        public async Task<CommandResult> CreateAsync(StoreCreateCommand command)
        {
            try
            {
                // Adicionar validações
                //CNPJ EXISTENTE NAO GRAVAR
                var store = new Store(command);

                if (!store.IsValid())
                    return new CommandResult(false, Resources.MissingInfo);

                var owner = await _userRepository.GetByIdAsync(command.OwnerId);

                if (owner is null)
                    return new CommandResult(false, Resources.OwnerNotFound);

                owner.ChageUserToOwner();

                store.SetOwner(owner);

                var category = await _categoryRepository.GetByIdAsync(command.CategoryId);

                if (category is null)
                    return new CommandResult(false, Resources.OwnerNotFound);

                store.SetCategory(category);

                await _storeRepository.AddAsync(store);
                await _storeRepository.SaveChangesAsync();

                return new CommandResult(true, store);
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex, ex.InnerException.Message);
            }
        }
        public async Task<CommandResult> UpdateAsync(StoreEditCommand command)
        {
            try
            {
                var store = await _storeRepository.GetByIdAsync(command.Id);

                if (store is null)
                    return new CommandResult(false, Resources.NotFound);

                store.UpdateAllUserInfo(command);

                _storeRepository.Update(store);
                await _storeRepository.SaveChangesAsync();

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
    }
}