using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Shared
{
    public class UndirectedGraph
    {
        public List<UndirectedEdge> Edges { get; private set; } = new List<UndirectedEdge>();
        public List<Node> Nodes { get; private set; } = new List<Node>();
    }
}
