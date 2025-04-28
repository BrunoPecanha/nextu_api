using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UFF.Domain.Repository;
using UFF.Domain.Services;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Store;

namespace WeApi.Controllers
{
    [ApiController]
    [Route("api/store")]
    public class StoreController : Controller
    {
        private readonly IStoreService _service;

        public StoreController(IStoreService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] StoreCreateCommand command)
        {
            var response = await _service.CreateAsync(command);

            if (!response.Valid)
                return BadRequest(response.Data);

            return Ok(response);
        }

        [HttpGet("id")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var store = await _service.GetByIdAsync(id);

            if (store is null)
                BadRequest(store);

            return Ok(store);
        }

        //TODO - Implementar paginação - Store
        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            var stores = await _service.GetAllAsync();

            if (!stores.Valid)
                BadRequest(stores.Data);

            return Ok(stores);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromBody] StoreEditCommand command)
        {
            var store = await _service.UpdateAsync(command);

            if (!store.Valid)
                return BadRequest(store.Data);

            return Ok(store);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result.Valid)
                return BadRequest(new CommandResult(false, result.Data));

            return Ok(new CommandResult(true, string.Empty));
        }
    }
}