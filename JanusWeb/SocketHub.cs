using Janus.DAL;
using Janus.Domain.Entites;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

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

    public async Task TriggerDownloadForScreen(Screen screen, Uri videoUri)
    {
        await Clients.Client(screen.ConnectionId).SendAsync("TriggerDownload", videoUri);
    }

    public async Task TriggerDownloadForEveryone(Uri videoUri)
    {
        await Clients.All.SendAsync("TriggerDownload", videoUri);
    }

    public async Task RegisterScreen(Screen screen)
    {
        screen.ConnectionId = Context.ConnectionId;
        await _dbContext.Screens.AddAsync(screen);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> SendScreenStatus(Guid screenId)
    {
        // Handle screen status updates here, like updating the online/offline status in your data structure.
        // You can also broadcast this information to other connected clients.
        var foundScreen = await _dbContext.Screens.FirstOrDefaultAsync(x => x.ScreenAppId.Equals(screenId));
        if (foundScreen == null)
        {
            Console.WriteLine("Asked for register from new screen");
            return true;
        }

        if (foundScreen.ConnectionId != Context.ConnectionId)
        {
            foundScreen.ConnectionId = Context.ConnectionId;
            _dbContext.Update(foundScreen);
            await _dbContext.SaveChangesAsync();
        }
        Console.WriteLine($"Screen found in db {screenId}");
        return false;
    }
}