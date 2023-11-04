using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NNChallenge.Interfaces;
using NNChallenge.Services;

namespace NNChallenge.ViewModels
{
	public class ForecastViewModel
	{
		private readonly IOpenWeatherApiService openWeatherApiService;
		public ForecastViewModel()
		{
			openWeatherApiService = new OpenWeatherApiService();
		}

		public async Task<List<HourWeatherForecastVO>> getForecast(string selectedLocation) {
            var items = await openWeatherApiService.GetDailyWeather(selectedLocation);
            return items.HourForecast
				.Select(hourForecast => (HourWeatherForecastVO)hourForecast)
				.ToList();
        }
	}
}

