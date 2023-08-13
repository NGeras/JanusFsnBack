using System;
using System.Threading.Tasks;
using Janus.ScreenApp.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace Janus.ScreenApp.Services;

public class WebSocketService : IWebSocketService
{
    private int _retry;
    public WebSocketService()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7066/Screens") // Replace with your hub URL
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

    public async Task<bool> InitializeConnection(Guid guid)
    {
        await OpenConnection();

        return await SendScreenStatus(guid);
    }

    public async Task SendMessage(string methodName, object arg)
    {
        await _hubConnection.InvokeAsync(methodName, arg);
    }
    
    private async Task<bool> SendScreenStatus(Guid guid)
    {
        return await _hubConnection.InvokeAsync<bool>("SendScreenStatus", guid);
    }

    public async Task CloseConnection()
    {
        await _hubConnection.StopAsync();
    }

    private async Task OpenConnection()
    {
        await _hubConnection.StartAsync();
    }
}