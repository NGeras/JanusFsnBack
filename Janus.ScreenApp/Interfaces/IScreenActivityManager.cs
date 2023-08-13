using System;
using System.Threading.Tasks;
using Janus.Domain.Entites;

namespace Janus.ScreenApp.Interfaces;

public interface IScreenActivityManager
{
    event EventHandler<Uri> VideoDownloaded;
    string? ConnectionId { get; }
    Task Activate(Guid guid);
    Task RegisterScreen(Screen screen);
}