using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Janus.ScreenApp.Interfaces;

namespace Janus.ScreenApp.ViewModels;

public class RegisterScreenViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    public RelayCommand RegisterCommand { get; }
    public RegisterScreenViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        RegisterCommand = new RelayCommand(Register);
    }
    
    private void Register()
    {
        _navigationService.NavigateToAdPlayerView();
    }
}