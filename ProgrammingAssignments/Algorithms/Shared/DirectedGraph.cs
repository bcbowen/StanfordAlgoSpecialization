using System.Collections.Generic;

namespace Algorithms.Shared
{
    public class DirectedGraph
    {

        public int NodeCount { get; set; }
        public int EdgeCount { get; set; }

        public List<DirectedNode> Nodes { get; private set; } = new List<DirectedNode>();


    }
}
