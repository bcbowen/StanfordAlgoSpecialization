using System.Collections.Generic;

using Algorithms.Shared;

namespace Algorithms.Graph.Dijkstra
{
    public class DijkstraNode : NodeBase
    {
        public DijkstraNode(int value) : base(value) { }

        public List<ReferencedNode> ReferencedNodes { get; private set; } = new List<ReferencedNode>();
        public int MinDistance { get; set; }
    }
}
