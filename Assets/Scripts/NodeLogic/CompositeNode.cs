using System.Collections.Generic;
using UnityEngine;

public abstract class CompositeNode : Node
{
    public abstract List<Node> children { get; set; }
    public abstract Node currentChild {get; set;}
    
    public virtual void AddChild(Node newChild) => children.Add(newChild);
    public virtual void RemoveChild(Node newChild) => children.Remove(newChild);
}
