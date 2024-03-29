﻿using System.Net.Http;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Janus.ScreenApp.Interfaces;
using Janus.ScreenApp.Managers;
using Janus.ScreenApp.Services;
using Janus.ScreenApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Janus.ScreenApp;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private void ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddScoped(_ => new HttpClient());
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<IWebSocketService, WebSocketService>();
        services.AddTransient<AdPlayerViewModel, AdPlayerViewModel>();
        services.AddTransient<RegisterScreenViewModel, RegisterScreenViewModel>();
        services.AddTransient<IScreenActivityManager, ScreenActivityManager>();

        Ioc.Default.ConfigureServices(services.BuildServiceProvider());
    }

    private void App_OnStartup(object sender, StartupEventArgs e)
    {
        ConfigureServices();
    }
}