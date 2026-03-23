using System;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovementManager
{
    private NavMeshAgent navmeshAgent;
    private Vector3 destination;

    public AgentMovementManager(NavMeshAgent navmeshAgent){
        this.navmeshAgent = navmeshAgent;
    }
    
    public void Move(Vector3 destination)
    {
        this.destination = destination;
        navmeshAgent.SetDestination(destination);
    }

    public void Stop()
    {
        navmeshAgent.isStopped = false;
        navmeshAgent.SetDestination(destination);
    }
}
