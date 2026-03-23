using System;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovementManager : MonoBehaviour
{
    [SerializeField] NavMeshAgent navmeshAgent;

    public void Move(Vector3 destination)
    {
        navmeshAgent.SetDestination(destination);
    }

    public void Stop()
    {
        navmeshAgent.SetDestination(navmeshAgent.destination);
    }
}
