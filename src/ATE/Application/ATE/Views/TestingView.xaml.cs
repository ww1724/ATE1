using ATE.Controls;
using ATE.ViewModels;
using ReactiveUI;

namespace ATE.Views
{

    public class TestingViewBase : ReactiveUserControl<TestingViewModel> { }
    /// <summary>
    /// TestingView.xaml 的交互逻辑
    /// </summary>
    /// 
    public partial class TestingView : TestingViewBase
    {
        public TestingView()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, x => x.UrlPathSegment, x => x.urlpathtextbox.Text);
            });
        }
    }
}
