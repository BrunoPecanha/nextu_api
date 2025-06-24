using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using UFF.Domain.Dto;
using UFF.Domain.Enum;
using UFF.Domain.Repository;
using UFF.Domain.Services;
using UFF.Service.Hubs;

namespace UFF.Service
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(INotificationRepository notificationRepository, IMapper mapper, IHubContext<NotificationHub> hubContext)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        public async Task SendNotificationAsync(int userId, NotificationEnum type, string title, string msg, object? metadata = null)
        {
            // Primeiro salva no banco
            await _notificationRepository.SendNotificationAsync(
                userId: userId,
                type: type,
                title: title,
                msg: msg,
                metadata: metadata
            );

            var notificationPayload = new
            {
                UserId = userId,
                Type = type.ToString(),
                Title = title,
                Message = msg,
                Metadata = metadata
            };

            await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveNotification", notificationPayload);
        }

        public async Task<List<NotificationDto>> GetUserNotificationsAsync(int userId)
        {
            var notifications = await _notificationRepository.GetUserNotificationsAsync(userId);
            return _mapper.Map<List<NotificationDto>>(notifications);
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            await _notificationRepository.MarkAsReadAsync(notificationId);
        }

        public async Task MarkAllAsReadAsync(int userId)
        {
            await _notificationRepository.MarkAllAsReadAsync(userId);
        }

        public async Task DeleteNotificationAsync(int notificationId)
        {
            await _notificationRepository.DeleteNotificationAsync(notificationId);
        }
    }
}
