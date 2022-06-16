using AutoMapper;
using CoinPrediction.DAL.EfDbContext.Entities;
using CoinPrediction.Model;

namespace CoinPrediction.DAL.AutoMapper
{
    public class CryptoMarketProfile: Profile
    {
        /// <summary>
        /// Help mapping the Model-Entity objects.
        /// </summary>
        public CryptoMarketProfile() 
        {
            CreateMap<DbCoin, Coin>().ReverseMap();
            CreateMap<DbUser, User>().ReverseMap();
            CreateMap<DbUserAsset, UserAsset>().ReverseMap();
            CreateMap<DbPairHourBTCUSDT, PairHourBTCUSDT>().ReverseMap();
            CreateMap<DbPairMinuteBTCUSDT, PairMinuteBTCUSDT>().ReverseMap();
            CreateMap<DbSimulationResult, SimulationResult>().ReverseMap();
        }
    }
}
