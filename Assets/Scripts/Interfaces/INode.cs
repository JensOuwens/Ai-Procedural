using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public interface INode
{
    public NodeStatus currentStatus { get; set; }

    public string name {get; set;}
    
    public List<INode> children {get; set;}

    protected int currentChild {get; set;}
}
