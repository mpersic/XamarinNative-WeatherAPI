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
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

