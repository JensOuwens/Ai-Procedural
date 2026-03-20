using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : CompositeNode
{
    public override AgentContext agentContext { get; set; }
    public override NodeStatus currentStatus { get; set; }
    
    public override List<Node> children { get; set; }
    public override Node currentChild { get; set; }
    
    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}
