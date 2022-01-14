using System;

namespace Algorithms.Shared
{
    public abstract class NodeBase : IEquatable<NodeBase>
    {
        public int Value { get; set; }

        public NodeBase(int value) 
        {
            Value = value;
        }

        public bool Equals(Shared.NodeBase? other)
        {
            return Equals(other);
        }

        public override bool Equals(object obj)
        {
            var n = obj as NodeBase;
            if (n == null) return false;
            
            return Value.Equals(n.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        
    }
}
