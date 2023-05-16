using ATE.Core.Mvvm;
using Caliburn.Micro;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;
using Zoranof.GraphicsFramework;
using Zoranof.GraphicsFramework.Base;
using Zoranof.WorkFlow;

namespace ATE.ViewModels
{
    public class TestingViewModel : PropertyChangedBase, IViewModel
    {
        DispatcherTimer DispatcherTimer;
        public TestingViewModel()
        {
            DispatcherTimer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };

            DispatcherTimer.Tick += (object sender, EventArgs e) =>
            {
                AddSomeThingToGraphicsView();
            };

        }

        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; NotifyOfPropertyChange(); }
        }


        private ObservableCollection<GraphicsItem> items = new() { new GraphicsLineItem() { Pos = new Point(100, 100) }, new StartNode() { Pos = new Point(100, 100) } };

        public ObservableCollection<GraphicsItem> Items
        {
            get { return items; }
            set { items = value; NotifyOfPropertyChange(); }
        }

        public void AddSomeThingToGraphicsView()
        {
            Items.Add(new WorkflowNodeBase{ Pos = new System.Windows.Point((new Random().Next(0, 500)), (new Random().Next(0, 500))) });
        }
    }
}
