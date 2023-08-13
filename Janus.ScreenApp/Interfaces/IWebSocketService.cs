using System;
using System.Threading.Tasks;
using Janus.Domain.Entites;

namespace Janus.ScreenApp.Interfaces;

public interface IWebSocketService
{
    event EventHandler<string> MessageReceived;
    Task<bool> InitializeConnection(Guid guid);
    Task SendMessage(string methodName, object arg);
    Task CloseConnection();
    Task RegisterScreen(Screen screen);
}