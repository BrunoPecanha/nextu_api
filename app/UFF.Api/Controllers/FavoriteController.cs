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

        [HttpPost("like/store/id/userId")]
     //   [Authorize]
        public async Task<IActionResult> LikeStore(int id, int userId)
        {
            var like = await _service.LikeStore(id, userId);

            if (!like.Valid)
                BadRequest(like.Data);

            return Ok(like);
        }

        [HttpPost("dislike/store/id/userId")]
        //   [Authorize]
        public async Task<IActionResult> DislikeStore(int storeId, int userId)
        {
            var dislike = await _service.DislikeStore(storeId, userId);

            if (!dislike.Valid)
                BadRequest(dislike.Data);

            return Ok(dislike);
        }

        [HttpPost("like/professional/id/userId")]
        //   [Authorize]
        public async Task<IActionResult> LikeProfessional(int id, int userId)
        {
            var like = await _service.LikeProfessional(id, userId);

            if (!like.Valid)
                BadRequest(like.Data);

            return Ok(like);
        }

        [HttpPost("dislike/professional/id/userId")]
        //   [Authorize]
        public async Task<IActionResult> DislikeProfessional(int id, int userId)
        {
            var like = await _service.DislikeProfessional(id, userId);

            if (!like.Valid)
                BadRequest(like.Data);

            return Ok(like);
        }
    }
}