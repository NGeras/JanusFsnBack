using System.Windows.Controls;
using Janus.ScreenApp.Interfaces;
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
        _mainPageFrame.Navigate(new RegisterScreenView());
    }

    public void GoBack()
    {
        if (!_mainPageFrame.CanGoBack) return;
        _mainPageFrame.GoBack();
    }
}