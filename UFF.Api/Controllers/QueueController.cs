using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Queue;
using UFF.Domain.Commands.Store;
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
        public async Task<IActionResult> CreateAsync([FromBody] StoreCreateCommand command)
        {
            var response = await _service.CreateAsync(null);

            if (!response.Valid)
                return BadRequest(response.Data);

            return Ok(response);
        }

        [HttpPut]
        //[Authorize]
        public async Task<IActionResult> UpdateAsync([FromBody] StoreEditCommand command)
        {
            var store = await _service.UpdateAsync(null);

            if (!store.Valid)
                return BadRequest(store.Data);

            return Ok(store);
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

        [HttpGet("{customerId}/customer-in-queue")]
        //[Authorize]
        public async Task<IActionResult> GetCustomersInQueueByCustomerId([FromRoute]int customerId)
        {
            var customers = await _service.GetCustomerInQueueReducedByCustomerId(customerId);

            if (customers == null)
                BadRequest(customers);

            return Ok(customers);
        }

        [HttpGet("{customerId}/customer-in-queue/complement")]
        //[Authorize]
        public async Task<IActionResult> GetCustomerInQueueComplementByCustomerId([FromRoute] int customerId)
        {
            var customers = await _service.GetCustomerInQueueComplementByCustomerId(customerId);

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