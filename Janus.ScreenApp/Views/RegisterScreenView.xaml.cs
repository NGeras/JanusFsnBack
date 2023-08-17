using System.Windows.Controls;
using Janus.ScreenApp.ViewModels;

namespace Janus.ScreenApp.Views;

public partial class RegisterScreenView : Page
{
    private readonly RegisterScreenViewModel _viewModel;

    public RegisterScreenView(RegisterScreenViewModel viewModel)
    {
        _viewModel = viewModel;
        DataContext = _viewModel;
        InitializeComponent();
    }
}