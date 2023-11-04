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

    public interface IHourWeatherForecastVO
    {
        /// <summary>
        /// date of forecast
        /// </summary>
        DateTime Date { get; }
        /// <summary>
        /// temerature in Celcius
        /// </summary>
        float TeperatureCelcius { get; }
        /// <summary>
        /// Temperture in Fahrenheit
        /// </summary>
        float TeperatureFahrenheit { get; }
        /// <summary>
        /// url for picture
        /// </summary>
        string ForecastPitureURL { get; }
    }

    public class WeatherForecast : IWeatherForcastVO
    {
        // <summary>
        /// Name of the city
        /// </summary>
        public string City { get; }
        /// <summary>
        /// Array of weather forecast etries
        /// </summary>
        public IHourWeatherForecastVO[] HourForecast { get; }
    }

        public class WeatherForecastVO : IWeatherForcastVO
        {
            [JsonProperty("location")]
            public Location Location { get; set; }

            [JsonProperty("current")]
            public CurrentWeather CurrentWeather { get; set; }

            [JsonProperty("forecast")]
            public Forecast Forecast { get; set; }

            public string City => Location.Name;
            public IHourWeatherForecastVO[] HourForecast => Forecast.Forecastday[0].Hour.Select(hour => (IHourWeatherForecastVO)hour).ToArray();
        }

        public class Location
        {
            public string Name { get; set; }
            public string Region { get; set; }
            public string Country { get; set; }
            public double Lat { get; set; }
            public double Lon { get; set; }
            [JsonProperty("tz_id")]
            public string TimeZoneId { get; set; }
            [JsonProperty("localtime")]
            public string LocalTime { get; set; }
        }

        public class CurrentWeather
        {
            [JsonProperty("last_updated_epoch")]
            public long LastUpdatedEpoch { get; set; }
            [JsonProperty("last_updated")]
            public string LastUpdated { get; set; }
            [JsonProperty("temp_c")]
            public float TemperatureCelsius { get; set; }
            [JsonProperty("temp_f")]
            public float TemperatureFahrenheit { get; set; }
            [JsonProperty("is_day")]
            public int IsDay { get; set; }
            //public Condition Condition { get; set; }
            [JsonProperty("wind_mph")]
            public float WindMph { get; set; }
            [JsonProperty("wind_kph")]
            public float WindKph { get; set; }
            [JsonProperty("wind_degree")]
            public int WindDegree { get; set; }
            [JsonProperty("wind_dir")]
            public string WindDirection { get; set; }
            [JsonProperty("pressure_mb")]
            public float PressureMb { get; set; }
            [JsonProperty("pressure_in")]
            public float PressureIn { get; set; }
            [JsonProperty("precip_mm")]
            public float PrecipitationMm { get; set; }
            [JsonProperty("precip_in")]
            public float PrecipitationIn { get; set; }
            public int Humidity { get; set; }
            public int Cloud { get; set; }
            [JsonProperty("feelslike_c")]
            public float FeelsLikeCelsius { get; set; }
            [JsonProperty("feelslike_f")]
            public float FeelsLikeFahrenheit { get; set; }
            [JsonProperty("vis_km")]
            public float VisibilityKm { get; set; }
            [JsonProperty("vis_miles")]
            public float VisibilityMiles { get; set; }
            public float UV { get; set; }
            [JsonProperty("gust_mph")]
            public float GustMph { get; set; }
            [JsonProperty("gust_kph")]
            public float GustKph { get; set; }
        }

        public class Forecast
        {
            public List<Forecastday> Forecastday { get; set; }
        }

        public class Forecastday
        {
            public string Date { get; set; }
            [JsonProperty("date_epoch")]
            public long DateEpoch { get; set; }
            public Day Day { get; set; }
            public Astro Astro { get; set; }
            public List<Hour> Hour { get; set; }
        }

        public class Day
        {
            [JsonProperty("maxtemp_c")]
            public float MaxTempCelsius { get; set; }
            [JsonProperty("maxtemp_f")]
            public float MaxTempFahrenheit { get; set; }
            [JsonProperty("mintemp_c")]
            public float MinTempCelsius { get; set; }
            [JsonProperty("mintemp_f")]
            public float MinTempFahrenheit { get; set; }
            [JsonProperty("avgtemp_c")]
            public float AvgTempCelsius { get; set; }
            [JsonProperty("avgtemp_f")]
            public float AvgTempFahrenheit { get; set; }
            [JsonProperty("maxwind_mph")]
            public float MaxWindMph { get; set; }
            [JsonProperty("maxwind_kph")]
            public float MaxWindKph { get; set; }
            [JsonProperty("totalprecip_mm")]
            public float TotalPrecipitationMm { get; set; }
            [JsonProperty("totalprecip_in")]
            public float TotalPrecipitationIn { get; set; }
            [JsonProperty("totalsnow_cm")]
            public float TotalSnowCm { get; set; }
            [JsonProperty("avgvis_km")]
            public float AvgVisibilityKm { get; set; }
            [JsonProperty("avgvis_miles")]
            public float AvgVisibilityMiles { get; set; }
            [JsonProperty("avghumidity")]
            public float AvgHumidity { get; set; }
            [JsonProperty("daily_will_it_rain")]
            public int DailyWillItRain { get; set; }
            [JsonProperty("daily_chance_of_rain")]
            public int DailyChanceOfRain { get; set; }
            [JsonProperty("daily_will_it_snow")]
            public int DailyWillItSnow { get; set; }
            [JsonProperty("daily_chance_of_snow")]
            public int DailyChanceOfSnow { get; set; }
           // public Condition Condition { get; set; }
            public float UV { get; set; }
        }

        public class Astro
        {
            [JsonProperty("sunrise")]
            public string Sunrise { get; set; }
            [JsonProperty("sunset")]
            public string Sunset { get; set; }
            [JsonProperty("moonrise")]
            public string Moonrise { get; set; }
            [JsonProperty("moonset")]
            public string Moonset { get; set; }
            [JsonProperty("moon_phase")]
            public string MoonPhase { get; set; }
            [JsonProperty("moon_illumination")]
            public int MoonIllumination { get; set; }
            [JsonProperty("is_moon_up")]
            public int IsMoonUp { get; set; }
            [JsonProperty("is_sun_up")]
            public int IsSunUp { get; set; }
        }

    public class Hour
        {
            [JsonProperty("time")]
            public string Time { get; set; }
            [JsonProperty("temp_c")]
            public float TemperatureCelsius { get; set; }
            [JsonProperty("temp_f")]
            public float TemperatureFahrenheit { get; set; }
            [JsonProperty("is_day")]
            public int IsDay { get; set; }
            //public Condition Condition { get; set; }
            [JsonProperty("icon")]
            public string Icon { get; set; }
            [JsonProperty("wind_mph")]
            public float WindMph { get; set; }
            [JsonProperty("wind_kph")]
            public float WindKph { get; set; }
            [JsonProperty("wind_degree")]
            public int WindDegree { get; set; }
            [JsonProperty("wind_dir")]
            public string WindDirection { get; set; }
            [JsonProperty("pressure_mb")]
            public float PressureMb { get; set; }
            [JsonProperty("pressure_in")]
            public float PressureIn { get; set; }
            [JsonProperty("precip_mm")]
            public float PrecipitationMm { get; set; }
            [JsonProperty("precip_in")]
            public float PrecipitationIn { get; set; }
            public int Humidity { get; set; }
            public int Cloud { get; set; }
            [JsonProperty("feelslike_c")]
            public float FeelsLikeCelsius { get; set; }
            [JsonProperty("feelslike_f")]
            public float FeelsLikeFahrenheit { get; set; }
            [JsonProperty("windchill_c")]
            public float WindChillCelsius { get; set; }
            [JsonProperty("windchill_f")]
            public float WindChillFahrenheit { get; set; }
            [JsonProperty("heatindex_c")]
            public float HeatIndexCelsius { get; set; }
            [JsonProperty("heatindex_f")]
            public float HeatIndexFahrenheit { get; set; }
            [JsonProperty("dewpoint_c")]
            public float DewPointCelsius { get; set; }
            [JsonProperty("dewpoint_f")]
            public float DewPointFahrenheit { get; set; }
            [JsonProperty("will_it_rain")]
            public int WillItRain { get; set; }
            [JsonProperty("chance_of_rain")]
            public int ChanceOfRain { get; set; }
            [JsonProperty("will_it_snow")]
            public int WillItSnow { get; set; }
            [JsonProperty("chance_of_snow")]
            public int ChanceOfSnow { get; set; }
            [JsonProperty("vis_km")]
            public float VisibilityKm { get; set; }
            [JsonProperty("vis_miles")]
            public float VisibilityMiles { get; set; }
            [JsonProperty("uv")]
            public float UV { get; set; }
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

        public async Task<List<WeatherForecastVO>> GetCurrentWeather()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiHost}/current.json?key={_apiKey}&q={_city}");
                //var response = await _httpClient.GetAsync("https://api.weatherapi.com/v1/forecast.json?key=898147f83a734b7dbaa95705211612&q=Berlin&days=3&aqi=no&alerts=no");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var items = new List<WeatherForecastVO>();
                    var deserializadContent = JsonConvert.DeserializeObject<WeatherForecastVO>(content);
                    items.Add(deserializadContent);
                    return items;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task GetDailyWeather()
        {
            var response = await _httpClient.GetAsync($"{_apiHost}/forecast.json?key={_apiKey}&q={_city}&days={_days}&aqi={_aqi}&alerts={_alerts}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return;
                //return JsonConvert.DeserializeObject<DailyWeatherDataModel.RootObject>(content);
            }
            else
            {
                throw new Exception();
            }
        }

        ~OpenWeatherApi()
        {
            GC.Collect();
        }
}
}