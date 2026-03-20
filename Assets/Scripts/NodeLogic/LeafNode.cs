using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LeafNode : Node
{
    public override AgentContext agentContext { get; set; }
    private int agentContextID;
    private Blackboard blackboard;
    
    public override NodeStatus currentStatus { get; set; }
    
    public LeafNode(AgentContext agentContext, int agentContextID, Blackboard blackboard)
    {
        this.agentContext = agentContext;
        this.agentContextID = agentContextID;
        this.blackboard = blackboard;
    }
    
    public override void Execute()
    {
        switch (agentContextID)
        {
            case  0:
                agentContext.CallAgentAttackBehaviour(this);
                break;
            case 1:
                agentContext.CallAgentMovementBehaviour(this);
                break;
            case 2:
                agentContext.CallAgentPickUpBehaviour(this);
                break;
        }
    }

    public override void Reset()
    {
        
    }

    public void UpdateStatus(NodeStatus NewStatus)
    {
        currentStatus =  NewStatus;
    }
}
