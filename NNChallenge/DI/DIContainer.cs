using Autofac;
using NNChallenge.Interfaces;
using NNChallenge.Services;
using NNChallenge.ViewModels;

namespace NNChallenge.DI
{
    public class DIContainer
    {
        #region Fields

        private static IContainer _instance;

        #endregion Fields

        #region Properties

        public static IContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Builder().Build();
                }
                return _instance;
            }
        }

        #endregion Properties

        #region Methods

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

        private static void RegisterServices(ref ContainerBuilder builder)
        {
             }

        private static void RegisterViewModels(ref ContainerBuilder builder)
        {
        }

        #endregion Methods
    }
}