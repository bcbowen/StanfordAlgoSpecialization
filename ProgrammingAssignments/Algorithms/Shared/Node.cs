using System;

namespace Algorithms.Shared
{
    public class Node : NodeBase, IEquatable<Node>
    {
        public Node(int nodeId, int value) : base(nodeId, value) { }

        public bool Equals(Node other)
        {
            Node node = other as Node;
            if (node == null) return false;

            return NodeId == node.NodeId;
        }

        public override int GetHashCode()
        {
            return NodeId.GetHashCode();
        }
    }

   
}
