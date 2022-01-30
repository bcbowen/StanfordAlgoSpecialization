using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Graph.PrimMst
{
    /*
    public class PrimEdge
    {
        public PrimEdge(int node1, int node2, int weight)
        {
            _nodes[0] = new Node(System.Math.Min(node1, node2));
            _nodes[1] = new Node(System.Math.Max(node1, node2));
            Weight = weight;
        }

        private Node[] _nodes = new Node[2];

        public Node[] Nodes { get { return _nodes; } }

        public int Weight { get; set; }

        public bool ContainsNode(int nodeId) 
        {
            return Nodes[0].NodeId == nodeId || Nodes[1].NodeId == nodeId;
        }
        
        public bool ContainsNodes(int id1, int id2) 
        {
            return Nodes[0].NodeId == System.Math.Min(id1, id2) && Nodes[0].NodeId == System.Math.Max(id1, id2);
        }
    }
    */
    
}
