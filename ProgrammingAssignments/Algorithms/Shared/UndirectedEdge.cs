namespace Algorithms.Shared
{
    public class UndirectedEdge
    {
        /// <summary>
        /// Since the edges are undirected the nodes will be in asc order for convenience
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        public UndirectedEdge(int id1, int id2) 
        {
            Node node = new Node(System.Math.Min(id1, id2), 0);
            _nodes[0] = node;
            node = new Node(System.Math.Max(id1, id2), 0);
            _nodes[1] = node;
        }


        private Node[] _nodes = new Node[2];

        public Node[] Nodes { get { return _nodes; } }
    }
}
