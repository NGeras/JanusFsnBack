using System;
using System.Threading.Tasks;
using Janus.Domain.Entites;
using Janus.ScreenApp.Interfaces;

namespace Janus.ScreenApp.Managers;

public class ScreenActivityManager : IScreenActivityManager
{
    private readonly INavigationService _navigationService;
    private readonly IWebSocketService _webSocketService;

    public ScreenActivityManager(INavigationService navigationService, IWebSocketService webSocketService)
    {
        _navigationService = navigationService;
        _webSocketService = webSocketService;
    }

    public async Task Activate(Guid guid)
    {
        if (await _webSocketService.InitializeConnection(guid))
        {
            _navigationService.NavigateToScreenRegisterView();
            return;
        }

        _navigationService.NavigateToAdPlayerView();
    }

    public async Task RegisterScreen(Screen screen)
    {
        await _webSocketService.RegisterScreen(screen);
        _navigationService.NavigateToAdPlayerView();
    }
}