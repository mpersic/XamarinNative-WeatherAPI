using System;
using System.Drawing.Drawing2D;
using System.Linq;
using Autofac;
using CoreFoundation;
using NNChallenge.DI;
using NNChallenge.Interfaces;
using NNChallenge.ViewModels;
using UIKit;

namespace NNChallenge.iOS
{
    public partial class ForecastViewController : UIViewController
    {
        private readonly ForecastViewModel _viewModel;
        private UITableView tableView;

        public ForecastViewController() : base("ForecastViewController", null)
        {
            _viewModel = DIContainer.Instance.Resolve<ForecastViewModel>();
        }

        public string SelectedLocation { get; set; }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            tableView = new UITableView
            {
                Frame = View.Bounds,
                AutoresizingMask = UIViewAutoresizing.FlexibleHeight
            };

            View.AddSubview(tableView);

            NSLayoutConstraint.ActivateConstraints(new[]
           {
                tableView.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor),
                tableView.LeadingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.LeadingAnchor),
                tableView.TrailingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TrailingAnchor),
                tableView.BottomAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.BottomAnchor)
            });

            if (!string.IsNullOrEmpty(SelectedLocation))
            {
                if (Helper.IsInternetConnectionAvailable())
                {
                    try
                    {
                        await _viewModel.GetDailyWeather(SelectedLocation);
                        var selectedDaysWeatherData = _viewModel.GetHourlyWeatherForSelectedDays();
                        Title = _viewModel.GetLocationName();

                        if (selectedDaysWeatherData.Count == 0)
                        {
                            ShowToast("No items in the list.");
                        }
                        else
                        {
                            tableView.Source = new TableViewDataSource(selectedDaysWeatherData);
                            tableView.ReloadData();
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

