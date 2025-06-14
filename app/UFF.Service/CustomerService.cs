using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Customer;
using UFF.Domain.Commands.Queue;
using UFF.Domain.Dto;
using UFF.Domain.Repository;
using UFF.Domain.Services;
using UFF.Service.Hubs;
using UFF.Service.Properties;

namespace UFF.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerServiceRepository _customerServiceRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<QueueHub> _hubContext;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper, IHubContext<QueueHub> hubContext, ICustomerServiceRepository customerServiceRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _hubContext = hubContext;
            _customerServiceRepository = customerServiceRepository;
        }

        public async Task<CommandResult> GetByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer is null)
                return new CommandResult(false, customer);

            return new CommandResult(true, _mapper.Map<CustomerDto>(customer));
        }

        public async Task<CommandResult> GetCustomerHistory(int userId, CustomerHistoryFilterCommand command)
        {
            var customerHistory = await _customerRepository.GetCustomerHistoryAsync(userId, command.StartDate, command.EndDate);

            if (customerHistory == null)
                return new CommandResult(false, customerHistory);

            return new CommandResult(true, _mapper.Map<CustomerHistoryDto[]>(customerHistory));
        }

        public async Task<CommandResult> UpdateAsync(CustomerEditServicesPaymentCommand command)
        {
            try
            {
                var customer = await _customerRepository.GetByIdAsync(command.Id);

                if (customer is null)
                    return new CommandResult(false, Resources.NotFound);

                customer.UpdateCustomer(command, customer.QueueId);

                _customerRepository.Update(customer);
                await _customerRepository.SaveChangesAsync();

                await this.SendUpdateNotificationToGroup(customer.Queue);

                return new CommandResult(true, _mapper.Map<CustomerDto>(customer));
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex.Message);
            }
        }

        public async Task UpdatePriceAndTimeForVariableServiceAsync(CustomerVariablesCommand command)
        {
            try
            {
                var customerServices = await _customerServiceRepository.GetCustomersSelectedServices(command.CustomerId);

                foreach (var service in customerServices)
                {
                    var updatedService = command.CustomerServices
                        .FirstOrDefault(s => s.ServiceId == service.ServiceId);

                    if (updatedService != null)
                    {
                        service.OverridePrice(updatedService.Price);
                        service.OverrideDuration(updatedService.Duration);

                        _customerServiceRepository.Update(service);
                    }
                }

                await _customerServiceRepository.SaveChangesAsync();
                await this.SendUpdateNotificationToGroup(customerServices.FirstOrDefault().Queue);
            }
            catch (Exception ex)
            {
                // Logar o erro
            }
        }

        private async Task SendUpdateNotificationToGroup(Domain.Entity.Queue queue)
        {
            await _hubContext.Clients
                .Group($"company-{queue.StoreId}")
                .SendAsync("UpdateQueue");
        }
    }
}
