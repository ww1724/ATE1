using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using Zoranof.WorkFlow;
using Zoranof.WorkFlow.Common;

namespace Zoranof.Workflow.Test.test2
{
    public class AddStep : StepBody
    {
        public int A { get; set; }

        public int B { get; set; }

        public int Out { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
           var data = context.Workflow.Data;
            var b = context.Step.Name;
            Out = A + B;
            Console.WriteLine($"Calculate { A } Add { B } = { Out }");

            return ExecutionResult.Next();
        }
    }

    public class AddNode : WorkflowNode
    {
        public AddNode() : base()
        {
            InputOptions.Add(new ExecNodeOption(this) { Text = "132456" });
        }
    }
}
