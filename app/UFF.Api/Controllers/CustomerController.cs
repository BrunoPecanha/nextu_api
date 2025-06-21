using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UFF.Domain.Commands.Customer;
using UFF.Domain.Commands.Queue;
using UFF.Domain.Services;

namespace WeApi.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var customer = await _service.GetByIdAsync(id);

            if (customer is null)
                BadRequest();

            return Ok(customer);
        }

        [HttpGet("pending/{storeId}")]
        [Authorize]
        public async Task<IActionResult> GetPendingOrdersCount([FromRoute] int storeId)
        {
            var pendingCount = await _service.GetPendingOrdersCount(storeId);

            if (pendingCount is null)
                BadRequest();

            return Ok(pendingCount);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateAsyncAsync([FromBody] CustomerEditServicesPaymentCommand command)
        {
            var response = await _service.UpdateAsync(command);

            if (!response.Valid)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Recupera todas as filas de um estabelecimento
        /// </summary>  
        [HttpPost("{userId}/period")]
        [Authorize]
        public async Task<IActionResult> GetCustomerHistory([FromRoute] int userId, [FromBody] CustomerHistoryFilterCommand command)
        {
            var customerHistory = await _service.GetCustomerHistory(userId, command);

            if (customerHistory is null)
                BadRequest();

            return Ok(customerHistory);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdatePriceAndTimeForVariableServiceAsync([FromBody] CustomerVariablesCommand command)
        {
            await _service.UpdatePriceAndTimeForVariableServiceAsync(command);
            return Ok();
        }
    }
}