using System.Collections.Generic;
using UnityEngine;

public class AgentPickUpManager
{
    private AgentMovementManager movement;
    private List<Weapon> weapons;
    private IAgent agent;
    private Transform agentTransform;

    private Weapon targetWeapon;
    private float pickupRange = 1.5f;

    public AgentPickUpManager(AgentMovementManager movement, IAgent agent, List<Weapon> weapons, Transform agentTransform)
    {
        this.movement = movement;
        this.agent = agent;
        this.weapons = weapons;
        this.agentTransform = agentTransform;
    }

    public NodeStatus Tick()
    {
        if (agent.HasWeapon)
            return NodeStatus.Completed;

        if (weapons == null || weapons.Count == 0)
            return NodeStatus.Failed;

        if (targetWeapon == null)
            targetWeapon = GetNearestWeapon();

        if (targetWeapon == null)
            return NodeStatus.Failed;

        movement.Move(targetWeapon.transform.position);

        float dist = Vector3.Distance(agentTransform.position, targetWeapon.transform.position);

        if (dist <= pickupRange)
        {
            agent.HasWeapon = true;
            weapons.Remove(targetWeapon);
            targetWeapon.DestroyThisWeapon();

            targetWeapon = null;
            return NodeStatus.Completed;
        }

        return NodeStatus.Running;
    }

    private Weapon GetNearestWeapon()
    {
        Weapon nearest = null;
        float minDist = float.MaxValue;

        foreach (var weapon in weapons)
        {
            float dist = Vector3.Distance(agentTransform.position, weapon.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = weapon;
            }
        }

        return nearest;
    }

    public void Stop()
    {
        movement.Stop();
        targetWeapon = null;
    }
}
