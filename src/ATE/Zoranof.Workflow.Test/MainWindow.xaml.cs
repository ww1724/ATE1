using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
using WorkflowCore.Interface;
using Zoranof.Workflow.Test.test1;
using Zoranof.WorkFlow.Base;

namespace Zoranof.Workflow.Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        IServiceProvider ServiceProvider
            => (Application.Current as App).serviceProvider;


        IWorkflowHost host =>
            (Application.Current as App).serviceProvider.GetService<IWorkflowHost>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeWorkflowComponent();
        }

        private async void InitializeWorkflowComponent()
        {
            await Task.Run(() => { 
                host.Start(); 
            });

            // test 1 
            host.RegisterWorkflow<Test1Workflow, Dictionary<string, object>>();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await host.StartWorkflow("Test1", 1, new Dictionary<string, object> { });
        }
    }

    public class MyData
    {

        public int Result;

        public Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
    }



}
