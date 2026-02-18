using System;
using UnityEngine;
using System.Runtime.InteropServices;

public class BoidComputeController : MonoBehaviour
{
    #region Fields

    [Header("simulation")] 
    [SerializeField] private int agentCount = 10000;

    [SerializeField] private float worldsize = 50f;

    [Header("Rendering")] 
    [SerializeField] private Mesh agentMesh;
    [SerializeField] private Material agentMaterial;    

    
    [Header("Compute")]
    [SerializeField] private ComputeShader boidShader;
    
    ComputeBuffer boidBuffer;
    private int moveKernal;
    private Bounds drawBounds;

    const string MoveKernalName = "BoidsCompute";
    static readonly int agentCountID = Shader.PropertyToID("AgentCount");
    static readonly int deltaTimeID = Shader.PropertyToID("deltaTime");
    static readonly int agentsID = Shader.PropertyToID("agents");
    
    #endregion

    private void Start()
    {
        boidBuffer = new ComputeBuffer(agentCount, System.Runtime.InteropServices.Marshal.SizeOf<BoidAgent>());
        
        
    }
}

[StructLayout(LayoutKind.Sequential)]
struct BoidAgent
{
    public Vector3 position;
    public Vector2 velocity;
    public Vector3 target;
    public float maxSpeed;
};

