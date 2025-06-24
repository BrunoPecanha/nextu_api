using System.Collections.Generic;
using System.Threading.Tasks;
using UFF.Domain.Dto;
using UFF.Domain.Enum;

namespace UFF.Domain.Services
{
    public interface INotificationService
    {
        Task SendNotificationAsync(int userId, NotificationEnum type, string title, string msg, object? metadata = null);
        Task<List<NotificationDto>> GetUserNotificationsAsync(int userId);
        Task MarkAsReadAsync(int notificationId);
        Task MarkAllAsReadAsync(int userId);
        Task DeleteNotificationAsync(int notificationId);
    }
}