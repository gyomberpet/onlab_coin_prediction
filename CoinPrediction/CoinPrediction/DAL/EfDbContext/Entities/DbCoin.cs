namespace CoinPrediction.DAL.EfDbContext.Entities
{
    public class DbCoin
    {
        public Guid Id { get; set; }

        public string? CoinId { get; set; }

        public string? Name { get; set; }

        public string? Symbol { get; set; } 
    }
}
