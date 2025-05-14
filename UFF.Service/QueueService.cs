using AutoMapper;
using System;
using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Customer;
using UFF.Domain.Commands.Queue;
using UFF.Domain.Dto;
using UFF.Domain.Entity;
using UFF.Domain.Repository;
using UFF.Domain.Services;

namespace UFF.Service
{
    public class QueueService : IQueueService
    {
        private readonly IQueueRepository _queueRepository;
        private readonly IUserRepository _userRepository;
        private readonly IQueueCustomerRepository _queueCustomerRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerServiceRepository _customerServiceRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public QueueService(IQueueRepository repository, IMapper mapper, IUserRepository userRepository, IQueueCustomerRepository queueCustomerRepository,
            ICustomerRepository customerRepository, ICustomerServiceRepository customerServiceRepository, IServiceRepository serviceRepository, IUnitOfWork uow)
        {
            _queueRepository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
            _queueCustomerRepository = queueCustomerRepository;
            _customerRepository = customerRepository;
            _customerServiceRepository = customerServiceRepository;
            _serviceRepository = serviceRepository;
            _unitOfWork = uow;
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

                var queueCustomer = new QueueCustomer(customer, user, queue);
                await _queueCustomerRepository.AddAsync(queueCustomer);

                foreach (var selectedService in command.SelectedServices)
                {
                    var service = await _serviceRepository.GetByIdAsync(selectedService.ServiceId);
                    if (service == null)
                        continue;

                    var customerServiceSeleted = new CustomerService(customer, service, queue, selectedService.Quantity, service.Id, service.Duration);
                    await _customerServiceRepository.AddAsync(customerServiceSeleted);
                }

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

                var customer = await _customerRepository.GetByIdAsync(customerId);
                if (customer == null)
                    return new CommandResult(false, "Cliente não encontrada");

                customer.SetStartTime();
                _customerRepository.Update(customer);

                await _unitOfWork.CommitAsync();
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

                var customer = await _customerRepository.GetByIdAsync(customerId);
                if (customer == null)
                    return new CommandResult(false, "Cliente não encontrada");

                customer.SetTimeCalledInQueue();
                _customerRepository.Update(customer);

                //Criar tabela de notificações, guardar a notificação e enviar via push para o cliente;

                await _unitOfWork.CommitAsync();
                return new CommandResult(true, customer.TimeCalledInQueue.Value.ToString("HH:mm"));
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

                var customer = await _customerRepository.GetByIdAsync(customerId);
                if (customer == null)
                    return new CommandResult(false, "Cliente não encontrada");

                customer.SetEndTime();
                _customerRepository.Update(customer);

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

                var customer = await _customerRepository.GetByIdAsync(command.CustomerId);
                if (customer == null)
                    return new CommandResult(false, "Cliente não encontrado");

                customer.RemoveMissingCustomer(command.RemoveReason);
                _customerRepository.Update(customer);

                await _unitOfWork.CommitAsync();
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

        public async Task<CommandResult> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        private async Task UpdateTotalLeftTimeInQueueAndTotalCustomers(CustomerInQueueCardDetailsDto customerInQueueCardDetailsDto)
            => (customerInQueueCardDetailsDto.TotalPeopleInQueue, customerInQueueCardDetailsDto.TimeToWait) = await _queueRepository.GetQueueStatusAsync(customerInQueueCardDetailsDto.QueueId, customerInQueueCardDetailsDto.Position);
    }
}