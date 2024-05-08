namespace CoinPrediction.DAL.EfDbContext.Entities
{
    public class DbPairHourBTCUSDT
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Symbol { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double VolumeBTC { get; set; }
        public double VolumeUSDT { get; set; }
        public int? TradeCount { get; set; }

    }
}
