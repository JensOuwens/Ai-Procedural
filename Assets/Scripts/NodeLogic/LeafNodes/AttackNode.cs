using UnityEngine;

public class AttackNode : LeafNode
{
    public override NodeStatus currentStatus { get; set; }
    public override AgentContext agentContext { get; set; }
    
    public AttackNode(AgentContext agentContext)
    {
        this.agentContext = agentContext;
    }
    public override void Execute()
    {
        if (currentStatus != NodeStatus.Running)
            currentStatus = NodeStatus.Running;
        
        agentContext.CallAgentAttackBehaviour(this);
    }

    public override void Reset()
    {
        agentContext.StopAgentAttackBehaviour();
    }
}
