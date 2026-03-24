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
        agentContext.CallAgentAttackBehaviour(this);
        UpdateStatus(NodeStatus.Running);
    }

    public override void Reset()
    {
        agentContext.StopAgentAttackBehaviour();
    }
}
