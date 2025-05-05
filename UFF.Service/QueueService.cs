using AutoMapper;
using System;
using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Queue;
using UFF.Domain.Dto;
using UFF.Domain.Entity;
using UFF.Domain.Repository;
using UFF.Domain.Services;
using UFF.Service.Properties;

namespace UFF.Service
{
    public class QueueService : IQueueService
    {
        private readonly IQueueRepository _queueRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;

        public QueueService(IQueueRepository repository, IMapper mapper, IStoreRepository storeRepository)
        {
            _queueRepository = repository;
            _mapper = mapper;
            _storeRepository = storeRepository;
        }

        public async Task<CommandResult> CreateAsync(QueueCreateCommand command)
        {
            try
            {
                var store = await _storeRepository.GetByIdAsync(command.StoreId);

                if (store == null)
                    return new CommandResult(false, "Store Not Found");

                var queue = new Queue(command.Description, store);

                if (!queue.IsValid())
                    return new CommandResult(false, Resources.MissingInfo);

                await _queueRepository.AddAsync(queue);
                await _queueRepository.SaveChangesAsync();

                return new CommandResult(true, queue);
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex.Message);
            }
        }

        public async Task<CommandResult> GetAllByStoreIdAsync(int idStore)
        {
            var queue = await _queueRepository.GetAllByStoreIdAsync(idStore);

            if (queue == null)
                return new CommandResult(false, queue);

            return new CommandResult(true, _mapper.Map<QueueDto[]>(queue));
        }

        public async Task<CommandResult> GetOpenedQueueByEmployeeId(int id)
        {
            var queue = await _queueRepository.GetOpenedQueueByEmployeeId(id);

            if (queue == null)
                return new CommandResult(false, queue);

            return new CommandResult(true, _mapper.Map<QueueDto[]>(queue));
        }

        public async Task<CommandResult> GetAllCustomersInQueueByEmployeeAndStoreId(int storeId, int employeeId)
        {
            var customers = await _queueRepository.GetAllCustomersInQueueByEmployeeAndStoreId(storeId, employeeId);

            if (customers == null)
                return new CommandResult(false, customers);

            var dto = _mapper.Map<CustomerInQueueForEmployeeDto[]>(customers);

            return new CommandResult(true, dto);
        }

        public async Task<CommandResult> GetCustomerInQueueCardByCustomerId(int userId)
        {
            var customers = await _queueRepository.GetCustomerInQueueCardByUserId(userId);

            if (customers == null)
                return new CommandResult(false, customers);

            var dto = _mapper.Map<CustomerInQueueCardDto[]>(customers);

            return new CommandResult(true, dto);
        }

        public async Task<CommandResult> GetCustomerInQueueCardDetailsByCustomerId(int customerId, int queueId)
        {
            var customers = await _queueRepository.GetCustomerInQueueCardDetailsByCustomerId(customerId, queueId);

            if (customers == null)
                return new CommandResult(false, customers);

            var dto = _mapper.Map<CustomerInQueueCardDetailsDto>(customers);
            await UpdateTotalLeftTimeInQueueAndTotalCustomers(dto);


            return new CommandResult(true, dto);
        }

        public async Task<CommandResult> GetByDateAsync(int idStore, DateTime date)
        {
            var queue = await _queueRepository.GetByDateAsync(date, idStore);

            if (queue == null)
                return new CommandResult(false, queue);

            return new CommandResult(true, _mapper.Map<QueueDto[]>(queue));
        }

        public async Task<CommandResult> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CommandResult> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<CommandResult> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        private async Task UpdateTotalLeftTimeInQueueAndTotalCustomers(CustomerInQueueCardDetailsDto customerInQueueCardDetailsDto)
            => (customerInQueueCardDetailsDto.TotalPeopleInQueue, customerInQueueCardDetailsDto.TimeToWait) = await _queueRepository.GetQueueStatusAsync(customerInQueueCardDetailsDto.QueueId, customerInQueueCardDetailsDto.Position);
       
        public async Task<CommandResult> UpdateAsync(QueueEditCommand command)
        {
            throw new NotImplementedException();
        }
    }
}