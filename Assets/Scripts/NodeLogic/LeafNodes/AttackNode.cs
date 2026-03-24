using UnityEngine;

public class AttackNode : LeafNode
{
    public override NodeStatus currentStatus { get; set; }
    public override AgentContext agentContext { get; set; }

    private BehaviourTreeManager tree;
    
    public AttackNode(AgentContext agentContext, BehaviourTreeManager tree)
    {
        this.agentContext = agentContext;
        this.tree = tree;
    }
    public override void Execute()
    {
        //tree.SetState("Attacking");
        agentContext.CallAgentAttackBehaviour(this);
        UpdateStatus(NodeStatus.Running);
    }

    public override void Reset()
    {
        agentContext.StopAgentAttackBehaviour();
    }
}
