using UnityEngine;

public class WeaponSensor
{
    public bool Sense(Transform target)
    {
        if (target == null) return false;

        GuardAgent agent = target.GetComponent<GuardAgent>();
        if (agent == null) return false;

        return agent.HasWeapon;
    }
}
