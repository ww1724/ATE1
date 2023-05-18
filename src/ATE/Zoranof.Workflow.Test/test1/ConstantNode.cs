using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Zoranof.Workflow.Test.test1
{
    public class ConstantNode : StepBody
    {
        public int OutA { get; set; }
        public int OutB { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            OutA = 10;
            OutB = 20;
            return ExecutionResult.Next();
        }
    }
}
