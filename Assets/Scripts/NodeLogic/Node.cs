using System.Collections.Generic;
using UnityEngine;

public abstract class Node
{
    public abstract NodeStatus currentStatus {get; set;}

    public abstract void Execute();
    
    public abstract void Reset();

    public virtual void UpdateStatus(NodeStatus NewStatus)
    {
        currentStatus =  NewStatus;
    }

    public virtual NodeStatus GetCurrentStatus()
    {
        return currentStatus;
    }
}
