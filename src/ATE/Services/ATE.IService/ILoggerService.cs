using System;
using System.Collections.Generic;
using System.Text;

namespace ATE.Wpf.Services.Interfaces
{
    public interface ILoggerService
    {
        void Info(string Message);

        void Warn(string Message);

        void Error(string Message);

        void Error(string Message, Exception exception);
    }
}
