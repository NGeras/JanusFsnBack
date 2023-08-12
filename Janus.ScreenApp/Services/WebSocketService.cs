using System;
using System.Threading;
using System.Threading.Tasks;
using Janus.ScreenApp.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using WebSocketSharp;

namespace Janus.ScreenApp.Services;

public class WebSocketService : IWebSocketService
{
    private int _retry;
    public WebSocketService()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7030/Screens") // Replace with your hub URL
            .Build();

        _hubConnection.On<string>("ReceiveMessage", message =>
        {
            // Handle the received message
            MessageReceived?.Invoke(this, message);
            Console.WriteLine("Received message: " + message);
        });
    }
    public event EventHandler<string> MessageReceived;
    private readonly HubConnection _hubConnection;

    public async Task SendMessage(string message)
    {
        await _hubConnection.InvokeAsync("SendMessage", message);
    }

    public async Task CloseConnection()
    {
        await _hubConnection.StopAsync();
    }

    public async Task OpenConnection()
    {
        await _hubConnection.StartAsync();
    }
}