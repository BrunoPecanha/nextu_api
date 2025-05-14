using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Customer;
using UFF.Domain.Commands.Queue;
using UFF.Domain.Services;

namespace WeApi.Controllers
{
    [ApiController]
    [Route("api/queue")]
    public class QueueController : Controller
    {
        private readonly IQueueService _service;

        public QueueController(IQueueService service)
        {
            _service = service;
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> AddCustomerToQueueAsync([FromBody] QueueAddCustomerCommand command)
        {
            var response = await _service.AddCustomerToQueueAsync(command);

            if (!response.Valid)
                return BadRequest(response.Data);

            return Ok(response);
        }

        [HttpGet("start-service/{customerId}")]
        public async Task<IActionResult> StartCustomerService([FromRoute] int customerId)
        {
            var customer = await _service.StartCustomerService(customerId);

            if (!customer.Valid)
                return BadRequest(customer);

            return Ok(customer);
        }

        [HttpGet("notify-customer/{customerId}")]
        public async Task<IActionResult> NotifyTimeCustomerWasCalledInTheQueue([FromRoute] int customerId)
        {
            var customer = await _service.SetTimeCustomerWasCalledInTheQueue(customerId);

            if (!customer.Valid)
                return BadRequest(customer);

            return Ok(customer);
        }

        [HttpPut("finish-service/{customerId}")]
        public async Task<IActionResult> NotifyTimeCustomerServiceWasCompleted([FromRoute] int customerId)
        {
            var customer = await _service.SetTimeCustomerServiceWasCompleted(customerId);

            if (!customer.Valid)
                return BadRequest(customer);

            return Ok(customer);
        }

        [HttpPut("remove")]
        public async Task<IActionResult> RemoveMissingCustomer([FromBody] CustomerRemoveFromQueueCommand command)
        {
            var customer = await _service.RemoveMissingCustomer(command);

            if (!customer.Valid)
                return BadRequest(customer);

            return Ok(customer);
        }

        [HttpPost("date")]
        //[Authorize]
        public async Task<IActionResult> GetByDateAsync([FromBody] QueueRequestCommand command)
        {
            var queues = await _service.GetByDateAsync(command.StoreId, command.Date);

            if (queues == null)
                BadRequest(queues);

            return Ok(queues);
        }

        [HttpGet("{id}/employee")]
        //[Authorize]
        public async Task<IActionResult> GetOpenedQueueByEmployeeId([FromRoute] int id)
        {
            var queues = await _service.GetOpenedQueueByEmployeeId(id);

            if (queues == null)
                BadRequest(queues);

            return Ok(queues);
        }

        [HttpGet("{storeId}/{employeeId}/customers-in-queue")]
        //[Authorize]
        public async Task<IActionResult> GetAllCustomersInQueueByEmployeeAndStoreId([FromRoute] int storeId, int employeeId)
        {
            var customers = await _service.GetAllCustomersInQueueByEmployeeAndStoreId(storeId, employeeId);

            if (customers == null)
                BadRequest(customers);

            return Ok(customers);
        }

        [HttpGet("{customerId}/card")]
        //[Authorize]
        public async Task<IActionResult> GetCustomerInQueueCardByCustomerId([FromRoute] int userId)
        {
            var queueUserIsIn = await _service.GetCustomerInQueueCardByCustomerId(userId);

            if (queueUserIsIn == null)
                BadRequest(queueUserIsIn);

            return Ok(queueUserIsIn);
        }

        [HttpGet("{customerId}/{queueId}/card/details")]
        //[Authorize]
        public async Task<IActionResult> GetCustomerInQueueCardDetailsByCustomerId([FromRoute] int customerId, int queueId)
        {
            var customers = await _service.GetCustomerInQueueCardDetailsByCustomerId(customerId, queueId);

            if (customers == null)
                BadRequest(customers);

            return Ok(customers);
        }

        [HttpGet("{idStore}")]
        //[Authorize]
        public async Task<IActionResult> GetAllByStoreIdAsync(int idStore)
        {
            var queue = await _service.GetAllByStoreIdAsync(idStore);

            if (!queue.Valid)
                BadRequest(queue.Data);

            return Ok(queue);
        }

        [HttpDelete]
        //[Authorize]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result.Valid)
                return BadRequest(new CommandResult(false, result.Data));

            return Ok(new CommandResult(true, string.Empty));
        }
    }
}