namespace UFF.Domain.Entity
{
    public class StoreRating : To
    {
        private StoreRating()
        {
        }

        public int StoreId { get; set; }
        public Store Store { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int? ProfessionalId { get; set; }
        public User Professional { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }

        public StoreRating(int storeId, int userId, int professionalId, int score, string comment)
        {
            StoreId = storeId;
            UserId = userId;
            ProfessionalId = professionalId;
            Score = score;
            Comment = comment;
        }
    }
}