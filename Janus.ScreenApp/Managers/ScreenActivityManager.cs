using System;
using System.Threading.Tasks;
using Janus.ScreenApp.Interfaces;

namespace Janus.ScreenApp.Managers;

public class ScreenActivityManager : IScreenActivityManager
{
    private readonly INavigationService _navigationService;
    private readonly IWebSocketService _webSocketService;
    
    private bool _isRegistrationRequired;

    public ScreenActivityManager(INavigationService navigationService, IWebSocketService webSocketService)
    {
        _navigationService = navigationService;
        _webSocketService = webSocketService;
    }

    public async Task Initialize(Guid guid)
    {
        _isRegistrationRequired = await _webSocketService.InitializeConnection(guid);
    }

    public void Activate()
    {
        if (_isRegistrationRequired)
        {
            _navigationService.NavigateToScreenRegisterView();
            return;
        }
        _navigationService.NavigateToAdPlayerView();
    }
}