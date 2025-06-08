namespace UFF.Domain.Dto
{
    public class StoreRatingDto
    {
        public int Id { get; set; }
        public string Store { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }
    }
}