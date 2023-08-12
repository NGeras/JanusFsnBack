using System;
using System.Threading.Tasks;
using WebSocketSharp;

namespace Janus.ScreenApp.Interfaces;

public interface IWebSocketService
{
    event EventHandler<string> MessageReceived;
    Task SendMessage(string message);
    Task CloseConnection();
    Task OpenConnection();
}