using ATE.Core;
using ATE.Core.Model;
using ATE.Core.Mvvm;
using ATE.Core.Stores;
using Caliburn.Micro;
using System.Reflection.Metadata;

namespace ATE.ViewModels
{
    public class ShellViewModel : Conductor<object>, IViewModel
    {
        public MenuStore MenuStore
            => (MenuStore)IoC.Get<SimpleContainer>().GetInstance<IViewModel>(Constants.MenuStore);

        public MenuStore TestingStore
            => (MenuStore)IoC.Get<SimpleContainer>().GetInstance<IViewModel>(Constants.TestingStore);  

        public IWindowManager WindowManager
            => IoC.Get<IWindowManager>();

        public ShellViewModel()
        {
            ActivateItemAsync(new TestingViewModel());

            MenuStore.PropertyChanged += MenuStore_PropertyChanged;
        }


        private void MenuStore_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentMenuItem")
                ActivateItemAsync(IoC.Get<SimpleContainer>().GetInstance<IViewModel>(MenuStore.CurrentMenuItem.Path));
        }

        public MenuItem CurrentMenuItem
        {
            get { return MenuStore.CurrentMenuItem; }
        }

        public void MenuItemChangedAction(MenuItem menuItem)
        {

        }


        #region Actions
        public void LoadTestingCodeAction()
        {
            WindowManager.ShowDialogAsync(new LoadTestingCodeViewModel());
        }

        public void NavigationTo(string path)
        {
            ActivateItemAsync(IoC.Get<SimpleContainer>().GetInstance<IViewModel>(path));
        }
        #endregion
    }
}
