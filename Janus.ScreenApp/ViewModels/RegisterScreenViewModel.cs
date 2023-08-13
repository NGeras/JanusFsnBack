using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Janus.ScreenApp.Interfaces;

namespace Janus.ScreenApp.ViewModels;

public class RegisterScreenViewModel : ObservableObject
{
    private readonly IScreenActivityManager _screenActivityManager;
    private string _category;
    private string _location;
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

    public RegisterScreenViewModel(IScreenActivityManager screenActivityManager)
    {
        _screenActivityManager = screenActivityManager;
        RegisterCommand = new RelayCommand(Register);
    }
    
    private void Register()
    {
        var a = Category;
        var b = Location;
    }
}