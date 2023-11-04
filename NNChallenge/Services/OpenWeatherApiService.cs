using System;
using Newtonsoft.Json;
using NNChallenge.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using NNChallenge.Data;

namespace NNChallenge.Services
{
    public class OpenWeatherApiService : IOpenWeatherApiService
    {
        private readonly string _apiHost = "https://api.weatherapi.com/v1";
        private readonly string _apiKey = "898147f83a734b7dbaa95705211612";
        private readonly int _days = 3;
        private readonly string _aqi = "no";
        private readonly string _alerts = "no";
        private readonly HttpClient _httpClient;


        public OpenWeatherApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IWeatherForecastVO> GetDailyWeather(string city)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiHost}/forecast.json?key={_apiKey}&q={city}&days={_days}&aqi={_aqi}&alerts={_alerts}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var deserializedContent = JsonConvert.DeserializeObject<Root>(content);

                    var allDays = deserializedContent.forecast.forecastday
                        .SelectMany(forecastday => forecastday.hour)
                        .ToList();

                    var mappedHourForecasts = allDays.Select(hour => new HourWeatherForecastVO
                    {
                        Date = DateTime.Parse(hour.time),
                        TemperatureCelcius = hour.temp_c,
                        TemperatureFahrenheit = hour.temp_f,
                        ForecastPictureURL = "https:" + hour.condition.icon
                    }).ToArray();

                    var weatherForecast = new WeatherForecastVO
                    {
                        City = deserializedContent.location.name,
                        HourForecast = mappedHourForecasts
                    };

                    return weatherForecast;
                }
                else
                {
                    throw new Exception("Failed to retrieve forecast data.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

