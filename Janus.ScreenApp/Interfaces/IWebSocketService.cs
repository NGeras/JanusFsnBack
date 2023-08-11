using System;
using WebSocketSharp;

namespace Janus.ScreenApp.Interfaces;

public interface IWebSocketService
{
    event EventHandler<MessageEventArgs> MessageReceived;
    void SendMessage(string message);
    void CloseConnection();
    void OpenConnection();
}