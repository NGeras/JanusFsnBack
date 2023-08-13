using System;
using System.Threading.Tasks;
using Janus.Domain;
using Janus.Domain.Entites;
using Janus.ScreenApp.Interfaces;
using Janus.ScreenApp.Properties;
using Microsoft.AspNetCore.SignalR.Client;

namespace Janus.ScreenApp.Services;

public class WebSocketService : IWebSocketService
{
    public event EventHandler<Uri> TriggerVideoDownload; 
    
    private readonly HubConnection _hubConnection;
    private int _retry;

    public WebSocketService()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7066/ScreenSocket") // Replace with your hub URL
            .Build();

        // _hubConnection.On<Enums.HubMessageType, object>(Enums.HubMethodNames.ReceiveMessage.ToString(), MessageReceivedHandler);
        _hubConnection.On<Enums.HubMessageType, Uri>(Enums.HubMethodNames.ReceiveMessage.ToString(),
            MessageReceivedHandler);
        _hubConnection.Reconnected += HubConnectionOnReconnected;
    }

    private async Task HubConnectionOnReconnected(string? arg)
    {
        await SendScreenStatus(Settings.Default.ScreenId);
    }

    private void MessageReceivedHandler(Enums.HubMessageType hubMessageType, Uri argument)
    {
        Console.WriteLine("Received message: " + hubMessageType);
        switch (hubMessageType)
        {
            case Enums.HubMessageType.TriggerVideoDownload:
                TriggerVideoDownload(this, argument);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(hubMessageType), hubMessageType, null);
        }
    }

    public string? ConnectionId => _hubConnection.ConnectionId;

    public async Task<bool> InitializeConnection(Guid guid)
    {
        await OpenConnection();

        return await SendScreenStatus(guid);
    }

    public async Task SendMessage(string methodName, object arg)
    {
        await _hubConnection.InvokeAsync(methodName, arg);
    }

    public async Task CloseConnection()
    {
        await _hubConnection.StopAsync();
    }

    public async Task RegisterScreen(Screen screen)
    {
        await _hubConnection.SendAsync(Enums.HubMethodNames.RegisterScreen.ToString(), screen);
    }

    private async Task<bool> SendScreenStatus(Guid guid)
    {
        return await _hubConnection.InvokeAsync<bool>(Enums.HubMethodNames.SendScreenStatus.ToString(), guid);
    }

    private async Task OpenConnection()
    {
        await _hubConnection.StartAsync();
    }
}