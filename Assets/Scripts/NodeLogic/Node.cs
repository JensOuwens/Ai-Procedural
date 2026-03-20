using System.Collections.Generic;
using UnityEngine;

public abstract class Node
{
    public abstract AgentContext agentContext { get; set; }
    public abstract NodeStatus currentStatus {get; set;}

    public abstract void Execute();
    
    public abstract void Reset();
}
