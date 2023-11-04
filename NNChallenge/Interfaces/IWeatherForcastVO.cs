using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NNChallenge.Interfaces;

namespace NNChallenge.Interfaces
{
    public interface IWeatherForcastVO
    {
        /// <summary>
        /// Name of the city
        /// </summary>
        string City { get; }
        /// <summary>
        /// Array of weather forecast etries
        /// </summary>
        IHourWeatherForecastVO[] HourForecast { get; }
    }

    public class WeatherForcastVO : IWeatherForcastVO
    {
        public string City { get; set; }
        public IHourWeatherForecastVO[] HourForecast { get; set; }
    }


    public interface IHourWeatherForecastVO
    {
        /// <summary>
        /// date of forecast
        /// </summary>
        DateTime Date { get; }
        /// <summary>
        /// temerature in Celcius
        /// </summary>
        float TemperatureCelcius { get; }
        /// <summary>
        /// Temperture in Fahrenheit
        /// </summary>
        float TemperatureFahrenheit { get; }
        /// <summary>
        /// url for picture
        /// </summary>
        string ForecastPictureURL { get; }
    }

    public class HourWeatherForecastVO : IHourWeatherForecastVO
    {
        public DateTime Date { get; set; }
        public float TemperatureCelcius { get; set; }
        public float TemperatureFahrenheit { get; set; }
        public string ForecastPictureURL { get; set; }
    }

        public class OpenWeatherApi
    {
        private string _apiHost = "https://api.weatherapi.com/v1";
        private string _apiKey = "898147f83a734b7dbaa95705211612";
        private string _city;
        private int _days;
        private string _aqi;
        private string _alerts;
        private HttpClient _httpClient;


        public OpenWeatherApi(string city, int days = 3, string aqi = "no", string alerts = "no")
        {
            _httpClient = new HttpClient();
            _city = city;
            _days = days;
            _aqi = aqi;
            _alerts = alerts;
        }

        //public async Task<List<WeatherForecastVO>> GetCurrentWeather()
        //{
        //    try
        //    {
        //        var response = await _httpClient.GetAsync($"{_apiHost}/current.json?key={_apiKey}&q={_city}");
        //        //var response = await _httpClient.GetAsync("https://api.weatherapi.com/v1/forecast.json?key=898147f83a734b7dbaa95705211612&q=Berlin&days=3&aqi=no&alerts=no");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var content = await response.Content.ReadAsStringAsync();
        //            var items = new List<WeatherForecastVO>();
        //            var deserializadContent = JsonConvert.DeserializeObject<WeatherForecastVO>(content);
        //            items.Add(deserializadContent);
        //            return items;
        //        }
        //        else
        //        {
        //            throw new Exception();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public async Task<IWeatherForcastVO> GetDailyWeather()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiHost}/forecast.json?key={_apiKey}&q={_city}&days={_days}&aqi={_aqi}&alerts={_alerts}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var deserializedContent = JsonConvert.DeserializeObject<Root>(content);

                    // Flatten the list of forecastday items
                    var allDays = deserializedContent.forecast.forecastday
                        .SelectMany(forecastday => forecastday.hour) // Assumes the "hour" property contains the 24 days
                        .ToList();

                    // Map the data to IHourWeatherForecastVO
                    var mappedHourForecasts = allDays.Select(hour => new HourWeatherForecastVO
                    {
                        Date = DateTime.Parse(hour.time),
                        TemperatureCelcius = hour.temp_c,
                        TemperatureFahrenheit = hour.temp_f,
                        ForecastPictureURL = "https:" + hour.condition.icon
                    }).ToArray();

                    // Create the IWeatherForcastVO object
                    var weatherForecast = new WeatherForcastVO
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


        //public async Task<List<Hour>> GetDailyWeather()
        //{
        //    try
        //    {
        //        var response = await _httpClient.GetAsync($"{_apiHost}/forecast.json?key={_apiKey}&q={_city}&days={_days}&aqi={_aqi}&alerts={_alerts}");

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var content = await response.Content.ReadAsStringAsync();
        //            var deserializadContent = JsonConvert.DeserializeObject<Root>(content);
        //            // Flatten the list of forecastday items
        //            var allDays = deserializadContent.forecast.forecastday
        //                .SelectMany(forecastday => forecastday.hour) // Assumes the "hour" property contains the 24 days
        //                .ToList();

        //            return allDays;
        //        }
        //        else
        //        {
        //            throw new Exception();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
           
        //}

        ~OpenWeatherApi()
        {
            GC.Collect();
        }
    }
}