using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Janus.ScreenApp.Interfaces;
using Janus.ScreenApp.Properties;
using Janus.ScreenApp.Services;
using Janus.ScreenApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Janus.ScreenApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddScoped(sp => new HttpClient());
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IWebSocketService, WebSocketService>();
            services.AddTransient<AdPlayerViewModel, AdPlayerViewModel>();
            services.AddTransient<RegisterScreenViewModel, RegisterScreenViewModel>();
            
            Ioc.Default.ConfigureServices(services.BuildServiceProvider());
        }

        private async void App_OnStartup(object sender, StartupEventArgs e)
        {
            ConfigureServices();
            await ConfigureWebSocketConnection();
        }

        private async Task ConfigureWebSocketConnection()
        {
            var webSocketService = Ioc.Default.GetRequiredService<IWebSocketService>();
            if (Settings.Default.ScreenId != Guid.Empty)
            {
                var currentScreenGuid = new Guid();
                Settings.Default.ScreenId = currentScreenGuid;
                Settings.Default.Save();
            }

            await webSocketService.InitializeConnection(Settings.Default.ScreenId);
        }
    }
}