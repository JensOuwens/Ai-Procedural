using UnityEngine;

public class VisionSensor
{
    public float Radius;
    public float Angle;
    public LayerMask ObstructionMask;
    public LayerMask PlayerMask;

    public VisionSensor(float radius, float angle, LayerMask obstructionMask, LayerMask playerMask)
    {
        Radius = radius;
        Angle = angle;
        ObstructionMask = obstructionMask;
        PlayerMask = playerMask;
    }

    public bool Sense(Transform origin, Transform target)
    {
        Vector3 originEye = origin.position + Vector3.up * 1f;
        Vector3 targetPos = target.position + Vector3.up * 1f;
        Vector3 toTarget = targetPos - originEye;

        float dist = new Vector3(toTarget.x, 0, toTarget.z).magnitude;
        if (dist > Radius) return false;

        Vector3 forwardFlat = new Vector3(origin.forward.x, 0, origin.forward.z).normalized;
        Vector3 toTargetFlat = new Vector3(toTarget.x, 0, toTarget.z).normalized;
        if (Vector3.Angle(forwardFlat, toTargetFlat) > Angle * 0.5f)
            return false;

        if (Physics.Raycast(originEye, toTarget.normalized, toTarget.magnitude, ObstructionMask))
        {
            return false; 
        }


        RaycastHit hit;
        if (Physics.Raycast(originEye, toTarget.normalized, out hit, toTarget.magnitude, PlayerMask))
        {
            if (hit.transform == target)
                return true; 
        }

        return false; 
    }
}