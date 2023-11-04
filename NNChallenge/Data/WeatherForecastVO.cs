using NNChallenge.Interfaces;

namespace NNChallenge.Data
{
    public class WeatherForecastVO : IWeatherForecastVO
    {
        public string City { get; set; }
        public IHourWeatherForecastVO[] HourForecast { get; set; }
    }
}
