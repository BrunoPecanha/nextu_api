using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using uff.Domain.Commands.User;
using uff.Domain;

namespace WeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("auth")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCommand request)
        {   
            var token = await _authService.AuthSync(request.Email, request.Password);
           
            if (string.IsNullOrWhiteSpace(token))
                return Unauthorized();

            return Ok(new { Token = token });
        }
    }
}