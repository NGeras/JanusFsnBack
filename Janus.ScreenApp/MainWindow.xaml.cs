using System;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Janus.ScreenApp.Interfaces;
using Janus.ScreenApp.Properties;

namespace Janus.ScreenApp;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        Loaded += async (_, _) =>
        {
            Ioc.Default.GetRequiredService<INavigationService>().InitializeMainPage(MainFrame);
            var screenActivityManager = Ioc.Default.GetRequiredService<IScreenActivityManager>();
            if (Settings.Default.ScreenId == Guid.Empty)
            {
                var currentScreenGuid = Guid.NewGuid();
                Settings.Default.ScreenId = currentScreenGuid;
                Settings.Default.Save();
            }

            await screenActivityManager.Activate(Settings.Default.ScreenId);
        };

        InitializeComponent();
    }
}