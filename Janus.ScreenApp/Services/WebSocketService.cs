using System;
using System.Threading;
using Janus.ScreenApp.Interfaces;
using Newtonsoft.Json;
using WebSocketSharp;

namespace Janus.ScreenApp.Services;

public class WebSocketService : IWebSocketService
{
    private int _retry;
    public WebSocketService()
    {
        WebSocket = new WebSocket("ws://127.0.0.1:5005");
        WebSocket.OnOpen += (sender, e) =>
        {
            // Send a presence message to the server to indicate that the client is online
            // var presenceMessage = new
            //     { type = "PresenceMessage", userName = Environment.UserName };
            // var presenceJson = JsonConvert.SerializeObject(presenceMessage);
            // WebSocket.Send(presenceJson);
        };

        WebSocket.OnMessage += WebSocketOnOnMessage;

        WebSocket.OnError += (sender, args) => { Console.WriteLine(args.Exception); };

        WebSocket.OnClose += (sender, e) =>
        {
            if (e.WasClean)
            {
                Console.WriteLine("WebSocket disconnected!");
                return;
            }

            if (_retry < 5)
            {
                _retry++;
                Thread.Sleep(5000);
                WebSocket.Connect();
            }
            else
            {
                WebSocket.Log.Error("The reconnecting has failed.");
                Console.WriteLine("WebSocket disconnected!");
                _retry = 0;
            }
        };
    }
    public event EventHandler<MessageEventArgs>? MessageReceived;
    private WebSocket WebSocket { get; }

    public void SendMessage(string message)
    {
        WebSocket.Send(message);
    }

    public void CloseConnection()
    {
        WebSocket.Close(CloseStatusCode.Normal);
    }

    public void OpenConnection()
    {
        WebSocket.Connect();
    }
    private void WebSocketOnOnMessage(object? sender, MessageEventArgs e)
    {
        MessageReceived?.Invoke(sender, e);
    }
}