using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Dto;
using UFF.Domain.Repository;
using UFF.Domain.Services;

namespace UFF.Service
{
    public class OrderService : IOrderService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public OrderService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CommandResult> GetOrdersWatingApprovment(int storeId, int employeeId)
        {
            var ordersWaiting = await _customerRepository.GetOrdersWatingApprovment(storeId, employeeId);

            if (ordersWaiting is null || !ordersWaiting.Any())
                return new CommandResult(false, ordersWaiting);

            return new CommandResult(true, _mapper.Map<List<OrderDto>>(ordersWaiting));
        }

        public async Task<CommandResult> GetOrdersWatingApprovment(int storeId)
        {
            var ordersWaiting = await _customerRepository.GetOrdersWatingApprovment(storeId);

            if (ordersWaiting is null || !ordersWaiting.Any())
                return new CommandResult(false, ordersWaiting);

            return new CommandResult(true, _mapper.Map<List<OrderDto>>(ordersWaiting));
        }

    }
}