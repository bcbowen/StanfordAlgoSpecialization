using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Shared
{
    public class UndirectedEdge
    {
        public UndirectedEdge(int value1, int value2) 
        {
            Node node = new Node(value1);
            _nodes[0] = node;
            node = new Node(value2);
            _nodes[1] = node;
        }


        private Node[] _nodes = new Node[2];

        public Node[] Nodes { get { return _nodes; } }
    }
}
