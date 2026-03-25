using UnityEngine;

public class MoveToLastKnownPositionNode : LeafNode
{
    public override NodeStatus currentStatus { get; set; }
    public override AgentContext agentContext { get; set; }

    private BlackBoard blackboard;
    private float reachDistance = 1.5f;

    private BehaviourTreeManager tree;
    
    private float waitTime = 1f;
    private float waitTimer = 0f;
    private bool isWaiting = false;

    public MoveToLastKnownPositionNode(AgentContext context, BlackBoard blackboard, BehaviourTreeManager tree)
    {
        this.agentContext = context;
        this.blackboard = blackboard;
        this.tree = tree;
    }

    public override void Execute()
    {
        tree.SetState("Searching Last Position");

        if (!blackboard.HasLastKnownPosition)
        {
            UpdateStatus(NodeStatus.Failed);
            return;
        }

        if (isWaiting)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitTime)
            {
                waitTimer = 0f;
                isWaiting = false;
                blackboard.HasLastKnownPosition = false;
                UpdateStatus(NodeStatus.Completed);
            }
            else
            {
                UpdateStatus(NodeStatus.Running);
            }

            return;
        }

        agentContext.MoveToPosition(blackboard.lastKnownPlayerPosition);

        float dist = Vector3.Distance(
            agentContext.GetAgentPosition(),
            blackboard.lastKnownPlayerPosition
        );

        if (dist <= reachDistance)
        {
            isWaiting = true;
            agentContext.StopMovement();
        }

        UpdateStatus(NodeStatus.Running);
    }

    public override void Reset()
    {
        agentContext.StopMovement();
    }
}
