using ATE.Core.Mvvm;
using ATE.GraphicsFramework;
using ATE.GraphicsFramework.GraphicsBaseItems;
using Caliburn.Micro;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ATE.ViewModels
{
    public class TestingViewModel : PropertyChangedBase, IViewModel
    {
        DispatcherTimer DispatcherTimer;
        public TestingViewModel()
        {
            MyTitle = "Start;";
            DispatcherTimer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };

            DispatcherTimer.Tick += (object? sender, EventArgs e) =>
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


        private ObservableCollection<GraphicsItem> items = new() { new GraphicsLineItem() };

        public ObservableCollection<GraphicsItem> Items
        {
            get { return items; }
            set { items = value; NotifyOfPropertyChange(); }
        }

        private string myTitle;

        public string MyTitle
        {
            get { return myTitle; }
            set { myTitle = value; NotifyOfPropertyChange(); }
        }


        public void AddSomeThingToGraphicsView()
        {
            Items.Add(new GraphicsLineItem () { Pos = new System.Windows.Point((new Random().Next(0, 500)), (new Random().Next(0, 500))) });
            //Items.Add(new GraphicsLineItem () { Pos = new System.Windows.Point((new Random().Next(0, 500)), (new Random().Next(0, 500))) });
            //Items.Add(new GraphicsLineItem () { Pos = new System.Windows.Point((new Random().Next(0, 500)), (new Random().Next(0, 500))) });
            //Items.Add(new GraphicsLineItem () { Pos = new System.Windows.Point((new Random().Next(0, 500)), (new Random().Next(0, 500))) });
            //Items.Add(new GraphicsLineItem () { Pos = new System.Windows.Point((new Random().Next(0, 500)), (new Random().Next(0, 500))) });
            //Items.Add(new GraphicsLineItem () { Pos = new System.Windows.Point((new Random().Next(0, 500)), (new Random().Next(0, 500))) });
            Text = "成功添加";
            MyTitle += " ";
        }
    }
}
