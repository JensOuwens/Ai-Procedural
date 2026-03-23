using UnityEngine;

public class DistanceToPlayerSensor
{
    public float Sense(Transform origin, Transform target)
    {
        if (origin == null || target == null) return float.MaxValue;

        return Vector3.Distance(origin.position, target.position);
    }
}
