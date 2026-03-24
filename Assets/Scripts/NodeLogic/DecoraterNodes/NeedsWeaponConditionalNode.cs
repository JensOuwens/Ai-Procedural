using UnityEngine;

public class NeedsWeaponConditionalNode : DecoratorNode
{
    public override NodeStatus currentStatus { get; set; }
    
    public override BlackBoard blackboard { get; set; }
    public override Node ChildNode { get; set; }

    public NeedsWeaponConditionalNode(BlackBoard blackboard, Node childNode)
    {
        this.blackboard = blackboard;
        this.ChildNode = childNode;
    }
    
    public override void Execute()
    {
        if (blackboard.needsWeapon)
        {
            ChildNode.Execute();
            UpdateStatus(ChildNode.GetCurrentStatus());
        }
        else
        {
            UpdateStatus(NodeStatus.Failed);
        }
    }

    public override void Reset()
    {
        UpdateStatus(NodeStatus.Running);
        ChildNode.Reset();
    } 
}
