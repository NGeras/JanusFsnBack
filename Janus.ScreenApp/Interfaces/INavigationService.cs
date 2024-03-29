﻿using System.Windows.Controls;

namespace Janus.ScreenApp.Interfaces;

public interface INavigationService
{
    void InitializeMainPage(Frame mainPageFrame);
    void NavigateToScreenRegisterView();
    void NavigateToAdPlayerView();
    void GoBack();
}