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
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            IWorkflowHost host = ServiceProvider.GetService<IWorkflowHost>();
            await Task.Run(() => { host.Start(); });
            host.RegisterWorkflow<Test1Workflow, MyData>();
            await host.StartWorkflow("Test1", 1, new MyData() { });
        }
    }

    public class MyData
    {

        public int Result;

        public Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
    }

    public class Test1Workflow : IWorkflow<MyData>
    {
        public string Id => "Test1";

        public int Version => 1;

        public void Build(IWorkflowBuilder<MyData> builder)
        {
            builder
                .StartWith<ConstantNode>()
                    .Output(data => data.keyValuePairs["A"], step => step.OutA)
                    .Output(data => data.keyValuePairs["B"], step => step.OutB)
                .Then<AddStep>()
                    .Input(step => step.A, data => data.keyValuePairs["A"])
                    .Input(step => step.B, data => data.keyValuePairs["B"])
                    .Output(data => data.keyValuePairs["Result"], step => step.Out)
                .Then<ResultStep>()
                    .Input(step => step.Result, data => data.keyValuePairs["Result"]);
        }
    }
}
