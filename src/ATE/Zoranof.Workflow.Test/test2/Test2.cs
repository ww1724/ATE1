using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using WorkflowCore.Interface;
using WorkflowCore.Services.DefinitionStorage;

namespace Zoranof.Workflow.Test.test2
{
    public static class Test2Helper
    {
        public static string Id => "Test2";

        public static void RegisterTest2Workflow(this IWorkflowHost host, IServiceProvider serviceProvider)
        {
            IDefinitionLoader loader = serviceProvider.GetService<IDefinitionLoader>();
            string jsonText = File.ReadAllText("test2/test2.json");

            if (jsonText == "")
            {
                MessageBox.Show("Test2.Json Nof Found Or Null Content");
                return;
            }
            loader.LoadDefinition(jsonText, Deserializers.Json);

        }

        public static void StartTest2Workflow(this IWorkflowHost host)
        {
            Console.WriteLine("Demo2: Workflow Module Build Flow Object With Json String");
            host.StartWorkflow("Test2", 1, new Dictionary<string, object>());
        }
    }

}
