using System;

namespace UFF.Domain.Entity
{
    public class FavoriteProfessional : To
    {
        private FavoriteProfessional()
        {
        }

        public int UserId { get; private set; }
        public int ProfessionalId { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public User User { get; private set; } = null;
        public User Professional { get; private set; } = null;

        public FavoriteProfessional(int userId, int professionalId)
        {
            UserId = userId;
            ProfessionalId = professionalId;
        }
    }
}