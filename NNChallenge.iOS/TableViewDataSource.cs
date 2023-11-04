using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using NNChallenge.Data;
using NNChallenge.Interfaces;
using SDWebImage;
using UIKit;

namespace NNChallenge.iOS
{
    public class TableViewDataSource : UITableViewSource
    {
        public TableViewDataSource(List<HourWeatherForecastVO> forecastVOs)
        {
            this.ForecastVOs = forecastVOs;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 60;
        }

        public List<HourWeatherForecastVO> ForecastVOs { get; set; }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("CustomCell") as CustomTableViewCell ?? new CustomTableViewCell("CustomCell");
            var forecast = ForecastVOs[indexPath.Row];

            cell.TitleLabel.Text = $"{forecast.TemperatureCelcius}C / {forecast.TemperatureFahrenheit}F";
            cell.SubtitleLabel.Text = forecast.Date.ToString("MMMM d, yyyy");
            cell.CustomImageView.SetImage(new NSUrl(forecast.ForecastPictureURL), UIImage.FromBundle("placeholder_image.png"));

            return cell;
        }

        public class CustomTableViewCell : UITableViewCell
        {
            public UIImageView CustomImageView { get; set; }
            public UILabel TitleLabel { get; set; }
            public UILabel SubtitleLabel { get; set; }

            public CustomTableViewCell(string reuseIdentifier) : base(UITableViewCellStyle.Default, reuseIdentifier)
            {
                CustomImageView = new UIImageView(new CGRect(10, 10, 40, 40));
                TitleLabel = new UILabel(new CGRect(60, 10, 200, 20));
                SubtitleLabel = new UILabel(new CGRect(60, 30, 200, 20));

                ContentView.AddSubviews(CustomImageView, TitleLabel, SubtitleLabel);
            }
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return ForecastVOs.Count();
        }
    }
}
