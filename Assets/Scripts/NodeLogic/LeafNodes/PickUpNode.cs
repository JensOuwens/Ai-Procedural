using UnityEngine;

public class PickUpNode : LeafNode
{
    public override NodeStatus currentStatus { get; set; }
    public override AgentContext agentContext { get; set; }

    private BehaviourTreeManager tree;
    
    public PickUpNode(AgentContext agentContext, BehaviourTreeManager tree)
    {
        this.agentContext = agentContext;
        this.tree = tree;
    }
    public override void Execute()
    {
        tree.SetState("PickUpWeapon");
        
        if (currentStatus != NodeStatus.Running)
            currentStatus = NodeStatus.Running;
        
        agentContext.CallAgentPickUpBehaviour(this);
    }

    public override void Reset()
    {
        agentContext.StopAgentPickUpBehaviour();
    }
}
