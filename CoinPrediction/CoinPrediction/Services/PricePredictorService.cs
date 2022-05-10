using CoinPrediction.Model;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CoinPrediction.Services
{
    public class PricePredictorService: IPricePredictorService
    {
        private readonly IConfiguration configuration;
        private readonly ProcessStartInfo psi;
        private readonly string script;
        private const string trainedFinish = "Model trained successfully!";
        private const double minProfitPercent = 0.01;
        private const double maxLosePercent = 0.007;

        public PricePredictorService(IConfiguration configuration) 
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            psi = new ProcessStartInfo();
            psi.FileName = configuration.GetValue<string>("PythonExePath");
            script = configuration.GetValue<string>("ScriptPath");

            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
        }

        public SimulationResult PredictBTCUSDT(SimulationParams parameters) 
        {
            var result = TrainModelAndGetPredictionResults(parameters);
            var formatted = ProcessPredictions(result.Predictions);

            var orders = MakeBuySellOrders(formatted, result.Valids);

            var simulationResult = EvalueateOrders(orders, result.Valids, parameters.InputMoney);
            
            return simulationResult;
        }

        private PredictionResult TrainModelAndGetPredictionResults(SimulationParams parameters) 
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            var start = dateTime.AddMilliseconds(parameters.StartStamp).ToLocalTime();
            var end = dateTime.AddMilliseconds(parameters.EndStamp).ToLocalTime();
            var frequency = parameters.Frequency;
            var trainTestSplit = parameters.TrainTestSplit;

            psi.ArgumentList.Add(script);
            psi.ArgumentList.Add(start.ToString());
            psi.ArgumentList.Add(end.ToString());
            psi.ArgumentList.Add(frequency);
            psi.ArgumentList.Add(trainTestSplit.ToString());
            psi.ArgumentList.Add(trainedFinish);

            var jsonResult = "";

            using (var process = Process.Start(psi))
            {
                while (!process.StandardOutput.EndOfStream)
                {
                    var line = process.StandardOutput.ReadLine();

                    if (line == trainedFinish)
                        break;
                    Console.WriteLine(line);
                }
                jsonResult = process.StandardOutput.ReadToEnd();
            };

            var result = JsonConvert.DeserializeObject<PredictionResult>(jsonResult);

            return result;
        }

        private List<List<double>> ProcessPredictions(List<List<double>> resultList) 
        {
            var formatted = new List<List<double>>();
            var innerListsLength = resultList[0].Count;

            for (int i = 0; i < innerListsLength; i++)
            {
                var innerList = new List<double>();

                foreach (var item in resultList)
                {
                    innerList.Add(item[i]);
                }

                formatted.Add(innerList);
            }

            formatted.RemoveRange(formatted.Count - 1 - resultList.Count, resultList.Count);
            formatted.RemoveAt(0);            

            return formatted;

        }

        private List<OrderAction> MakeBuySellOrders(List<List<double>> formatted, List<double> valids) 
        {
            var orders = new List<OrderAction>();

            var maxForEachPeriod = formatted.Select(x => x.Max()).ToList();
            var minForEachPeriod = formatted.Select(x => x.Min()).ToList();        

            for (int i = 0; i < formatted.Count; i++)
            {
                var currentPrice = valids[i];
                var topDiffPercentage = 1 - (maxForEachPeriod[i] / currentPrice);
                var bottomDiffPercentage = 1 - (currentPrice / minForEachPeriod[i]);

                if (topDiffPercentage > minProfitPercent && bottomDiffPercentage < maxLosePercent)
                {
                    orders.Add(OrderAction.TRY_BUY);
                }
                else if (topDiffPercentage < bottomDiffPercentage && bottomDiffPercentage > maxLosePercent)
                {
                    orders.Add(OrderAction.TRY_SELL);
                }
                else 
                {
                    orders.Add(OrderAction.PASSIVE);
                }
            }

            return orders;
        }

        private SimulationResult EvalueateOrders(List<OrderAction> orders, List<double> valids, double inputMoney) 
        {
            var usdt = inputMoney;
            var coin = 0.0;

            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i] == OrderAction.TRY_BUY && usdt > 0)
                {
                    coin = usdt / valids[i];
                    usdt = 0;
                }
                else if (orders[i] == OrderAction.TRY_SELL && coin > 0) 
                {
                    usdt = coin * valids[i];
                    coin = 0;
                }
            }

            var finalBalance = usdt + coin * valids[orders.Count - 1];

            return new SimulationResult() { ProfitUSDT = finalBalance - inputMoney };
        }
    }
}
