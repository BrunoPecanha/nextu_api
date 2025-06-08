namespace UFF.Domain.Commands.Store
{
    public class StoreRatingCommand
    {
        public int StoreId { get; set; }
        public int UserId { get; set; }
        public int ProfessionalId { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }
    }
}