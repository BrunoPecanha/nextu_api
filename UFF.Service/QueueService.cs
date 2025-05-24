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

                int nextPositionInQueue = await _customerRepository.GetLastPositionInQueueByStoreAndEmployeeIdAsync(queue.StoreId, queue.EmployeeId);

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

        public async Task<CommandResult> CreateQueueAsync(QueueCreateCommand command)
        {

            if (!command.IsValid)
            {
                return new CommandResult(false, command.Notifications);
            }

            try
            {

                await _unitOfWork.BeginTransactionAsync();

                var employee = await _userRepository.GetByIdAsync(command.EmployeeId);

                if (employee == null)
                    return new CommandResult(false, "Funcionário não encontrada");

                var queue = new Queue(command.Description, command.StoreId, command.EmployeeId);

                if (!queue.IsValid())
                    return new CommandResult(false, "Falha ao criar a fila");

                await _queueRepository.AddAsync(queue);

                await _unitOfWork.CommitAsync();

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
                    return new CommandResult(false, "Cliente não encontrado");

                customer.SetStartTime();
                _customerRepository.Update(customer);

                await RecalculateQueueAsync(customer.Queue.StoreId, customer.Queue.EmployeeId, customer.Id);

                await _unitOfWork.CommitAsync();
                await SendUpdateNotificationToGroup(customer.Queue);

                return new CommandResult(true, $"Atendimento inicializado para o cliente {customer.Id}");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new CommandResult(false, $"Erro ao iniciar atendimento: {ex.Message}");
            }
        }

        public async Task<CommandResult> SetTimeCustomerWasCalledInTheQueue(int customerId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var customer = await _customerRepository.GetByIdReducedAsync(customerId);
                if (customer == null)
                    return new CommandResult(false, "Cliente não encontrado");

                customer.SetTimeCalledInQueue();
                _customerRepository.Update(customer);

                await RecalculateQueueAsync(customer.Queue.StoreId, customer.Queue.EmployeeId, customer.Id);

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
                    return new CommandResult(false, "Cliente não encontrado");

                customer.SetEndTime();
                _customerRepository.Update(customer);

                await RecalculateQueueAsync(customer.Queue.StoreId, customer.Queue.EmployeeId);

                await _unitOfWork.CommitAsync();
                await SendUpdateNotificationToGroup(customer.Queue);

                return new CommandResult(true, $"Cliente {customer.Id} finalizado às {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new CommandResult(false, $"Erro ao finalizar atendimento: {ex.Message}");
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
                _customerRepository.Update(customer);

                await RecalculateQueueAsync(customer.Queue.StoreId, customer.Queue.EmployeeId, customer.Id);

                await _unitOfWork.CommitAsync();
                await SendUpdateNotificationToGroup(customer.Queue);

                return new CommandResult(true, $"Cliente {customer.Id} removido da fila às {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new CommandResult(false, $"Erro ao remover cliente: {ex.Message}");
            }
        }

        public async Task<CommandResult> GetAllByStoreIdAsync(int idStore)
        {
            var queue = await _queueRepository.GetAllByStoreIdAsync(idStore, null, null, null, null);

            if (queue == null)
                return new CommandResult(false, queue);

            return new CommandResult(true, _mapper.Map<QueueDto[]>(queue));
        }

        public async Task<CommandResult> GetAllByDateAndStoreIdAsync(int idStore, QueueFilterRequestCommand command)
        {
            var queue = await _queueRepository.GetAllByStoreIdAsync(idStore, command.ResponsibleId, command.QueueStatus, command.StartDate, command.EndDate);

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

            var customer = queueUserIsIn.FirstOrDefault();
            if (customer == null)
                return new CommandResult(false, null);

            await RecalculateQueueAsync(customer.Queue.StoreId, customer.Queue.EmployeeId);

            var dto = _mapper.Map<CustomerInQueueCardDto[]>(queueUserIsIn);
            await SetEstimatedTimeForCustomer(dto);

            return new CommandResult(true, dto);
        }

        public async Task<CommandResult> GetQueueReport(int id)
        {
            var queueReport = await _queueRepository.GetQueueReport(id);

            if (queueReport == null)
                return new CommandResult(false, queueReport);

            var dto = _mapper.Map<QueueReportDto[]>(queueReport);

            return new CommandResult(true, dto);
        }

        public async Task<CommandResult> GetCustomerInQueueCardDetailsByCustomerId(int customerId, int queueId)
        {
            var customer = await _queueRepository.GetCustomerInQueueCardDetailsByCustomerId(customerId, queueId);

            if (customer == null)
                return new CommandResult(false, customer);

            await RecalculateQueueAsync(customer.Queue.StoreId, customer.Queue.EmployeeId);

            var dto = _mapper.Map<CustomerInQueueCardDetailsDto>(customer);
            await UpdateValuesEstimates(dto, customer);

            return new CommandResult(true, dto);
        }

        public async Task<CommandResult> GetByDateAsync(int idStore, DateTime date)
        {
            var queue = await _queueRepository.GetByDateAsync(date, idStore);

            if (queue == null)
                return new CommandResult(false, queue);

            return new CommandResult(true, _mapper.Map<QueueDto[]>(queue));
        }

        public async Task<CommandResult> CloseQueueAsync(int queueId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var queue = await _queueRepository.GetByIdAsync(queueId);

                if (queue == null)
                    return new CommandResult(false, "Fila não encontrada.");

                queue.Close();
                _queueRepository.Update(queue);

                await _unitOfWork.CommitAsync();
                return new CommandResult(true, $"Fila ${queue.Name} fechada às {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new CommandResult(false, $"Erro ao fechar a fila:  {ex.Message}");
            }
        }

        public async Task<CommandResult> PauseQueueAsync(QueuePauseCommand command)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var queue = await _queueRepository.GetByIdAsync(command.Id);

                if (queue == null)
                    return new CommandResult(false, "Fila não encontrada.");

                if (queue.Status == QueueStatusEnum.Open)
                    queue.Pause(command.PauseReason);
                else
                    queue.Unpause();

                _queueRepository.Update(queue);

                await _unitOfWork.CommitAsync();
                return new CommandResult(true, $"Fila ${queue.Name} pausada às {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new CommandResult(false, $"Erro ao fechar a fila:  {ex.Message}");
            }
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
                _customerRepository.Update(customer);

                await RecalculateQueueAsync(queue.StoreId, queue.EmployeeId);
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

        public async Task<CommandResult> Delete(int queueId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var queue = await _queueRepository.GetByIdAsync(queueId);

                if (queue == null)
                    return new CommandResult(false, "Cliente não encontrado");

                _queueRepository.Remove(queue);

                await _unitOfWork.CommitAsync();

                return new CommandResult(true, "Fila deletada com sucesso");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new CommandResult(false, $"Erro ao remover fila: {ex.Message}");
            }
        }

        #region Método auxiliares
        private async Task UpdateValuesEstimates(CustomerInQueueCardDetailsDto dto, Customer customer)
        {
            if (dto == null)
                return;

            (dto.TotalPeopleInQueue, dto.TimeToWait) = await UpdateTotalLeftTimeInQueueAndTotalCustomers(dto.QueueId, dto.Position, dto.Id);
            InsertTokenIfItsCustomerTurn(dto);
        }
        public async Task RecalculateQueueAsync(int storeId, int employeeId, int? skipCustomerUpdateId = null)
        {
            var customers = await _queueRepository.GetAllCustomersInQueueByEmployeeAndStoreId(storeId, employeeId);

            var inService = customers.FirstOrDefault(c => c.Status == CustomerStatusEnum.InService);

            var waitingList = customers
                              .Where(c => c.Status == CustomerStatusEnum.Waiting || c.Status == CustomerStatusEnum.Absent)
                              .OrderBy(c => c.TimeEnteredQueue);


            var acumulatedTime = TimeSpan.Zero;

            if (inService != null)
            {
                inService.SetPosition(0);

                var totalServiceTime = TimeSpan.FromMinutes(inService.CustomerServices
                    .Where(s => s.Service != null)
                    .Sum(s => s.Service.Duration.TotalMinutes));

                if (inService.ServiceStartTime.HasValue)
                {
                    var elapsed = DateTime.UtcNow.ToLocalTime() - inService.ServiceStartTime.Value.ToLocalTime();
                    var remaining = totalServiceTime - elapsed;
                    inService.EstimatedWaitingTime = remaining > TimeSpan.Zero ? remaining : TimeSpan.Zero;
                    acumulatedTime = inService.EstimatedWaitingTime;
                }
                else
                {
                    inService.EstimatedWaitingTime = totalServiceTime;
                    acumulatedTime += totalServiceTime;
                }

                if (skipCustomerUpdateId != inService.Id)
                    _customerRepository.Update(inService);
            }

            int position = 1;
            foreach (var customer in waitingList.Where(x => x.Id != skipCustomerUpdateId))
            {
                customer.SetPosition(position++);

                var totalServiceTime = TimeSpan.FromMinutes(customer.CustomerServices
                    .Where(s => s.Service != null)
                    .Sum(s => s.Service.Duration.TotalMinutes));

                customer.EstimatedWaitingTime = acumulatedTime;
                acumulatedTime += totalServiceTime;

                _customerRepository.Update(customer);
            }
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
                (_, customerInQueueCard.TimeToWait) = await UpdateTotalLeftTimeInQueueAndTotalCustomers(customerInQueueCard.QueueId, customerInQueueCard.Position, customerInQueueCard.Id);
            }
        }
        private async Task<(int totalPeopleInQueue, TimeSpan timeToWait)> UpdateTotalLeftTimeInQueueAndTotalCustomers(int queueId, int position, int id)
            => await _queueRepository.GetQueueStatusAsync(queueId, position, id);
        private void InsertTokenIfItsCustomerTurn(CustomerInQueueCardDetailsDto dto)
        {
            if (dto != null && (dto.Position == 0))
                dto.Token = _tokenService.CreateToken(dto.Id, dto.QueueId);
        }
        #endregion
    }
}