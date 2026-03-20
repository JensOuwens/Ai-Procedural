using UnityEngine;

public abstract class LeafNode : Node
{
    public abstract AgentContext agentContext { get; set; }
}
