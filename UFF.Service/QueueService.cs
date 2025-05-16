using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Customer;
using UFF.Domain.Commands.Queue;
using UFF.Domain.Dto;
using UFF.Domain.Entity;
using UFF.Domain.Enum;
using UFF.Domain.Repository;
using UFF.Domain.Services;
using UFF.Service.Hubs;

namespace UFF.Service
{
    public class QueueService : IQueueService
    {
        private readonly IQueueRepository _queueRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerServiceRepository _customerServiceRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IHubContext<QueueHub> _hubContext;

        public QueueService(IQueueRepository repository, IMapper mapper, IUserRepository userRepository,
            ICustomerRepository customerRepository, ICustomerServiceRepository customerServiceRepository, IServiceRepository serviceRepository, IUnitOfWork uow,
            IHubContext<QueueHub> hubContext, ITokenService tokenService)
        {
            _queueRepository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _customerServiceRepository = customerServiceRepository;
            _serviceRepository = serviceRepository;
            _unitOfWork = uow;
            _hubContext = hubContext;
            _tokenService = tokenService;
        }

        public async Task<CommandResult> AddCustomerToQueueAsync(QueueAddCustomerCommand command)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var queue = await _queueRepository.GetByIdAsync(command.QueueId);
                if (queue == null)
                    return new CommandResult(false, "Fila não encontrada");

                var user = await _userRepository.GetByIdAsync(command.UserId);

                int nextPositionInQueue = await _customerRepository.GetByLasPositionInQueueByStoreAndEmployeeIdAsync(queue.StoreId, queue.EmployeeId);

                var customer = new Customer(user, queue, command.PaymentMethod, command.Notes, nextPositionInQueue);

                await _customerRepository.AddAsync(customer);
                await _unitOfWork.SaveChangesAsync();

                foreach (var selectedService in command.SelectedServices)
                {
                    var service = await _serviceRepository.GetByIdAsync(selectedService.ServiceId);
                    if (service == null)
                        continue;

                    var customerServiceSeleted = new Domain.Entity.CustomerService(customer, service, queue, selectedService.Quantity, service.Price, service.Duration);
                    await _customerServiceRepository.AddAsync(customerServiceSeleted);
                }

                await _unitOfWork.CommitAsync();
                await SendUpdateNotificationToGroup(queue);

