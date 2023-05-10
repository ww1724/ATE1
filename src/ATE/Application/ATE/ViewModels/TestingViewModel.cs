using ATE.Core.Mvvm;
using ATE.GraphicsFramework;
using ATE.GraphicsFramework.GraphicsBaseItems;
using Caliburn.Micro;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace ATE.ViewModels
{
    public class TestingViewModel : PropertyChangedBase, IViewModel
    {     
        public TestingViewModel()
        {
            MyTitle = "Start;";
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
            Items.Add(new GraphicsLineItem ());
            Text += "成功添加";
            MyTitle += "你好";
        }
    }
}
