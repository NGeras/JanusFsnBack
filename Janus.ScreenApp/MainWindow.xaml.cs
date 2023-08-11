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
                var navigationService = Ioc.Default.GetRequiredService<INavigationService>();
                navigationService.InitializeMainPage(MainFrame);
                
                navigationService.NavigateToScreenRegisterView();
            };
            
            InitializeComponent();
        }
    }
}