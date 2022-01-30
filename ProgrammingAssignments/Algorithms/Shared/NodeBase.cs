using System;

namespace Algorithms.Shared
{
    public abstract class NodeBase : IEquatable<NodeBase>
    {
        public int Value { get; set; }

        public int NodeId { get; set; }

        public NodeBase(int nodeId, int value) 
        {
            NodeId = nodeId;
            Value = value;
        }

        public bool Equals(NodeBase other)
        {
            return Equals(other);
        }

        public override bool Equals(object obj)
        {
            var n = obj as NodeBase;
            if (n == null) return false;
            
            return n.Value == Value && n.NodeId == NodeId;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        
    }
}
