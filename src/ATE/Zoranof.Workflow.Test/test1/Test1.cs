using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using Zoranof.WorkFlow.Base;

namespace Zoranof.Workflow.Test.test1
{

    public class Test1Workflow : IWorkflow<Dictionary<string, object>>
    {
        public string Id => "Test1";

        public int Version => 1;

        public void Build(IWorkflowBuilder<Dictionary<string, object>> builder)
        {
            builder
                .StartWith<ConstantNode>()
                    .Output((step, data) => data.Add("A", step.OutA))
                    .Output((step, data) => data.Add("B", step.OutB))
                .Then<AddStep>()
                    .Input((step, data) => step.A = (int)(long)data["A"])
                    .Input((step, data) => step.B = (int)(long)data["B"])
            .Output((step, data) => data.Add("Result", step.Out))
            .Then<ResultStep>()
                .Input((step, data) => step.Result = (int)(long)data["Result"]);
        }
    }

}
