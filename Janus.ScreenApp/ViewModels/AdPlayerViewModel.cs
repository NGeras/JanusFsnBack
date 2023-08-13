using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Janus.ScreenApp.Interfaces;

namespace Janus.ScreenApp.ViewModels;

public class AdPlayerViewModel : ObservableObject
{
    private readonly IScreenActivityManager _screenActivityManager;
    private Uri _videoUri;
    public AsyncRelayCommand DownloadVideoCommand { get; set; }
    public Uri VideoUri
    {
        get => _videoUri;
        set => SetProperty(ref _videoUri, value);
    }

    public AdPlayerViewModel(IScreenActivityManager screenActivityManager)
    {
        _screenActivityManager = screenActivityManager;
        _screenActivityManager.VideoDownloaded += ScreenActivityManagerOnVideoDownloaded;
    }

    private void ScreenActivityManagerOnVideoDownloaded(object? sender, Uri videoUri)
    {
        VideoUri = videoUri;
    }
}