using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using Janus.ScreenApp.Interfaces;

namespace Janus.ScreenApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Loaded += (_, _) =>
            {
                Ioc.Default.GetRequiredService<INavigationService>().InitializeMainPage(MainFrame);
                var screenActivityManager = Ioc.Default.GetRequiredService<IScreenActivityManager>();
                screenActivityManager.Activate();
            };
            
            InitializeComponent();
        }
    }
}