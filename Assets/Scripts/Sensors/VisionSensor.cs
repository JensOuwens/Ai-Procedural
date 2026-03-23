using UnityEngine;

public class VisionSensor
{
    public float Radius;
    public float Angle;
    public LayerMask ObstructionMask;

    public VisionSensor(float radius, float angle, LayerMask obstructionMask)
    {
        Radius = radius;
        Angle = angle;
        ObstructionMask = obstructionMask;
    }

    public bool Sense(Transform origin, Transform target)
    {
        Vector3 originPos = origin.position;
        Vector3 forward = origin.forward;

        Vector3 toTarget = target.position - originPos;
        float dist = toTarget.magnitude;

        // Distance
        if (dist > Radius)
            return false;

        Vector3 dirToTarget = toTarget / dist;

        // Angle
        if (Vector3.Angle(forward, dirToTarget) > Angle * 0.5f)
            return false;

        // Obstruction
        if (Physics.Raycast(originPos, dirToTarget, dist, ObstructionMask))
            return false;

        // --- Visualization ---
        Vector3 leftBoundary = Quaternion.Euler(0, -Angle * 0.5f, 0) * forward;
        Vector3 rightBoundary = Quaternion.Euler(0, Angle * 0.5f, 0) * forward;

        Debug.DrawRay(originPos, forward * Radius, Color.green);
        Debug.DrawRay(originPos, leftBoundary * Radius, Color.yellow);
        Debug.DrawRay(originPos, rightBoundary * Radius, Color.yellow);

        Debug.DrawLine(originPos, target.position, Color.red);

        return true;
    }
}
