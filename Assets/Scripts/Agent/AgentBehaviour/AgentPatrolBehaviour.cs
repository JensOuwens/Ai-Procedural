using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentPatrolBehaviour
{
    private AgentMovementManager movement;
    private List<Vector3> patrolSpots;

    private int currentIndex = -1;
    private float reachDistance = 1f;

    private float waitTime = 1f;
    private float waitTimer = 0f;
    private bool isWaiting = false;

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

        // --- WAITING STATE ---
        if (isWaiting)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitTime)
            {
                isWaiting = false;
                waitTimer = 0f;
                currentIndex = GetNextIndex();
            }

            return NodeStatus.Running;
        }

        // --- MOVING STATE ---
        movement.Move(target);

        float dist = Vector3.Distance(target, movementPosition());

        if (dist <= reachDistance)
        {
            isWaiting = true;
            movement.Stop(); // stop while waiting
        }

        return NodeStatus.Running;
    }

    private int GetNextIndex()
    {
        if (patrolSpots.Count == 1)
            return 0;

        int newIndex;
        do
        {
            newIndex = Random.Range(0, patrolSpots.Count);
        } 
        while (newIndex == currentIndex);

        return newIndex;
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