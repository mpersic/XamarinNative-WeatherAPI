using System;
using System.Drawing.Drawing2D;
using System.Linq;
using CoreFoundation;
using NNChallenge.Interfaces;
using NNChallenge.ViewModels;
using UIKit;

namespace NNChallenge.iOS
{
    public partial class ForecastViewController : UIViewController
    {

        public string SelectedLocation { get; set; }
        private ForecastViewModel viewModel;

        public ForecastViewController() : base("ForecastViewController", null)
        {
            viewModel = new ForecastViewModel();
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            UITableView tableView = (UITableView)View.ViewWithTag(1);
            if (!string.IsNullOrEmpty(SelectedLocation))
            {
                if (Helper.IsInternetConnectionAvailable())
                {
                    try
                    {
                        await viewModel.GetDailyWeather(SelectedLocation);
                        var selectedDaysWeatherData = viewModel.GetHourlyWeatherForSelectedDays();
                        Title = viewModel.GetLocationName();


                        if (selectedDaysWeatherData.Count == 0)
                        {
                            ShowToast("No items in the list.");
                        }
                        else
                        {
                            tableView.Source = new TableViewDataSource(selectedDaysWeatherData);
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowToast("An error occurred: " + ex.Message);
                    }

                }
                else
                {
                    ShowToast("No internet connection.");
                }

            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        private void ShowToast(string message)
        {
            //@objc constant
            var NSEC_PER_SEC = 1000000000;
            var alertController = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            PresentViewController(alertController, true, null);

            DispatchQueue.MainQueue.DispatchAfter(new DispatchTime(DispatchTime.Now, 2 * NSEC_PER_SEC), () =>
            {
                alertController.DismissViewController(true, null);
            });
        }
    }
}

