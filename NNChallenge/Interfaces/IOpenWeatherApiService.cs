using System;
using System.Threading.Tasks;

namespace NNChallenge.Interfaces
{
	public interface IOpenWeatherApiService
	{
        Task<IWeatherForcastVO> GetDailyWeather(string city);
    }
}

