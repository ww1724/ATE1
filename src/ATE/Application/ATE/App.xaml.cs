using ATE.Wpf.Services;
using ATE.Wpf.Services.Interfaces;
using Splat;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ATE
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Locator.CurrentMutable.RegisterLazySingleton(() => new DbService(), typeof(IDbService));
            Locator.CurrentMutable.RegisterLazySingleton(() => new LoggerService(), typeof(ILoggerService));

            base.OnStartup(e);
        }
    }
}
