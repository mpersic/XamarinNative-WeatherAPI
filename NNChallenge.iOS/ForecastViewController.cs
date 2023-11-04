using System;

using UIKit;

namespace NNChallenge.iOS
{
    public partial class ForecastViewController : UIViewController
    {
        public ForecastViewController() : base("ForecastViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Forecast";
            UITableView tableView = (UITableView)View.ViewWithTag(1);
            tableView.RegisterClassForCellReuse(typeof(UITableViewCell), "cell");
            tableView.Source = new TableViewDataSource();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

