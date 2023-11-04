using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NNChallenge.Data;
using NNChallenge.Interfaces;
using NNChallenge.Services;

namespace NNChallenge.ViewModels
{
	public class ForecastViewModel
	{
		private readonly IOpenWeatherApiService _openWeatherApiService;
		private IWeatherForecastVO weatherForecastVO;

		public ForecastViewModel(IOpenWeatherApiService openWeatherApiService)
		{
			_openWeatherApiService = openWeatherApiService;
		}

		public async Task<IWeatherForecastVO> GetWeatherForecast(string selectedLocation) {
			//I would normally return HourWeatherForecasthere and set page title according
			//to passed parameter but due to challenge requirements i will break the logic
			//in multiple steps
            weatherForecastVO = await _openWeatherApiService.GetWeatherForecast(selectedLocation);
			return weatherForecastVO;
        }

		public string GetLocationName()
		{
			return weatherForecastVO.City;
		}

        public List<HourWeatherForecastVO> GetHourlyWeatherForSelectedDays()
        {
            return weatherForecastVO.HourForecast
                .Select(hourForecast => (HourWeatherForecastVO)hourForecast)
                .ToList();
        }
    }
}

