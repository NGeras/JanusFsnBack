using System;
using System.Threading.Tasks;
using Janus.Domain.Entites;

namespace Janus.ScreenApp.Interfaces;

public interface IScreenActivityManager
{
    Task Activate(Guid guid);
    Task RegisterScreen(Screen screen);
}