using AutoMapper;
using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Dto;
using UFF.Domain.Repository;
using UFF.Domain.Services;

namespace UFF.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CommandResult> GetByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer is null)
                return new CommandResult(false, customer);            

            return new CommandResult(true, _mapper.Map<CustomerDto>(customer));
        }       
    }
}