using ATE.Core.Model;
using ATE.Core.Mvvm;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace ATE.Core.Stores
{


    public class MenuStore : Conductor<object>, IViewModel
    {
        private ObservableCollection<MenuItem>  appMenus;
        private MenuItem                        currentMenuItem;

        public ObservableCollection<MenuItem>   AppMenus { get => appMenus; set { appMenus = value; NotifyOfPropertyChange(); } }

        public MenuItem                         CurrentMenuItem { get => currentMenuItem; set { currentMenuItem = value; NotifyOfPropertyChange(); } }


        public MenuStore()
        {
            AppMenus = new() {
                new MenuItem { Text="Testing Board", Path=Constants.TestingBoardView, CanClose=false },
                new MenuItem { Text="Console", Path=Constants.ConsoleView }
            };

            CurrentMenuItem = AppMenus[0];
        }
    }
}
