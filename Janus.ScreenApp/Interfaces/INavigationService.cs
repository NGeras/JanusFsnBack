using System;
using System.Windows.Controls;

namespace Janus.ScreenApp.Interfaces;

public interface INavigationService
{
    void InitializeMainPage(Frame mainPageFrame);
    void NavigateToScreenRegisterView();
    void GoBack();
}