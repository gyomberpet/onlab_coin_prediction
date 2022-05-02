namespace CoinPrediction.Model
{
    public class PairHourBTCUSDT
    {
        public int Id { get; init; }
        public DateTime Date { get; init; }
        public string Symbol { get; init; }
        public double Open { get; init; }
        public double High { get; init; }
        public double Low { get; init; }
        public double Close { get; init; }
        public double VolumeBTC { get; init; }
        public double VolumeUSDT { get; init; }
        public int? TradeCount { get; init; }
    }
}
