using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using uff.domain.Services;

namespace WeApi.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;


        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet("all")]
      //  [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await _service.GetAllAsync();

            if (!categories.Valid)
                BadRequest(categories.Data);

            return Ok(categories);
        }       
    }
}