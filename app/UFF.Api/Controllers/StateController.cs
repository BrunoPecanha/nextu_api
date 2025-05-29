using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UFF.Service;

namespace WeApi.Controllers
{
    [ApiController]
    [Route("api/state")]
    public class StateController : Controller
    {
        [HttpGet("id")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(string id)
        {            
            var result = await StateService.GetBySiglaAsync(id);

            if (!result.Valid)
                return BadRequest(result.Data); 

            return Ok(result); 
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {            
            var result = await StateService.GetAllAsync();

            if (!result.Valid)
                return BadRequest(result.Data); 

            return Ok(result); 
        }
    }
}
