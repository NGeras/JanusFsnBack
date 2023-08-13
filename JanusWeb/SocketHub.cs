using Janus.DAL;
using Microsoft.AspNetCore.SignalR;

namespace JanusWeb;

internal class SocketHub : Hub
{
    private readonly JanusDbContext _dbContext;

    public SocketHub(JanusDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override async Task OnConnectedAsync()
    {
        // Perform any logic you need upon client connection

        await SendMessage("A new client has connected."); // Trigger SendMessage upon client connection
        Console.WriteLine($"Client {Context.ConnectionId} connected");

        await base.OnConnectedAsync();
    }

    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine($"Client {Context.ConnectionId} disconnected");
        Console.WriteLine(exception);
        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendScreenStatus(Guid screenId)
    {
        // Handle screen status updates here, like updating the online/offline status in your data structure.
        // You can also broadcast this information to other connected clients.
        if (_dbContext.Screens.FirstOrDefault(x => x.ScreenAppId.Equals(screenId)) == null)
        {
            await Clients.Caller.SendAsync("RequireRegistration");
            Console.WriteLine("Asked for register from new screen");
            return;
        }

        Console.WriteLine($"Screen found in db {screenId}");
        await Clients.All.SendAsync("ReceiveScreenStatus", screenId);
    }
}