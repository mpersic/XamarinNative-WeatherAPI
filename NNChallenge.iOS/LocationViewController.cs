using System;
using NNChallenge.Constants;
using NNChallenge.iOS.ViewModel;
using UIKit;

namespace NNChallenge.iOS
{
    public partial class LocationViewController : UIViewController
    {
        public LocationViewController() : base("LocationViewController", null)
        {
        }       

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Location";
            _submitButton.TitleLabel.Text = "Submit";
            _contentLabel.Text = "Select your location.";
            _submitButton.TouchUpInside += SubmitButtonTouchUpInside;

            _picker.Model = new LocationPickerModel(LocationConstants.LOCATIONS);
        }

        private void SubmitButtonTouchUpInside(object sender, EventArgs e)
        {
            var selected = _picker.SelectedRowInComponent(0);
            var forecastView = new ForecastViewController();
            this.NavigationController.PushViewController(forecastView, true);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

