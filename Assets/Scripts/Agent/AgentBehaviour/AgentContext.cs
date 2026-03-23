using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentContext : MonoBehaviour
{
    private AgentAttackManager agentAttackManager;
    private AgentMovementManager agentMovementManager;
    private AgentPickUpManager agentPickUpManager;
    private AgentPatrolBehaviour patrolBehaviour;
    
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform agentTransform;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GuardAgent agent;
    [SerializeField] private List<Weapon> weapons;
    [SerializeField] private List<Vector3> patrolPositions;
    
    public BlackBoard blackBoard;

    private void Awake()
    {
        agentMovementManager = new AgentMovementManager(navMeshAgent);
        agentAttackManager = new AgentAttackManager(agentMovementManager, agentTransform, playerTransform);
        agentPickUpManager = new AgentPickUpManager(agentMovementManager, agent, weapons, agentTransform);
        patrolBehaviour = new AgentPatrolBehaviour(agentMovementManager, patrolPositions);
    }
    
    public void CallAgentAttackBehaviour(Node callBackNode)
    {
        StopAgentPatrolBehaviour();
        StopAgentPickUpBehaviour();
        
        NodeStatus status = agentAttackManager.Tick();
        callBackNode.currentStatus = status;
    }

    public void CallAgentPickUpBehaviour(Node callBackNode)
    {
        StopAgentAttackBehaviour();
        StopAgentPatrolBehaviour();
        
        NodeStatus status = agentPickUpManager.Tick();
        callBackNode.currentStatus = status;
    }

    public void CallAgentPatrolBehaviour(Node callBackNode)
    {
        StopAgentAttackBehaviour();
        StopAgentPickUpBehaviour();
        
        NodeStatus status = patrolBehaviour.Tick();
        callBackNode.currentStatus = status;
    }

    public void StopAgentAttackBehaviour()
    {
        agentAttackManager.Stop();
    }
    
    public void StopAgentPickUpBehaviour()
    {
        agentPickUpManager.Stop();
    }  
    
    public void StopAgentPatrolBehaviour()
    {
        patrolBehaviour.Stop();
    }
    
    public void MoveToPosition(Vector3 position)
    {
        agentMovementManager.Move(position);
    }

    public void StopMovement()
    {
        agentMovementManager.Stop();
    }

    public Vector3 GetAgentPosition()
    {
        return agentTransform.position;
    }
    
    
}
