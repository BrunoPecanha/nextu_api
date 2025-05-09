using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
            var store = await _service.GetByIdAsync(id);

            if (store is null)
                BadRequest(store);

            return Ok(store);
        }
    }
}