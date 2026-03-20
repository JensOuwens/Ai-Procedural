using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class DecoratorNode : Node
{
    public abstract Blackboard blackboard { get; set; }
    public abstract Node ChildNode { get; set; }
}
