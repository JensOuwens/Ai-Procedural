using UnityEngine;

public class AgentContext : MonoBehaviour
{
    [SerializeField] private AgentAttackManager agentAttackManager;
    [SerializeField] private AgentMovementManager agentMovementManager;
    [SerializeField] private AgentPickUpManager agentPickUpManager;
    [SerializeField] private AgentPatrolBehaviour patrolBehaviour;

    public void CallAgentAttackBehaviour(Node callBackNode)
    {
        //Update status, for loop, check for status in agentattackbehavour, then call it in the node
    }

    public void CallAgentMovementBehaviour(Node callBackNode)
    {
        //Update status, for loop, check for status in CallAgentMovementBehaviour, then call it in the node
    }

    public void CallAgentPickUpBehaviour(Node callBackNode)
    {
        //Update status, for loop, check for status in CallAgentPickUpBehaviour, then call it in the node
    }

    public void CallAgentPatrolBehaviour(Node callBackNode)
    {
        //Update status, for loop, check for status in CallAgentPatrolBehaviour, then call it in the node
    }

    public void StopAgentAttackBehaviour()
    {
        
    }
    
    public void StopAgentMovementBehaviour()
    {
        
    }   
    
    public void StopAgentPickUpBehaviour()
    {
        
    }  
    
    public void StopAgentPatrolBehaviour()
    {
        
    }
    
    
    
    
}
