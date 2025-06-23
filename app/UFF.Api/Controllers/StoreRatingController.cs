using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UFF.Domain.Commands.Store;
using UFF.Domain.Services;

namespace WeApi.Controllers
{
    [ApiController]
    [Route("api/store-rating")]
    public class StoreRatingController : Controller
    {
        private readonly IStoreRatingService _service;

        public StoreRatingController(IStoreRatingService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            var storeRatings = await _service.GetAllAsync();

            if (!storeRatings.Valid)
                BadRequest(storeRatings.Data);

            return Ok(storeRatings);
        }

        [HttpGet("all/store/id")]
        [Authorize]
        public async Task<IActionResult> GetAllAsync(int id)
        {
            var storeRatings = await _service.GetByStoreIdAsync(id);

            if (!storeRatings.Valid)
                BadRequest(storeRatings.Data);

            return Ok(storeRatings);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var storeRating = await _service.GetByIdAsync(id);

            if (storeRating is null)
                BadRequest(storeRating);

            return Ok(storeRating);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromForm] StoreRatingCommand command)
        {
            var response = await _service.CreateAsync(command);

            if (!response.Valid)
                return BadRequest(response);

            return Ok(response);
        }
    }
}