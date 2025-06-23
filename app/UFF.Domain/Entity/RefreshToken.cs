using System;
using System.Security.Cryptography;

namespace UFF.Domain.Entity
{
    public class RefreshToken : To
    {
        private RefreshToken()
        {
        }

        public int UserId { get; set; }
        public User User { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsRevoked { get; set; }

        public RefreshToken(User user)
        {
            UserId = user.Id;
            Token = GenerateRefreshToken();
            ExpiryDate = DateTime.UtcNow.AddDays(7);
        }

        public void RevokeToken()
        {
            IsRevoked= true;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}