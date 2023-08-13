using System;
using System.Threading.Tasks;

namespace Janus.ScreenApp.Interfaces;

public interface IScreenActivityManager
{
    Task Initialize(Guid guid);
    void Activate();
}