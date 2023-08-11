using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Janus.ScreenApp.ViewModels;

namespace Janus.ScreenApp.Views;

public partial class AdPlayerView : Page
{
    private AdPlayerViewModel _viewModel;
    public AdPlayerView(AdPlayerViewModel viewModel)
    {
        _viewModel = viewModel;
        DataContext = viewModel;
        InitializeComponent();
    }

    private void VideoPlayer_OnLoaded(object sender, RoutedEventArgs e)
    {
        VideoPlayer.Source =
            new Uri(@"C:\Users\Kirill.BAIT\source\repos\JanusFsnBack\Janus.ScreenApp\Assets\video.mp4");
    }

    private void VideoPlayer_OnMediaEnded(object sender, RoutedEventArgs e)
    {
        VideoPlayer.Position = TimeSpan.Zero;
    }
}