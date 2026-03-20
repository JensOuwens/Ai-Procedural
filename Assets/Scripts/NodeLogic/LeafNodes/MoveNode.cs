using UnityEngine;

public class MoveNode : LeafNode
{
    public override NodeStatus currentStatus { get; set; }
    public override AgentContext agentContext { get; set; }
    
    public MoveNode(AgentContext agentContext)
    {
        this.agentContext = agentContext;
    }
    public override void Execute()
    {
        if (currentStatus != NodeStatus.Running)
            currentStatus = NodeStatus.Running;
        
        agentContext.CallAgentMovementBehaviour(this);
    }

    public override void Reset()
    {
        agentContext.StopAgentMovementBehaviour();
    }
}
