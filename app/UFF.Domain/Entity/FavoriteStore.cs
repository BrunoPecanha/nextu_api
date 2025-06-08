using System;

namespace UFF.Domain.Entity
{
    public class FavoriteStore : To
    {
        private FavoriteStore()
        {
        }

        public int UserId { get; private set; }
        public int StoreId { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public User User { get; private set; }
        public Store Store { get; private set; }

        public FavoriteStore(int userId, int storeId)
        {
            UserId = userId;
            StoreId = storeId;
        }
    }
}