using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class AgentPatrolBehaviour
{
    private AgentMovementManager movement;
    private List<Vector3> patrolSpots;

    private int currentIndex = -1;
    private float reachDistance = 1f;

    public AgentPatrolBehaviour(AgentMovementManager movement, List<Vector3> patrolSpots)
    {
        this.movement = movement;
        this.patrolSpots = patrolSpots;
    }

    public NodeStatus Tick()
    {
        if (patrolSpots == null || patrolSpots.Count == 0)
            return NodeStatus.Failed;

        if (currentIndex == -1)
            currentIndex = Random.Range(0, patrolSpots.Count);

        Vector3 target = patrolSpots[currentIndex];
        movement.Move(target);

        float dist = Vector3.Distance(target, movementPosition());

        if (dist <= reachDistance)
        {
            currentIndex = Random.Range(0, patrolSpots.Count);
        }

        return NodeStatus.Running;
    }

    private Vector3 movementPosition()
    {
        return movement.GetType()
            .GetField("navmeshAgent", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(movement) is NavMeshAgent agent ? agent.transform.position : Vector3.zero;
    }

    public void Stop()
    {
        movement.Stop();
    }
}
