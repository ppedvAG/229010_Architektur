using Microsoft.Extensions.DependencyInjection;
using ppedv.CarRentalXPress.Data.EfCore;
using ppedv.CarRentalXPress.Model.Contracts;
using ppedv.CarRentalXPress.UI.Desktop.ViewModels;
using System.Configuration;
using System.Data;
using System.Reflection.Metadata;
using System.Windows;

namespace ppedv.CarRentalXPress.UI.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();

            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            string conString = "Server=(localdb)\\mssqllocaldb;Database=CarRentalXPress_Tests;Trusted_Connection=true";

            services.AddSingleton<IRepository>(new CarRentalXPressContextRepositoryAdapter(conString));
            services.AddSingleton<CarsViewModel>();

            return services.BuildServiceProvider();
        }
    }

}
