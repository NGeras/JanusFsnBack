using System;
using System.Net;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Janus.ScreenApp.ViewModels;

public class AdPlayerViewModel : ObservableObject
{
    private Uri _videoUri;
    public AsyncRelayCommand DownloadVideoCommand { get; set; }
    public Uri VideoUri
    {
        get => _videoUri;
        set => SetProperty(ref _videoUri, value);
    }

    public AdPlayerViewModel()
    {
        DownloadVideoCommand = new AsyncRelayCommand(DownloadVideo);
    }

    public async Task DownloadVideo()
    {
        const string videoPath = @"C:\Users\Kirill.BAIT\source\repos\JanusFsnBack\Janus.ScreenApp\Assets\tets.mp4";
        using var client = new WebClient();
        
        await client.DownloadFileTaskAsync("https://localhost:7224/staticfiles/video.mp4", 
            videoPath);

        VideoUri = new Uri(videoPath);
    }
}