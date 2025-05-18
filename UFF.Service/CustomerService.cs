using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections;
using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Customer;
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
        private readonly IMapper _mapper;
        private readonly IHubContext<QueueHub> _hubContext;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper, IHubContext<QueueHub> hubContext)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        public async Task<CommandResult> GetByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer is null)
                return new CommandResult(false, customer);            

            return new CommandResult(true, _mapper.Map<CustomerDto>(customer));
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

        private async Task SendUpdateNotificationToGroup(Domain.Entity.Queue queue)
        {
            await _hubContext.Clients
                .Group($"company-{queue.StoreId}")
                .SendAsync("UpdateQueue");
        }
    }
}