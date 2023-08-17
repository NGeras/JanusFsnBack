using System.Windows.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using Janus.ScreenApp.Interfaces;
using Janus.ScreenApp.ViewModels;
using Janus.ScreenApp.Views;

namespace Janus.ScreenApp.Services;

public class NavigationService : INavigationService
{
    private Frame _mainPageFrame;

    public void InitializeMainPage(Frame mainPageFrame)
    {
        _mainPageFrame = mainPageFrame;
    }

    public void NavigateToScreenRegisterView()
    {
        var viewModel = Ioc.Default.GetRequiredService<RegisterScreenViewModel>();
        _mainPageFrame.Navigate(new RegisterScreenView(viewModel));
    }

    public void NavigateToAdPlayerView()
    {
        var viewModel = Ioc.Default.GetRequiredService<AdPlayerViewModel>();
        _mainPageFrame.Navigate(new AdPlayerView(viewModel));
    }

    public void GoBack()
    {
        if (!_mainPageFrame.CanGoBack) return;
        _mainPageFrame.GoBack();
    }
}