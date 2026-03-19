using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public interface INode
{
    public NodeStatus currentStatus { get; set; }

    public void AddChild(Node child);

    public NodeStatus Process();

    public void Reset();
}
