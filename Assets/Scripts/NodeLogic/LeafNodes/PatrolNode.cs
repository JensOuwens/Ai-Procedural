using UnityEngine;

public class PatrolNode : LeafNode
{
    public override NodeStatus currentStatus { get; set; }
    public override AgentContext agentContext { get; set; }

    private BehaviourTreeManager tree;
    
    public PatrolNode(AgentContext agentContext, BehaviourTreeManager tree)
    {
        this.agentContext = agentContext;
        this.tree = tree;
    }
    public override void Execute()
    {
        tree.SetState("Patrol");
        agentContext.CallAgentPatrolBehaviour(this);

        if (agentContext.blackBoard.SeePlayer)
        {
            UpdateStatus(NodeStatus.Failed);
            return;
        }

        UpdateStatus(NodeStatus.Running);
    }

    public override void Reset()
    {
        agentContext.StopAgentPatrolBehaviour();
    }
}
