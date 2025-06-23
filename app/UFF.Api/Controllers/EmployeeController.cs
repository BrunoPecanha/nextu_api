using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UFF.Domain.Commands.Employee;
using UFF.Domain.Services;

namespace WeApi.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeStoreService _service;

        public EmployeeController(IEmployeeStoreService service)
        {
            _service = service;
        }

        [HttpPost("send")]
        [Authorize]
        public async Task<IActionResult> SendInviteToEmployee([FromBody] EmployeeStoreSendInviteCommand command)
        {
            var invite = await _service.SendInviteToEmployee(command);

            if (invite is null)
                BadRequest(invite);

            return Ok(invite);
        }

        [HttpPost("respond")]
        [Authorize]
        public async Task<IActionResult> RespondInvite([FromBody] EmployeeStoreAswerInviteCommand command)
        {
            var invite = await _service.RespondInvite(command);

            if (invite is null)
                BadRequest(invite);

            return Ok(invite);
        }

        [HttpGet("employee-invites/{id}")]
        [Authorize]
        [Authorize]
        public async Task<IActionResult> GetPendingAndAcceptedInvitesByUser(int id)
        {
            var response = await _service.GetPendingAndAcceptedInvitesByUser(id);

            if (!response.Valid)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("store-invites/{id}")]
        [Authorize]
        [Authorize]
        public async Task<IActionResult> GetPendingAndAcceptedInvitesByStore(int id)
        {
            var response = await _service.GetPendingAndAcceptedInvitesByStore(id);

            if (!response.Valid)
                return BadRequest(response);

            return Ok(response);
        }
    }
}