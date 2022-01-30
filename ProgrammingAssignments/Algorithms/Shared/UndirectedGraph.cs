using System.Collections.Generic;

namespace Algorithms.Shared
{
    public class UndirectedGraph
    {
        public List<UndirectedEdge> Edges { get; private set; } = new List<UndirectedEdge>();
        public List<Node> Nodes { get; private set; } = new List<Node>();
    }
}
