using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;

namespace Zoranof.Workflow.Test.test2
{
    public class Test2 : IWorkflow
    {
        public string Id => "Test2";

        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {
            
        }
    }
}
