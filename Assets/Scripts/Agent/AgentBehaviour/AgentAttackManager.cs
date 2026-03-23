using UnityEngine;

public class AgentAttackManager
{
    private AgentMovementManager movement;
    private Transform agentTransform;
    private Transform playerTransform;

    private float attackRange = 2f;
    private float attackCooldown = 1f;
    private float lastAttackTime;

    public AgentAttackManager(AgentMovementManager movement, Transform agentTransform, Transform playerTransform)
    {
        this.movement = movement;
        this.agentTransform = agentTransform;
        this.playerTransform = playerTransform;
    }

    public NodeStatus Tick()
    {
        if (playerTransform == null)
            return NodeStatus.Failed;

        float distance = Vector3.Distance(agentTransform.position, playerTransform.position);

        if (distance > attackRange)
        {
            movement.Move(playerTransform.position);
            return NodeStatus.Running;
        }

        movement.Stop();

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            var damageable = playerTransform.GetComponent<IDamageable>();
            if (damageable == null)
                return NodeStatus.Failed;

            damageable.DealDamage(10);
            lastAttackTime = Time.time;
        }

        return NodeStatus.Running; // still attacking loop
    }

    public void Stop()
    {
        movement.Stop();
    }
}