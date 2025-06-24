using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UFF.Domain.Enum;
using UFF.Domain.Services;

namespace WeApi.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationController : Controller
    {
        private readonly INotificationService _service;

        public NotificationController(INotificationService service)
        {
            _service = service;
        }

        [HttpGet("user/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetUserNotificationsAsync([FromRoute] int userId)
        {
            var notifications = await _service.GetUserNotificationsAsync(userId);
            return Ok(notifications);
        }

        [HttpPut("{id}/mark-as-read")]
        [Authorize]
        public async Task<IActionResult> MarkAsReadAsync([FromRoute] int id)
        {
            await _service.MarkAsReadAsync(id);
            return NoContent();
        }

        [HttpPut("mark-all-as-read/{userId}")]
        [Authorize]
        public async Task<IActionResult> MarkAllAsReadAsync([FromRoute] int userId)
        {
            await _service.MarkAllAsReadAsync(userId);
            return NoContent();
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteNotificationAsync([FromRoute] int id)
        {
            await _service.DeleteNotificationAsync(id);
            return NoContent();
        }

        [HttpPost("send")]
        [Authorize]
        public async Task<IActionResult> SendNotificationAsync(
            [FromQuery] int userId,
            [FromQuery] NotificationEnum type,
            [FromQuery] string title,
            [FromQuery] string message,
            [FromQuery] string? metadata = null)
        {
            await _service.SendNotificationAsync(userId, type, title, message, metadata);
            return Ok();
        }
    }
}