using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UFF.Domain.Commands.Service;
using UFF.Domain.Services;

namespace WeApi.Controllers
{
    [ApiController]
    [Route("api/services")]
    public class ServiceController : Controller
    {
        private readonly IServiceService _service;

        public ServiceController(IServiceService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] ServiceCreateCommand command)
        {
            var response = await _service.CreateAsync(command);

            if (!response.Valid)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("{serviceId}")]
        public async Task<IActionResult> UpdateAsyncAsync(int serviceId, [FromForm] ServiceEditCommand command)
        {
            var response = await _service.UpdateAsync(command);

            if (!response.Valid)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("all/{idStore}")]
        //[Authorize]
        public async Task<IActionResult> GetAllAsync([FromRoute]int idStore)
        {
            var services = await _service.GetAllAsync(idStore);

            if (!services.Valid)
                BadRequest(services.Data);

            return Ok(services);
        }

        [HttpGet("{id}")]
        //[Authorize]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var service = await _service.GetByIdAsync(id);

            if (service is null)
                BadRequest(service);

            return Ok(service);
        }

        [HttpGet("categories")]
        //[Authorize]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var store = await _service.GetAllCategoriesAsync();

            if (store is null)
                BadRequest(store);

            return Ok(store);
        }
    }
}