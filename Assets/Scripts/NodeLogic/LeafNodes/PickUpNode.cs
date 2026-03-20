using UnityEngine;

public class PickUpNode : LeafNode
{
    public override NodeStatus currentStatus { get; set; }
    public override AgentContext agentContext { get; set; }
    
    public PickUpNode(AgentContext agentContext)
    {
        this.agentContext = agentContext;
    }
    public override void Execute()
    {
        if (currentStatus != NodeStatus.Running)
            currentStatus = NodeStatus.Running;
        
        agentContext.CallAgentPickUpBehaviour(this);
    }

    public override void Reset()
    {
        agentContext.StopAgentPickUpBehaviour();
    }
}
