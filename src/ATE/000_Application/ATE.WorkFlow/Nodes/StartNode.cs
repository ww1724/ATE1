using WorkflowCore.Interface;
using WorkflowCore.Models;
using Zoranof.WorkFlow;

namespace ATE.WorkFlow.Nodes
{
    public class StartStep : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class StartNode : WorkflowNode
    {

    }

}
