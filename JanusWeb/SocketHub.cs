using Microsoft.AspNetCore.SignalR;

namespace JanusWeb
{
    internal class SocketHub : Hub
    {
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

        public async Task SendScreenStatus(string screenId, bool isOnline)
        {
            // Handle screen status updates here, like updating the online/offline status in your data structure.
            // You can also broadcast this information to other connected clients.
            Console.WriteLine($"New screen added {screenId}");
            await Clients.All.SendAsync("ReceiveScreenStatus", screenId, isOnline);
        }
    }
}