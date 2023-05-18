using ATE.Core.Mvvm;
using Caliburn.Micro;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using WorkflowCore.Interface;
using Zoranof.GraphicsFramework;
using Zoranof.GraphicsFramework.Shapes;
using Zoranof.WorkFlow;
using Zoranof.WorkFlow.Base;

namespace ATE.ViewModels
{
    public class TestingViewModel : Screen, IViewModel
    {
        DispatcherTimer DispatcherTimer;

        IServiceProvider serviceProvider
            => IoC.Get<IServiceProvider>();

        IWorkflowHost Host;

        public TestingViewModel()
        {
            Items = new()
            { 
                //new LineItem() { Pos = new Point(100, 100), Width=200, Height=200 }, 
                new StartNode() { Pos = new Point(100, 100) } ,
                new StartNode() { Pos = new Point(200, 150) } ,
                new StartNode() { Pos = new Point(100, 600) } ,
                new WorkflowNode() { Pos = new Point(500, 600) } ,
                new WorkflowNode() { Pos = new Point(500, 200) } ,
                new WorkflowNode() { Pos = new Point(20, 200) } ,
            };
        }

        #region Props
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; NotifyOfPropertyChange(); }
        }

        private ObservableCollection<GraphicsItem> items;

        public ObservableCollection<GraphicsItem> Items
        {
            get { return items; }
            set { items = value; NotifyOfPropertyChange(); }
        }

        #endregion

        #region Lifetime
        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            Host = serviceProvider.GetService<IWorkflowHost>();

        }

        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);
        }
        #endregion

        #region Actions
        public void AddSomeThingToGraphicsView()
        {
            Items.Add(new WorkflowNode { Pos = new Point((new Random().Next(0, 500)), (new Random().Next(0, 500))) });

        }

        public async void Test1Action()
        {
            Host.RegisterWorkflow<HWorkflow, MyData>();
            await Task.Run(() =>
            {
                Host.Start();
            });
            var id = await Host.StartWorkflow<MyData>("H", 1, new MyData { A = 100, B = 10 });
        }

        public void Test2Action() { }

        public void Test3Action() { }

        public void Test4Action() { }


        #endregion



    }

    public class MyData
    {
        public int A;

        public int B;

        public int Result;
    }

    public class HWorkflow : IWorkflow<MyData>
    {
        public string Id => "H";

        public int Version => 1;

        public void Build(IWorkflowBuilder<MyData> builder)
        {
        }
    }
}
