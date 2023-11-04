using System;
using System.Drawing.Drawing2D;
using System.Linq;
using NNChallenge.Interfaces;
using UIKit;

namespace NNChallenge.iOS
{
    public partial class ForecastViewController : UIViewController
    {

        public string SelectedCity { get; set; }

        public ForecastViewController() : base("ForecastViewController", null)
        {
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Forecast";
            UITableView tableView = (UITableView)View.ViewWithTag(1);
            tableView.RegisterClassForCellReuse(typeof(UITableViewCell), "cell");
            if (!string.IsNullOrEmpty(SelectedCity))
            {
                var vs = new NNChallenge.Interfaces.OpenWeatherApi(city: SelectedCity);
                var items = await vs.GetDailyWeather();
                var data = items.HourForecast.Select(hourForecast => (HourWeatherForecastVO)hourForecast).ToList();

                tableView.Source = new TableViewDataSource(data);
                // Do something with the SelectedCity
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

