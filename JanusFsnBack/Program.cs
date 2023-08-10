using System;
using System.Collections.Concurrent;
using System.Net.NetworkInformation;
using Janus.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;
using ErrorEventArgs = WebSocketSharp.ErrorEventArgs;

namespace JanusFsnBack;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>();
                services.AddDbContext<JanusDbContext>();
            });
    }
}

public class Worker : IHostedService
{
    private static WebSocketServer _wssv;
    private readonly IServiceProvider _serviceProvider;

    public Worker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _wssv = new WebSocketServer(5005);
        _wssv.AddWebSocketService<MyWebSocketBehavior>("/");
        _wssv.Start();

        // using var scope = _serviceProvider.CreateScope();
        // var dbContext = scope.ServiceProvider.GetRequiredService<JanusDbContext>();
        //
        // // Now you can use dbContext to interact with the database
        // // Example:
        // var entities = await dbContext.Screens.ToListAsync();

        Console.WriteLine($"WebSocket server started. Listening on {_wssv.Address}:{_wssv.Port}");
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Stopping server");
        _wssv.Stop();
    }
}

internal class MyWebSocketBehavior : WebSocketBehavior
{
    protected override void OnOpen()
    {
        Console.WriteLine($"Client connected: {Context.UserEndPoint}");
    }

    protected override void OnError(ErrorEventArgs e)
    {
        Console.WriteLine(e.Exception);
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        // Handle the incoming message and send responses if needed.
        Console.WriteLine(e.Data);
        // Deserialize the JSON data to a dynamic object to access the "type" property
        var messageData = JsonConvert.DeserializeObject<dynamic>(e.Data);

        // Check the "type" property to differentiate between message types
        if (messageData != null)
        {
            string messageType = messageData.type;
            switch (messageType)
            {
                case "PresenceMessage":
                {

                    // It's a PresenceMessage, deserialize to the appropriate class
                    // var presenceMessage = JsonConvert.DeserializeObject<PresenceMessage>(e.Data);
                    // // Handle the PresenceMessage, e.g., add the machine to the list of online clients
                    // // and notify NextJS about it.
                    // // string userId = Context.User.GetHashCode().ToString(); // Get the user ID here.
                    // if (presenceMessage != null &&
                    //     !string.IsNullOrWhiteSpace(presenceMessage.type) &&
                    //     !string.IsNullOrWhiteSpace(presenceMessage.userName))
                    // {
                    //     ConnectedClients.TryAdd(ID, presenceMessage.userName);
                    //     Console.WriteLine(ConnectedClients.Count);
                    //     var data = new { type = "UserOnline", userId = ID, presenceMessage.userName };
                    //     var dataJson = JsonConvert.SerializeObject(data);
                    //     Console.WriteLine(dataJson);
                    //     var workstationList = ConnectedClients.Select(pair => new Workstation
                    //     {
                    //         userId = pair.Key,
                    //         userName = pair.Value
                    //     }).ToList();
                    //     var sessionIdResponse = new
                    //         { type = "ResponseMessage", sessionId = ID, connectedClients = workstationList };
                    //     var responseJson = JsonConvert.SerializeObject(sessionIdResponse);
                    //     Console.WriteLine(responseJson);
                    //     Send(responseJson);
                    //     Sessions.Broadcast(dataJson);
                    // }

                    break;
                }
                case "TriggerDownload":
                {
                    // It's an InviteMessage, deserialize to the appropriate class
                    // var inviteMessage = JsonConvert.DeserializeObject<InviteMessage>(e.Data);
                    // // Handle the InviteMessage, e.g., send an invite to the WPF client with the given GUID.
                    // if (inviteMessage != null &&
                    //     !string.IsNullOrWhiteSpace(ID) &&
                    //     ConnectedClients.ContainsKey(inviteMessage.userId))
                    //     Sessions.SendTo(e.Data, inviteMessage.userId);

                    break;
                }
            }
        }
    }

    protected override void OnClose(CloseEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(ID))
        {
            Console.WriteLine($"Client {ID} - disconnected {e.Code}");
            var data = new { type = "UserOffline", userId = ID };
            var dataJson = JsonConvert.SerializeObject(data);
            Sessions.Broadcast(dataJson);
        }

        // Remove the disconnected client from your list of online users (if needed).
    }
}