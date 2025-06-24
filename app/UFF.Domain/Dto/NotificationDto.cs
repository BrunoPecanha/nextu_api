using System;

namespace UFF.Domain.Dto
{
    public class NotificationDto
    {
        public Guid Id { get; set; }

        public string Type { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Message { get; set; } = null!;

        public bool IsRead { get; set; }

        public DateTimeOffset SentAt { get; set; }

        public string? Metadata { get; set; }
    }
}