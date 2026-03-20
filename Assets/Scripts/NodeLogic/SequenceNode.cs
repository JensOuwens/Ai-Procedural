using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : CompositeNode
{
    public override NodeStatus currentStatus { get; set; }
    
    public override List<Node> children { get; set; }
    public override int currentChild { get; set; }


    public SequenceNode(List<Node> children)
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
                UpdateStatus(NodeStatus.Completed);
                Reset();
                return;
            }


            children[currentChild].Execute();

            if (children[currentChild].GetCurrentStatus() == NodeStatus.Completed)
            {
                currentChild++;
                continue;
            }
            else if (children[currentChild].GetCurrentStatus() == NodeStatus.Failed)
            {
                Reset();
                UpdateStatus(NodeStatus.Failed);
                return;
            }
            else if (children[currentChild].GetCurrentStatus() == NodeStatus.Running)
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
