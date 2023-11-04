using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using NNChallenge.Interfaces;
using UIKit;

namespace NNChallenge.iOS
{
    public class TableViewDataSource : UITableViewSource
    {
        public TableViewDataSource(List<HourWeatherForecastVO> forecastVOs)
        {
            this.forecastVOs = forecastVOs;
        }

        public List<HourWeatherForecastVO> forecastVOs { get; set; }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("cell", indexPath);

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, "cell");
            }

            // Access the corresponding HourWeatherForecastVO from the list
            var forecast = forecastVOs[indexPath.Row];

            // Set the label's text to the temperature value from the forecast object
            cell.TextLabel.Text = $"{forecast.TemperatureCelcius}C / {forecast.TemperatureFahrenheit}F"; // Assuming TemperatureC is a property in HourWeatherForecastVO
            cell.DetailTextLabel.Text = forecast.Date.ToString("MMMM d, yyyy"); ; // Replace with your actual subtitle text
            return cell;
        }


        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return forecastVOs.Count();
        }
    }
}
