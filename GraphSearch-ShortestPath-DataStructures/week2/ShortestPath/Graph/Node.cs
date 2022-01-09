using System.Collections.Generic;


namespace Graph
{
    public class Node
    {
        public Node(int nodeId)
        {
            NodeId = nodeId;

        }

        public int NodeId { get; set; }

        public List<ReferencedNode> ReferencedNodes { get; private set; } = new List<ReferencedNode>();
        public int MinDistance { get; set; } 
    }
}
