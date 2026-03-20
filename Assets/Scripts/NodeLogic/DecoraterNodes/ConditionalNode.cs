using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ConditionalNode : DecoratorNode
{
    public override NodeStatus currentStatus { get; set; }
    
    public override Blackboard blackboard { get; set; }
    public override Node ChildNode { get; set; }

    public ConditionalNode(Blackboard blackboard, Node childNode)
    {
        this.blackboard = blackboard;
        this.ChildNode = childNode;
    }
    
    public override void Execute()
    {
        
    }

    public override void Reset()
    {
        
    }
}
