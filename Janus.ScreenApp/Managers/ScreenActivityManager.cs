using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Janus.Domain.Entites;
using Janus.ScreenApp.Interfaces;
using Janus.ScreenApp.Utils;

namespace Janus.ScreenApp.Managers;

public class ScreenActivityManager : IScreenActivityManager
{
    public event EventHandler<Uri>? VideoDownloaded;
    
    
    private readonly INavigationService _navigationService;
    private readonly IWebSocketService _webSocketService;
    private readonly HttpClient _httpClient;

    private string _currentVideoContentPath;

    public ScreenActivityManager(INavigationService navigationService, IWebSocketService webSocketService,
        HttpClient httpClient)
    {
        _navigationService = navigationService;
        _webSocketService = webSocketService;
        _httpClient = httpClient;
        _webSocketService.TriggerVideoDownload += WebSocketServiceOnTriggerVideoDownload;
    }

    private async void WebSocketServiceOnTriggerVideoDownload(object? sender, Uri videoUri)
    {
        _currentVideoContentPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.mp4");
        await _httpClient.DownloadFileTaskAsync(videoUri, _currentVideoContentPath);
        VideoDownloaded?.Invoke(this, new Uri(_currentVideoContentPath));
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