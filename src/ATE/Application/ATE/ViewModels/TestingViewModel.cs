using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATE.ViewModels
{
    public class TestingViewModel : ReactiveObject, IRoutableViewModel
    {
        public TestingViewModel(IScreen screen)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
        }

        public string UrlPathSegment => "Main.TestingView";

        public IScreen HostScreen { get; }
    }
}
