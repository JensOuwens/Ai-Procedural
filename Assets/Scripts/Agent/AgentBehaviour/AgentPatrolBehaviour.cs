using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentPatrolBehaviour
{
    private AgentMovementManager movement;
    private List<Vector3> patrolSpots;

    private int currentIndex = -1;
    private float reachDistance = 1f;      
    private float reachBuffer = 0.5f;      

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
        {
            return NodeStatus.Failed;
        }


        if (currentIndex == -1)
        {
            currentIndex = Random.Range(0, patrolSpots.Count);
        }

        Vector3 target = patrolSpots[currentIndex];
        Vector3 currentPos = movementPosition();


        float reachSqr = (reachDistance + reachBuffer) * (reachDistance + reachBuffer);
        if (!isWaiting && (target - currentPos).sqrMagnitude <= reachSqr)
        {
            isWaiting = true;
            waitTimer = 0f;
            movement.Stop();
            return NodeStatus.Running;
        }
        
        if (isWaiting)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitTime)
            {
                waitTimer = 0f;
                isWaiting = false;

                int nextIndex = GetNextIndex();
                currentIndex = nextIndex;
            }

            return NodeStatus.Running;
        }

        movement.Move(target);

        return NodeStatus.Running;
    }

    private int GetNextIndex()
    {
        if (patrolSpots.Count == 1)
            return 0;

        int newIndex;
        int attempts = 0;
        do
        {
            newIndex = Random.Range(0, patrolSpots.Count);
            attempts++;
            if (attempts > 10)
            {
                break;
            }
        } 
        while (newIndex == currentIndex);

        return newIndex;
    }

    private Vector3 movementPosition()
    {
        var navAgentField = movement.GetType().GetField("navmeshAgent", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (navAgentField == null) return Vector3.zero;

        if (navAgentField.GetValue(movement) is NavMeshAgent agent)
            return agent.transform.position;

        return Vector3.zero;
    }

    public void Stop()
    {
        movement.Stop();
        isWaiting = false;
        waitTimer = 0f;
    }
}