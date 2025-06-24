using System.Collections.Generic;
using System.Threading.Tasks;
using UFF.Domain.Entity;
using UFF.Domain.Enum;

namespace UFF.Domain.Repository
{
    public interface INotificationRepository : IRepositoryBase<Notification> {
        Task SendNotificationAsync(int userId, NotificationEnum type, string title, string msg, object? metadata = null);
        Task<List<Notification>> GetUserNotificationsAsync(int userId);
        Task MarkAsReadAsync(int notificationId);
        Task MarkAllAsReadAsync(int userId);
        Task DeleteNotificationAsync(int notificationId);
    }
}