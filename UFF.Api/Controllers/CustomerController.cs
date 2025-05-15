using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
      //  [Authorize]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var customer = await _service.GetByIdAsync(id);

            if (customer is null)
                BadRequest(customer);

            return Ok(customer);
        }
    }
}