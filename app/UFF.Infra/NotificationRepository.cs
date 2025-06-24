using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Entity;
using UFF.Domain.Enum;
using UFF.Domain.Repository;
using UFF.Infra.Context;

namespace UFF.Infra
{
    public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {
        private readonly IUffContext _dbContext;

        public NotificationRepository(UffContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SendNotificationAsync(int userId, NotificationEnum type, string titulo, string msg, object? metadata = null)
        {
            var user = await _dbContext.User.FindAsync(userId);
            if (user == null)
                throw new InvalidOperationException($"User with Id {userId} not found");

            var notification = new Notification(
                user,
                type,
                titulo,
                msg,
                metadata != null ? JsonConvert.SerializeObject(metadata) : null
            );

            await _dbContext.Notification.AddAsync(notification);
            _dbContext.SaveChanges();
        }

        public async Task<List<Notification>> GetUserNotificationsAsync(int userId)
        {
            return await _dbContext.Notification
                .Where(n => n.UserId == userId && n.DeletedAt == null)
                .OrderByDescending(n => n.SentAt)
                .ToListAsync();
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            var notification = await _dbContext.Notification.FindAsync(notificationId);
            if (notification != null && notification.DeletedAt == null)
            {
                notification.MarkAsRead();
                _dbContext.SaveChanges();
            }
        }

        public async Task MarkAllAsReadAsync(int userId)
        {
            var notifications = await _dbContext.Notification
                .Where(n => n.UserId == userId && n.DeletedAt == null && !n.IsRead)
                .ToListAsync();

            foreach (var notification in notifications)
            {
                notification.MarkAsRead();
            }

            _dbContext.SaveChanges();
        }

        public async Task DeleteNotificationAsync(int notificationId)
        {
            var notification = await _dbContext.Notification.FindAsync(notificationId);
            if (notification != null && notification.DeletedAt == null)
            {
                notification.Delete();
                _dbContext.SaveChanges();
            }
        }
    }
}
