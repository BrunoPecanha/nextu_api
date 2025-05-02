using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.Store;
using UFF.Domain.Services;

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
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var store = await _service.GetByIdAsync(id);

            if (store is null)
                BadRequest(store);

            return Ok(store);
        }

    
        [HttpGet("owner/{id}")]
        //[Authorize]
        public async Task<IActionResult> GetByOwnerId([FromRoute] int id)
        {
            var stores = await _service.GetByOwnerIdAsync(id);

            if (!stores.Valid)
                BadRequest(stores.Data);

            return Ok(stores);
        }


        [HttpGet("employee/{id}")]
        //[Authorize]
        public async Task<IActionResult> GetByEmployeeId([FromRoute] int id)
        {
            var stores = await _service.GetByEmployeeId(id);

            if (!stores.Valid)
                BadRequest(stores.Data);

            return Ok(stores);
        }

        [HttpGet("{categoryId}/stores")]

        public async Task<IActionResult> GetByCategoryId([FromRoute] int categoryId)
        {
            var stores = await _service.GetByCategoryIdAsync(categoryId);

            if (!stores.Valid)
                BadRequest(stores.Data);

            return Ok(stores);
        }


        [HttpGet("all")]
        //  [Authorize]
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