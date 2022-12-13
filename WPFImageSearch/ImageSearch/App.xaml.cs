using ImageSearchServer.Core;
using ImageSearchServer.MVVM.ViewModel;
using ImageSearchServer.DataAccesslayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SharedUtility;
using System.IO;
using System.Windows;

namespace ImageSearchServer
{
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }
        public IConfiguration Configuration { get; private set; }
        private const string APPSETTINGS_JSON_NAME = "appsettings.json";
        private string DEFAULT_LOGS_PATH = $"{Path.GetTempPath()}//IMLogs//Client//IMlogs.txt";

        public App()
        {
            SetupAppSettingsJSON();

            AppHost = Host.CreateDefaultBuilder()
                .UseSerilog((host, loggerConfiguration) =>
                {
                    loggerConfiguration.WriteTo.File(Configuration[ClientConstants.AppSettingsKey.LogsFolderPath] ?? DEFAULT_LOGS_PATH, rollingInterval: RollingInterval.Day);
                })
                .ConfigureServices((hostContext, services) => {
                    services.AddSingleton<ConfigurationBuilder>();
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<IMainViewModel, MainViewModel>();
                    services.AddSingleton<IClientAppDAL, ClientAppDAL>();
                    services.AddSingleton<ICustomHttpClientFactory, CustomHttpClientFactory>();
                    services.AddTransient<IHttpClientUtility, HttpClientUtility>();
                })
                .Build();
        }

        private void SetupAppSettingsJSON()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile(APPSETTINGS_JSON_NAME, optional: false, reloadOnChange: true);
            Configuration = builder.Build();

        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();
            var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
            startupForm.Show();

            base.OnStartup(e);
        }


        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }
}
