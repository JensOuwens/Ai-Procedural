using UnityEngine;

public class MoveToLastKnownPositionNode : LeafNode
{
    public override NodeStatus currentStatus { get; set; }
    public override AgentContext agentContext { get; set; }

    private BlackBoard blackboard;
    private float reachDistance = 1.5f;

    public MoveToLastKnownPositionNode(AgentContext context, BlackBoard blackboard)
    {
        this.agentContext = context;
        this.blackboard = blackboard;
    }

    public override void Execute()
    {
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
