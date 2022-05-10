namespace CoinPrediction.Model
{
    public class SimulationParams
    {
        public long StartStamp { get; set; }
        public long EndStamp { get; set; }
        public string Frequency { get; set; }
        public double InputMoney { get; set; }
        public double TrainTestSplit { get; set; }
    }
}
