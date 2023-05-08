using ATE.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ATE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : Window, IViewFor<ShellViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
            .Register(nameof(ViewModel), typeof(ShellViewModel), typeof(Shell));

        public ShellViewModel ViewModel
        {
            get => (ShellViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (ShellViewModel)value;
        }

        public Shell()
        {
            InitializeComponent();
            InitializeDataContext();
        }

        private void InitializeDataContext()
        {
            ViewModel = new ShellViewModel();
            DataContext = ViewModel;
            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, x => x.Router, x => x.RoutedViewHost.Router)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, x => x.GoNext, x => x.GoNextButton.Command)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, x => x.GoBack, x => x.GoBackButton.Command)
                    .DisposeWith(disposables);

            });
        }

        private void MinimizeWindow_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeWindow_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
