using System.Collections.Generic;
using UnityEngine;

public class Node : INode
{
    public NodeStatus currentStatus {get; set;}

    public readonly string name;
    
    public readonly List<Node> children = new();
    
    protected int currentChild;

    public Node(string name = "Node")
    {
        this.name = name;
    }
    
    public void AddChild(Node child) => children.Add(child);
    
    public virtual NodeStatus Process() => children[currentChild].Process();

    public void Reset()
    {
        currentChild = 0;
        foreach (var child in children)
        {
            child.Reset();
        }
    }
}
