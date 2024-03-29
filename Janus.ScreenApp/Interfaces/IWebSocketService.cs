﻿using System;
using System.Threading.Tasks;
using Janus.Domain.Entites;

namespace Janus.ScreenApp.Interfaces;

public interface IWebSocketService
{
    string? ConnectionId { get; }
    event EventHandler<Uri> TriggerVideoDownload;
    Task<bool> InitializeConnection(Guid guid);
    Task SendMessage(string methodName, object arg);
    Task CloseConnection();
    Task RegisterScreen(Screen screen);
}