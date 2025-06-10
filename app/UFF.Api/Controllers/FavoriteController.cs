using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UFF.Domain.Services;

namespace WeApi.Controllers
{
    [ApiController]
    [Route("api/favorite")]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteService _service;

        public FavoriteController(IFavoriteService service)
        {
            _service = service;
        }

        [HttpPost("like/store/{storeId}/{userId}")]
        //   [Authorize]
        public async Task<IActionResult> LikeStore(int storeId, int userId)
        {
            var like = await _service.LikeStore(storeId, userId);

            if (!like.Valid)
               return BadRequest(like.Data);

            return Ok(like);
        }

        [HttpDelete("dislike/store/{storeId}/{userId}")]
        //   [Authorize]
        public async Task<IActionResult> DislikeStore(int storeId, int userId)
        {
            var dislike = await _service.DislikeStore(storeId, userId);

            if (!dislike.Valid)
                return BadRequest(dislike.Data);

            return Ok(dislike);
        }

        [HttpPost("like/professional/{storeId}/{userId}")]
        //   [Authorize]
        public async Task<IActionResult> LikeProfessional(int storeId, int userId)
        {
            var like = await _service.LikeProfessional(storeId, userId);

            if (!like.Valid)
                return BadRequest(like.Data);

            return Ok(like);
        }

        [HttpDelete("dislike/professional/{storeId}/{userId}")]
        //   [Authorize]
        public async Task<IActionResult> DislikeProfessional(int storeId, int userId)
        {
            var like = await _service.DislikeProfessional(storeId, userId);

            if (!like.Valid)
                return BadRequest(like.Data);

            return Ok(like);
        }
    }
}