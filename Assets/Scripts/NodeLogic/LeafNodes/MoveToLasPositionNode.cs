using UnityEngine;

public class MoveToLastKnownPositionNode : LeafNode
{
    public override NodeStatus currentStatus { get; set; }
    public override AgentContext agentContext { get; set; }

    private BlackBoard blackboard;
    private float reachDistance = 1.5f;

    private BehaviourTreeManager tree;

    public MoveToLastKnownPositionNode(AgentContext context, BlackBoard blackboard, BehaviourTreeManager tree)
    {
        this.agentContext = context;
        this.blackboard = blackboard;
        this.tree = tree;
    }

    public override void Execute()
    {
        //tree.SetState("Go to last seen player position");
        agentContext.MoveToPosition(blackboard.lastKnownPlayerPosition);

        float dist = Vector3.Distance(
            agentContext.GetAgentPosition(),
            blackboard.lastKnownPlayerPosition
        );

        if (dist <= reachDistance)
        {
            UpdateStatus(NodeStatus.Completed);
            return;
        }

        UpdateStatus(NodeStatus.Running);
    }

    public override void Reset()
    {
        agentContext.StopMovement();
    }
}