                return new CommandResult(true, "Cliente adicionado à fila com sucesso");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new CommandResult(false, $"Erro ao adicionar à fila: {ex.Message}");
            }
        }

        public async Task<CommandResult> StartCustomerService(int customerId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var customer = await _customerRepository.GetByIdReducedAsync(customerId);
                if (customer == null)
                    return new CommandResult(false, "Cliente não encontrada");

                customer.SetStartTime();
                await UpdateEstimatedWaitTimeInMinutes(customer, true);           

                await _unitOfWork.CommitAsync();

                await SendUpdateNotificationToGroup(customer.Queue);
                return new CommandResult(true, $"Atendimento inicializado para o cliente {customer.Id}");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new CommandResult(false, $"Erro ao adicionar à fila: {ex.Message}");
            }
        }

        public async Task<CommandResult> SetTimeCustomerWasCalledInTheQueue(int customerId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var customer = await _customerRepository.GetByIdReducedAsync(customerId);
                if (customer == null)
                    return new CommandResult(false, "Cliente não encontrada");

                customer.SetTimeCalledInQueue();

                await UpdateEstimatedWaitTimeInMinutes(customer, true);

                await _unitOfWork.CommitAsync();
                await SendUpdateNotificationToGroup(customer.Queue);
                return new CommandResult(true, customer.TimeCalledInQueue.Value.ToLocalTime().ToString("HH:mm"));
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new CommandResult(false, $"Erro ao atualizar o cliente: {ex.Message}");
            }
        }

        public async Task<CommandResult> SetTimeCustomerServiceWasCompleted(int customerId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var customer = await _customerRepository.GetByIdReducedAsync(customerId);
                if (customer == null)
                    return new CommandResult(false, "Cliente não encontrada");

                customer.SetEndTime();

                await UpdateEstimatedWaitTimeInMinutes(customer, true);
                await _unitOfWork.CommitAsync();
                return new CommandResult(true, $"Cliente {customer.Id} finalizado às {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new CommandResult(false, $"Erro ao atualizar o cliente:  {ex.Message}");
            }
        }

        public async Task<CommandResult> RemoveMissingCustomer(CustomerRemoveFromQueueCommand command)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var customer = await _customerRepository.GetByIdReducedAsync(command.CustomerId);
                if (customer == null)
                    return new CommandResult(false, "Cliente não encontrado");

                customer.RemoveMissingCustomer(command.RemoveReason);             

                await UpdateEstimatedWaitTimeInMinutes(customer, true);

                await _unitOfWork.CommitAsync();
                await SendUpdateNotificationToGroup(customer.Queue);
                return new CommandResult(true, $"Cliente {customer.Id} removido da fila às {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new CommandResult(false, $"Erro ao remover cliente:  {ex.Message}");
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
            var queueUserIsIn = await _queueRepository.GetCustomerInQueueCardByUserId(userId);

            if (queueUserIsIn == null)
                return new CommandResult(false, queueUserIsIn);

            var dto = _mapper.Map<CustomerInQueueCardDto[]>(queueUserIsIn);
            await SetEstimatedTimeForCustomer(dto);

            return new CommandResult(true, dto);
        }

        public async Task<CommandResult> GetCustomerInQueueCardDetailsByCustomerId(int customerId, int queueId)
        {
            var customers = await _queueRepository.GetCustomerInQueueCardDetailsByCustomerId(customerId, queueId);

            if (customers == null)
                return new CommandResult(false, customers);

            var dto = _mapper.Map<CustomerInQueueCardDetailsDto>(customers);
            await UpdateValuesEstimates(dto, customers);

            return new CommandResult(true, dto);
        }

        public async Task<CommandResult> GetByDateAsync(int idStore, DateTime date)
        {
            var queue = await _queueRepository.GetByDateAsync(date, idStore);

            if (queue == null)
                return new CommandResult(false, queue);

            return new CommandResult(true, _mapper.Map<QueueDto[]>(queue));
        }

        public async Task<CommandResult> ExitQueueAsync(int customerId, int queueId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var customer = await _customerRepository.GetByIdReducedAsync(customerId);
                if (customer == null)
                    return new CommandResult(false, "Cliente não encontrado");

                var queue = await _queueRepository.GetByIdAsync(queueId);
                if (queue == null)
                    return new CommandResult(false, "Fila não encontrada");

                customer.ExitQueue();

                await UpdateEstimatedWaitTimeInMinutes(customer, true);
                await _unitOfWork.CommitAsync();

                await SendUpdateNotificationToGroup(queue);

                return new CommandResult(true, $"Cliente {customer.Id} removido da fila às {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new CommandResult(false, $"Erro ao remover cliente:  {ex.Message}");
            }
        }

        #region Método auxiliares

        private async Task UpdateValuesEstimates(CustomerInQueueCardDetailsDto dto, Customer customers)
        {
            if (dto == null)
                return;

            (dto.TotalPeopleInQueue, dto.TimeToWait) = await UpdateTotalLeftTimeInQueueAndTotalCustomers(dto.QueueId, dto.Position);
            dto.TimeToWait = await UpdateEstimatedWaitTimeInMinutes(customers);
            InsertTokenIfItsCustomerTurn(dto);
        }
        public async Task<TimeSpan> UpdateEstimatedWaitTimeInMinutes(Customer customer, bool customerFinished = false)
        {
            TimeSpan timeToWait = default;

            var leftCustomersInQueue = await _queueRepository.GetAllCustomersInQueueByEmployeeAndStoreId(customer.Queue.StoreId, customer.Queue.EmployeeId);

            if (leftCustomersInQueue == null || !leftCustomersInQueue.Any())
                return timeToWait;

            var acumulatedTime = TimeSpan.Zero;
            int position = 0;

            foreach (var _customer in leftCustomersInQueue.OrderBy(c => c.Position))
            {
                _customer.SetPosition(position++);

                var totalServiceTime = TimeSpan.FromMinutes(_customer.CustomerServices
                        .Where(s => s.Service != null)
                        .Sum(s => s.Service.Duration.TotalMinutes));

                if (_customer.Status == CustomerStatusEnum.InService && !customerFinished)
                {
                    if (_customer.ServiceStartTime.HasValue)
                    {
                        var elapsed = DateTime.UtcNow.ToLocalTime() - _customer.ServiceStartTime.Value.ToLocalTime();
                        var remaining = totalServiceTime - elapsed;
                        _customer.EstimatedWaitingTime = remaining > TimeSpan.Zero ? remaining : TimeSpan.Zero;
                        acumulatedTime = _customer.EstimatedWaitingTime;
                    }
                    else
                    {
                        _customer.EstimatedWaitingTime = totalServiceTime;
                        acumulatedTime += totalServiceTime;
                    }
                }
                else
                {
                    if (customer.Id == _customer.Id)
                        timeToWait = acumulatedTime;

                    _customer.EstimatedWaitingTime = acumulatedTime;
                    acumulatedTime += totalServiceTime;
                }

                if (customerFinished)
                    _customerRepository.Update(customer);
                else
                    _customerRepository.Update(_customer);
            }

            return timeToWait;
        }
        private async Task SendUpdateNotificationToGroup(Queue queue)
        {
            await _hubContext.Clients
                .Group($"company-{queue.StoreId}")
                .SendAsync("UpdateQueue");
        }
        private async Task SetEstimatedTimeForCustomer(CustomerInQueueCardDto[] customerInQueueCardDtos)
        {
            foreach (var customerInQueueCard in customerInQueueCardDtos)
            {
                (_, customerInQueueCard.TimeToWait) = await UpdateTotalLeftTimeInQueueAndTotalCustomers(customerInQueueCard.QueueId, customerInQueueCard.Position);
            }
        }
        private async Task<(int totalPeopleInQueue, TimeSpan timeToWait)> UpdateTotalLeftTimeInQueueAndTotalCustomers(int queueId, int position)
            => await _queueRepository.GetQueueStatusAsync(queueId, position);
        private void InsertTokenIfItsCustomerTurn(CustomerInQueueCardDetailsDto dto)
        {
            if (dto != null && (dto.Position == 0))
                dto.Token = _tokenService.CreateToken(dto.Id, dto.QueueId);
        }

        #endregion
    }
}