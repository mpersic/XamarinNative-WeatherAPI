using System;
using System.Threading.Tasks;

namespace NNChallenge.Interfaces
{
	public interface IOpenWeatherApiService
	{
        Task<IWeatherForecastVO> GetDailyWeather(string city);
    }
}

