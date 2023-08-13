using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Janus.ScreenApp.Utils;

namespace Janus.ScreenApp.ViewModels;

public class AdPlayerViewModel : ObservableObject
{
    private Uri _videoUri;
    private readonly HttpClient _httpClient;

    public AsyncRelayCommand DownloadVideoCommand { get; set; }
    public Uri VideoUri
    {
        get => _videoUri;
        set => SetProperty(ref _videoUri, value);
    }

    public AdPlayerViewModel(HttpClient httpClient)
    {
        DownloadVideoCommand = new AsyncRelayCommand(DownloadVideo);
        _httpClient = httpClient;
    }

    public async Task DownloadVideo()
    {
        var folder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        if (!string.IsNullOrWhiteSpace(folder))
        {
            var assetsPath = Path.Combine(folder, "Assets");
            string videoPath = Path.Combine(assetsPath, "test.mp4");
            await _httpClient.DownloadFileTaskAsync(new Uri("https://localhost:7066/staticfiles/video.mp4"), videoPath);

            VideoUri = new Uri(videoPath);
        }
    }
}