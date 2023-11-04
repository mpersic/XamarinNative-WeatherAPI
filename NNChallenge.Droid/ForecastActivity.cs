
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using NNChallenge.Interfaces;
using Square.Picasso;
using Xamarin.Essentials;

namespace NNChallenge.Droid
{
    [Activity(Label = "ForecastActivity")]
    public class ForecastActivity : Activity
    {
        private RecyclerView recyclerView;
        private WeatherForecastAdapter adapter;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_forecast);
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this));

            adapter = new WeatherForecastAdapter(this);
            recyclerView.SetAdapter(adapter);

            if (Intent.HasExtra("SelectedLocation"))
            {
                string selectedLocation = Intent.GetStringExtra("SelectedLocation");
                var vs = new NNChallenge.Interfaces.OpenWeatherApi(city: selectedLocation);
                var items = await vs.GetDailyWeather();
                var data = items.HourForecast.Select(hourForecast => (HourWeatherForecastVO)hourForecast).ToList();
                adapter.SetData(data);
                // Now you can use the selectedLocation in your ForecastActivity.
            }
            // Handle the case where the extra is not found if needed.
        }

    }

    public class WeatherForecastAdapter : RecyclerView.Adapter
    {
        private List<HourWeatherForecastVO> weatherForecastList;
        private Context context;

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
            private TextView cityTextView;
            private TextView temperatureTextView;
            private ImageView weatherImageView;

            public WeatherForecastViewHolder(View itemView) : base(itemView)
            {
                cityTextView = itemView.FindViewById<TextView>(Resource.Id.cityTextView);
                temperatureTextView = itemView.FindViewById<TextView>(Resource.Id.temperatureTextView);
                weatherImageView = itemView.FindViewById<ImageView>(Resource.Id.weatherImageView);

                // Initialize other views here as needed
            }

            public void Bind(HourWeatherForecastVO weatherForecast)
            {
                cityTextView.Text = $"{weatherForecast.TemperatureCelcius}C / {weatherForecast.TemperatureFahrenheit}F";
                temperatureTextView.Text = weatherForecast.Date.ToString("MMMM d, yyyy");
                Picasso.With(ItemView.Context)
            .Load(weatherForecast.ForecastPictureURL)
            .Into(weatherImageView);
                // Bind other views with data here
            }
        }
    }
}
