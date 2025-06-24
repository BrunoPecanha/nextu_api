using System;
using UFF.Domain.Enum;

namespace UFF.Domain.Entity
{
    public class Notification : To
    {
        private Notification() { }

        public Notification(User user, NotificationEnum type, string title, string message, string? metadata = null)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            UserId = user.Id;
            Type = type;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Metadata = metadata;
            IsRead = false;
            SentAt = DateTimeOffset.UtcNow;
        }

        public int UserId { get; private set; }
        public User User { get; private set; } = null!;

        public NotificationEnum Type { get; private set; }

        public string Title { get; private set; } = null!;

        public string Message { get; private set; } = null!;

        public bool IsRead { get; private set; }

        public DateTimeOffset SentAt { get; private set; }

        public string? Metadata { get; private set; }

        public DateTimeOffset? DeletedAt { get; private set; }

        public void MarkAsRead()
        {
            IsRead = true;
        }

        public void Delete()
        {
            DeletedAt = DateTimeOffset.UtcNow;
        }

        public void UpdateMetadata(string metadata)
        {
            Metadata = metadata;
        }
    }
}