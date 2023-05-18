using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Zoranof.WorkFlow.Base
{
    public class ResultStep : StepBody
    {
        public int Result;
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine($"Result is {Result}");
            Task.Delay(10000).Wait();
            return ExecutionResult.Next();
        }
    }

    public class ResultNode : WorkflowNode
    {
    }
}
