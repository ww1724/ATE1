using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using Zoranof.WorkFlow;

namespace Zoranof.Workflow.Test.test2
{
    public class ResultStep : StepBody
    {
        public object Result = 0;

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine($"Result is {(int)Result}");
            Task.Delay(10000).Wait();
            return ExecutionResult.Next();
        }
    }

    public class ResultNode : WorkflowNode
    {
    }
}
