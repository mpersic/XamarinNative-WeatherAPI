using Autofac;
using NNChallenge.Interfaces;
using NNChallenge.Services;
using NNChallenge.ViewModels;

namespace NNChallenge.DI
{
    public class DIContainer
    {
        private static IContainer _instance;

        public static IContainer Instance
        {
            get
            {
                _instance ??= Builder().Build();
                return _instance;
            }
        }
        public T Resolve<T>(params Autofac.Core.Parameter[] ps)
        {
            // Do not allow directly resolving view models from the container.
            // Note: only in the application
            return _instance.Resolve<T>(ps);
        }

        private static ContainerBuilder Builder()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<OpenWeatherApiService>().As<IOpenWeatherApiService>();
            builder.RegisterType<ForecastViewModel>();
            return builder;
        }
    }
}