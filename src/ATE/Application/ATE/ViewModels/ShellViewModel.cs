using ATE.Mvvm;
using Caliburn.Micro;

namespace ATE.ViewModels
{
    public class ShellViewModel : Conductor<object>, IViewModel
    {
        public ShellViewModel()
        {
            ActivateItemAsync(new TestingViewModel());
        }


    }
}
