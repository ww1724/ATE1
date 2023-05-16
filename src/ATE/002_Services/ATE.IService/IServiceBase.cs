using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATE.Wpf.Services.Interfaces
{
    public enum ServiceState
    {
        Uninit,
        Running,
        Paused, 
        Stop,
        Error
    }

    public interface IServiceBase
    {
        string Name { get; }
        string Description { get; }
        ServiceState ServiceState { get; }
    }
}
