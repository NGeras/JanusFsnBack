using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Janus.Domain.Entites;
using Janus.ScreenApp.Interfaces;
using Janus.ScreenApp.Properties;

namespace Janus.ScreenApp.ViewModels;

public class RegisterScreenViewModel : ObservableObject
{
    private readonly IScreenActivityManager _screenActivityManager;
    private string _category;
    private string _location;

    public RegisterScreenViewModel(IScreenActivityManager screenActivityManager)
    {
        _screenActivityManager = screenActivityManager;
        RegisterCommand = new RelayCommand(Register);
    }

    public RelayCommand RegisterCommand { get; }

    public string Category
    {
        get => _category;
        set => SetProperty(ref _category, value);
    }

    public string Location
    {
        get => _location;
        set => SetProperty(ref _location, value);
    }

    private void Register()
    {
        // var a = Category;
        // var b = Location;
        _screenActivityManager.RegisterScreen(new Screen
        {
            Category = Category,
            Location = Location,
            ScreenAppId = Settings.Default.ScreenId
        });
    }
}