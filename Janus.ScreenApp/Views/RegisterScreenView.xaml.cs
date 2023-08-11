using System;
using System.Windows.Controls;
using System.Windows.Navigation;
using Janus.ScreenApp.ViewModels;

namespace Janus.ScreenApp.Views;

public partial class RegisterScreenView : Page
{
    private RegisterScreenViewModel _viewModel;
    public RegisterScreenView(RegisterScreenViewModel viewModel)
    {
        _viewModel = viewModel;
        DataContext = _viewModel;
        InitializeComponent();
    }
}