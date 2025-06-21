using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Order;
using UFF.Domain.Dto;
using UFF.Domain.Entity;
using UFF.Domain.Enum;
using UFF.Domain.Repository;
using UFF.Domain.Services;

namespace UFF.Service
{
    public class OrderService : IOrderService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IQueueService _queueService;
        private readonly IStoreRepository _storeRepository;

        public OrderService(ICustomerRepository customerRepository, IMapper mapper, IQueueService queueService, IStoreRepository storeRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _storeRepository = storeRepository;
            _queueService = queueService;
        }

        public async Task<CommandResult> GetOrdersWatingApprovment(int storeId, int employeeId)
        {
            var store = await _storeRepository.GetByIdAsync(storeId);
            IList<Customer> ordersWaiting = null;

            if (store == null)
                return new CommandResult(false, "Loja não encontrada");

            if (store.ShareQueue)            
                ordersWaiting = await _customerRepository.GetOrdersWatingApprovment(storeId);            
            else
                ordersWaiting = await _customerRepository.GetOrdersWatingApprovment(storeId, employeeId);

            return new CommandResult(true, _mapper.Map<List<OrderDto>>(ordersWaiting));
        }

        public async Task<CommandResult> GetOrdersWatingApprovment(int storeId)
        {
            var ordersWaiting = await _customerRepository.GetOrdersWatingApprovment(storeId);

            if (ordersWaiting is null || !ordersWaiting.Any())
                return new CommandResult(false, ordersWaiting);

            return new CommandResult(true, _mapper.Map<List<OrderDto>>(ordersWaiting));
        }

        public async Task<CommandResult> ProcessOrder(int id, OrderProcessCommand command)
        {
            try
            {
                var order = await _customerRepository.GetByIdAsync(id);

                if (order == null)
                    return new CommandResult(false, "Pedido não encontrado");

                if (command.Status == CustomerStatusEnum.Rejected)
                    order.SetRejectInfo(command.RejectReason, command.ResposibleEmployee);
                else
                    order.Accept(command.ResposibleEmployee);

                _customerRepository.Update(order);
                await _customerRepository.SaveChangesAsync();

                await _queueService.SendUpdateNotificationToGroup(order.Queue);

                return new CommandResult(true, _mapper.Map<OrderDto>(order));
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex.Message);
            }
        }
    }
}