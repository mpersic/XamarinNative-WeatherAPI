using NNChallenge.Interfaces;
using System;

namespace NNChallenge.Data
{
    public class HourWeatherForecastVO : IHourWeatherForecastVO
    {
        public DateTime Date { get; set; }
        public float TemperatureCelcius { get; set; }
        public float TemperatureFahrenheit { get; set; }
        public string ForecastPictureURL { get; set; }
    }
}
