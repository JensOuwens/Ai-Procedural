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

        Vector3 originEye = originPos + Vector3.up * 1f;
        Vector3 targetPos = target.position + Vector3.up * 1f;

        Vector3 toTarget = targetPos - originEye;

        float dist = new Vector3(toTarget.x, 0, toTarget.z).magnitude;
        if (dist > Radius) return false;

        Vector3 forwardFlat = new Vector3(forward.x, 0, forward.z).normalized;
        Vector3 toTargetFlat = new Vector3(toTarget.x, 0, toTarget.z).normalized;

        if (Vector3.Angle(forwardFlat, toTargetFlat) > Angle * 0.5f)
            return false;

        if (Physics.Raycast(originEye, toTarget.normalized, toTarget.magnitude, ObstructionMask))
            return false;

        return true;
    }
}
