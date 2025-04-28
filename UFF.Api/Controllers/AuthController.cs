using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UFF.Domain.Commands.User;
using UFF.Domain.Repository;

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

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCommand request)
        {   
            var result = await _authService.AuthSync(request.Email, request.Password);
           
            if (!result.Valid)
                return Unauthorized();

            return Ok(result);
        }
    }
}