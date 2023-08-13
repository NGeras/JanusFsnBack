﻿using System;
using System.Threading.Tasks;

namespace Janus.ScreenApp.Interfaces;

public interface IWebSocketService
{
    event EventHandler<string> MessageReceived;
    Task<bool> InitializeConnection(Guid guid);
    Task SendMessage(string methodName, object arg);
    Task CloseConnection();
}