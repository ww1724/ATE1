using ATE.ViewModels;
using ATE.Wpf.Services.Interfaces;
using ATE.Wpf.Services;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using ATE.Core;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition;
using ATE.Core.Stores;
using ATE.Core.Mvvm;
using Microsoft.Extensions.DependencyInjection;
using Zoranof.WorkFlow;

namespace ATE
{
    public class AppBootstrapper : BootstrapperBase
    {
        private IServiceProvider serviceProvider;

        private CompositionContainer container;

        private AggregateCatalog aggregateCatalog;

        private IWindowManager windowManager;

        private IEventAggregator eventAggregator;

        private SimpleContainer simpleContainer;

        private ILoggerService loggerService;

        public AppBootstrapper()
        {
            Initialize();
        }

        private ServiceProvider ConfigurationServices()
        {
            var services = new ServiceCollection();
            services.AddCustomWorkFlow();
            return services.BuildServiceProvider();
        }

        protected override void Configure()
        {

            //new DirectoryCatalog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "modules"), "AutoFlasher.Modules.*.dll")
            //var aggregateCatalog = new AggregateCatalog(
            //    new AssemblyCatalog(Assembly.GetExecutingAssembly())
            //);

            aggregateCatalog = new AggregateCatalog(AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>());
            if (Directory.Exists(@"modules"))
                aggregateCatalog.Catalogs.Add(new DirectoryCatalog(@"modules", "Moudles.*.dll"));

            container = new CompositionContainer(aggregateCatalog);
            var batch = new CompositionBatch();

            windowManager = new WindowManager();
            batch.AddExportedValue(windowManager);

            eventAggregator = new EventAggregator();
            batch.AddExportedValue(eventAggregator);

            loggerService = new LoggerService();
            batch.AddExportedValue(loggerService);

            serviceProvider = ConfigurationServices();
            batch.AddExportedValue<IServiceProvider>(serviceProvider);

            simpleContainer = new SimpleContainer();
            simpleContainer.Singleton<IViewModel, ShellViewModel>(Constants.ShellView);
            simpleContainer.Singleton<IViewModel, MenuStore>(Constants.MenuStore);

            simpleContainer.PerRequest<IViewModel, TestingViewModel>(Constants.TestingBoardView);
            simpleContainer.PerRequest<IViewModel, ConsoleViewModel>(Constants.ConsoleView);
            batch.AddExportedValue(simpleContainer);

            container.Compose(batch);
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            base.OnExit(sender, e);
        }

        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            await windowManager.ShowWindowAsync(simpleContainer.GetInstance<IViewModel>(Constants.ShellView));
        }

        protected override object GetInstance(Type service, string key)
        {
            string contract = string.IsNullOrEmpty(key)
                ? AttributedModelServices.GetContractName(service)
                : key;

            var exports = container.GetExportedValues<object>(contract);

            if (exports.Any())
                return exports.First();

            throw new Exception($"找不到实例{contract}");

        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetExportedValues<object>(AttributedModelServices.GetContractName(service));
        }
    }
}
