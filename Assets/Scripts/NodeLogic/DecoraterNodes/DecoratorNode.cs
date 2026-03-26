using UnityEngine;

public abstract class DecoratorNode : Node
{
    public abstract BlackBoard blackboard { get; set; }
    public abstract Node ChildNode { get; set; }
}
