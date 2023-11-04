
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;

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
                var current = await vs.GetCurrentWeather();
                adapter.SetData(current);
                await vs.GetDailyWeather();
                // Now you can use the selectedLocation in your ForecastActivity.
            }
            // Handle the case where the extra is not found if needed.
        }

    }

    public class WeatherForecastAdapter : RecyclerView.Adapter
    {
        private List<Interfaces.WeatherForecastVO> weatherForecastList;
        private Context context;

        public WeatherForecastAdapter(Context context)
        {
            this.context = context;
            weatherForecastList = new List<Interfaces.WeatherForecastVO>();
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
                Interfaces.WeatherForecastVO weatherForecast = weatherForecastList[position];
                weatherHolder.Bind(weatherForecast);
            }
        }

        public override int ItemCount => weatherForecastList.Count;

        public void SetData(List<Interfaces.WeatherForecastVO> data)
        {
            weatherForecastList = data;
            NotifyDataSetChanged();
        }

        public class WeatherForecastViewHolder : RecyclerView.ViewHolder
        {
            private TextView cityTextView;
            private TextView temperatureTextView;

            public WeatherForecastViewHolder(View itemView) : base(itemView)
            {
                cityTextView = itemView.FindViewById<TextView>(Resource.Id.cityTextView);
                temperatureTextView = itemView.FindViewById<TextView>(Resource.Id.temperatureTextView);

                // Initialize other views here as needed
            }

            public void Bind(Interfaces.WeatherForecastVO weatherForecast)
            {
                cityTextView.Text = weatherForecast.City;
                temperatureTextView.Text = weatherForecast.CurrentWeather.TemperatureCelsius.ToString();

                // Bind other views with data here
            }
        }
    }
}
