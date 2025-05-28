using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UFF.Domain.Services;
using UFF.Domain.Commands;
using UFF.Domain.Commands.User;

namespace WeApi.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] UserCreateCommand command)
        {
            var response = await _service.CreateAsync(command);

            if (!response.Valid)
                return BadRequest(response.Data);

            return Ok(response);
        }

        [HttpGet("id")]
       // [Authorize]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var costumer = await _service.GetByIdAsync(id);

            if (costumer is null)
                BadRequest(costumer);

            return Ok(costumer);
        }

        //TODO - Implementar paginação - User
        [HttpGet("all")]
       // [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            var costumers = await _service.GetAllAsync();

            if (!costumers.Valid)
                BadRequest(costumers.Data);

            return Ok(costumers);
        }

        [HttpPut]
      //  [Authorize]
        public async Task<IActionResult> UpdateAsync([FromBody] UserEditCommand command)
        {
            var costumer = await _service.UpdateAsync(command);

            if (!costumer.Valid)
                return BadRequest(costumer.Data);

            return Ok(costumer);
        }

        [HttpDelete]
       // [Authorize]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result.Valid)
                return BadRequest(new CommandResult(false, result.Data));

            return Ok(new CommandResult(true, string.Empty));
        }
    }
}