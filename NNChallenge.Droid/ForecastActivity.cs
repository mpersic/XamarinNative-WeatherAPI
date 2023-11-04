
using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using NNChallenge.Data;
using NNChallenge.Interfaces;
using NNChallenge.ViewModels;
using Square.Picasso;
using Xamarin.Essentials;

namespace NNChallenge.Droid
{
    [Activity(Label = "ForecastActivity")]
    public class ForecastActivity : Activity
    {
        private RecyclerView recyclerView;
        private WeatherForecastAdapter adapter;
        private ForecastViewModel viewModel;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_forecast);

            //ActionBar.Title = "Weather Forecast"; // You can change the title to whatever you want

            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this));
            adapter = new WeatherForecastAdapter(this);
            recyclerView.SetAdapter(adapter);

            viewModel = new ForecastViewModel();

            if (Intent.HasExtra("SelectedLocation"))
            {
                var selectedLocation = Intent.GetStringExtra("SelectedLocation");
                if (Helper.IsInternetConnectionAvailable(this))
                {
                    try
                    {
                        await viewModel.GetDailyWeather(selectedLocation);
                        var selectedDaysWeatherData = viewModel.GetHourlyWeatherForSelectedDays();
                        ActionBar.Title = viewModel.GetLocationName(); // You can change the title to whatever you want

                        if (selectedDaysWeatherData.Count == 0)
                        {
                            Helper.ShowToast(this, "No items in the list.");
                        }
                        else
                        {
                            adapter.SetData(selectedDaysWeatherData);
                        }
                    }
                    catch (Exception ex)
                    {
                        Helper.ShowToast(this, "An error occurred: " + ex.Message);
                    }
                }
                else
                {
                    Helper.ShowToast(this, "No internet connection.");
                }
            }
        }
    }

    public class WeatherForecastAdapter : RecyclerView.Adapter
    {
        private List<HourWeatherForecastVO> weatherForecastList;
        private readonly Context context;

        public WeatherForecastAdapter(Context context)
        {
            this.context = context;
            weatherForecastList = new List<HourWeatherForecastVO>();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.weather_forecast_item, parent, false);
            WeatherForecastViewHolder viewHolder = new WeatherForecastViewHolder(view);
            return viewHolder;
        }


        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is WeatherForecastViewHolder weatherHolder)
            {
                HourWeatherForecastVO weatherForecast = weatherForecastList[position];
                weatherHolder.Bind(weatherForecast);
            }
        }

        public override int ItemCount => weatherForecastList.Count;

        public void SetData(List<HourWeatherForecastVO> data)
        {
            weatherForecastList = data;
            NotifyDataSetChanged();
        }

        public class WeatherForecastViewHolder : RecyclerView.ViewHolder
        {
            private readonly TextView dateTextView;
            private readonly TextView temperatureTextView;
            private readonly ImageView weatherImageView;

            public WeatherForecastViewHolder(View itemView) : base(itemView)
            {
                dateTextView = itemView.FindViewById<TextView>(Resource.Id.dateTextView);
                temperatureTextView = itemView.FindViewById<TextView>(Resource.Id.temperatureTextView);
                weatherImageView = itemView.FindViewById<ImageView>(Resource.Id.weatherImageView);
            }

            public void Bind(HourWeatherForecastVO weatherForecast)
            {
                temperatureTextView.Text = $"{weatherForecast.TemperatureCelcius}C / {weatherForecast.TemperatureFahrenheit}F";
                dateTextView.Text = weatherForecast.Date.ToString("MMMM d, yyyy");
                Picasso.With(ItemView.Context)
                    .Load(weatherForecast.ForecastPictureURL)
                    .Into(weatherImageView);
            }
        }
    }
}
