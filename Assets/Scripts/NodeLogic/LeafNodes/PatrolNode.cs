using UnityEngine;

public class PatrolNode : LeafNode
{
    public override NodeStatus currentStatus { get; set; }
    public override AgentContext agentContext { get; set; }
    
    public PatrolNode(AgentContext agentContext)
    {
        this.agentContext = agentContext;
    }
    public override void Execute()
    {
        if (currentStatus != NodeStatus.Running)
            currentStatus = NodeStatus.Running;
        
        agentContext.CallAgentPatrolBehaviour(this);
    }

    public override void Reset()
    {
        agentContext.StopAgentPatrolBehaviour();
    }
}
