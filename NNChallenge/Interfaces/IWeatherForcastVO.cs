using System;
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
}
