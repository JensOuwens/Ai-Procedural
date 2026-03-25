using UnityEngine;

public class BlackBoard : MonoBehaviour
{
    public bool hasWeapon;
    public bool SeePlayer;
    public float DistanceToPlayer;
    public bool needsWeapon;
    public bool HasLastKnownPosition;

    [SerializeField] private LayerMask playerMask;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject GuardAgent;

    private VisionSensor visionSensor;
    private WeaponSensor weaponSensor;
    private DistanceToPlayerSensor distanceToPlayerSensor;

    // Visualization settings
    [SerializeField, Range(1, 50)] private int coneResolution = 20;
    [SerializeField] private Color coneColor = Color.yellow;
    
    public Vector3 lastKnownPlayerPosition;

    private void Awake()
    {
        distanceToPlayerSensor = new DistanceToPlayerSensor();
        weaponSensor = new WeaponSensor();
        visionSensor = new VisionSensor(20f, 70f, playerMask);
    }

    private void Update()
    {
        if (GuardAgent == null || player == null) return;

        hasWeapon = weaponSensor.Sense(GuardAgent.transform);
        SeePlayer = visionSensor.Sense(GuardAgent.transform, player.transform);
        DistanceToPlayer = distanceToPlayerSensor.Sense(GuardAgent.transform, player.transform);
        
        if (visionSensor.Sense(GuardAgent.transform, player.transform))
        {
            SeePlayer = true;
            lastKnownPlayerPosition = player.transform.position;
            HasLastKnownPosition = true;
        }
        else
        {
            SeePlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (GuardAgent == null) return;
    
        // Draw Vision Cone
        Vector3 originPos = GuardAgent.transform.position;
        Vector3 forward = GuardAgent.transform.forward;
    
        float step = visionSensor.Angle / coneResolution;
    
        for (int i = 0; i <= coneResolution; i++)
        {
            float currentAngle = -visionSensor.Angle / 2 + step * i;
            Vector3 rayDir = Quaternion.Euler(0, currentAngle, 0) * forward;
            Gizmos.color = coneColor;
            Gizmos.DrawRay(originPos, rayDir * visionSensor.Radius);
        }
    
        // Draw line to player if detected
        if (SeePlayer && player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(originPos, player.transform.position);
        }
    }
}