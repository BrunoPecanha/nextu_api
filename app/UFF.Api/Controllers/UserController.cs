using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UFF.Domain.Commands;
using UFF.Domain.Commands.User;
using UFF.Domain.Enum;
using UFF.Domain.Services;

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

        [HttpGet("{id}")]
       // [Authorize]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var costumer = await _service.GetByIdAsync(id);

            if (costumer is null)
                BadRequest(costumer);

            return Ok(costumer);
        }

        /// <summary>
        /// Recupera os dados do usuário (quantas filas ele esta), colaborador (clientes aguardando) , proprietário (quantas filas abertas)
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <param name="profile">Perfil que quer verificar</param>
        /// <returns></returns>
        [HttpGet("info/{id}/{profile}")]
        // [Authorize]
        public async Task<IActionResult> GetUserInfoByIdAsync([FromRoute] int id, ProfileEnum profile)
        {
            var costumer = await _service.GetUserInfoByIdAsync(id, profile);

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
        public async Task<IActionResult> UpdateAsync([FromForm] UserEditCommand command)
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