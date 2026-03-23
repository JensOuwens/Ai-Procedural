using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : CompositeNode
{
    public override NodeStatus currentStatus { get; set; }
    public override List<Node> children { get; set; }
    public override int currentChild { get; set; }
    
    public SelectorNode(List<Node> children)
    {
        currentChild = 0;
        this.children =  children;
    }
    public override void Execute()
    {
        while (true)
        {
            if (currentChild >= children.Count)
            {
                UpdateStatus(NodeStatus.Failed);
                Reset();
                return;
            }

            Node current = children[currentChild];
            current.Execute();

            var status = current.GetCurrentStatus();

            if (status == NodeStatus.Completed)
            {
                UpdateStatus(NodeStatus.Completed);
                return;
            }

            if (status == NodeStatus.Failed)
            {
                current.Reset();
                currentChild++;
                continue;
            }

            if (status == NodeStatus.Running)
            {
                UpdateStatus(NodeStatus.Running);
                return;
            }
        }
    }

    public override void Reset()
    {
        currentChild = 0;
        foreach (Node child in children)
        {
            child.Reset();
        }
    }
}
