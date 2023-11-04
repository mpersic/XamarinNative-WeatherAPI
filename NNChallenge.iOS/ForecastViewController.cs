using System;
using System.Drawing.Drawing2D;
using System.Linq;
using NNChallenge.Interfaces;
using NNChallenge.ViewModels;
using UIKit;

namespace NNChallenge.iOS
{
    public partial class ForecastViewController : UIViewController
    {

        public string SelectedLocation { get; set; }

        public ForecastViewController() : base("ForecastViewController", null)
        {
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            UITableView tableView = (UITableView)View.ViewWithTag(1);
            if (!string.IsNullOrEmpty(SelectedLocation))
            {
                var viewModel = new ForecastViewModel();
                var data = await viewModel.getForecast(SelectedLocation);
                Title = SelectedLocation;
                tableView.Source = new TableViewDataSource(data);
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }
}

