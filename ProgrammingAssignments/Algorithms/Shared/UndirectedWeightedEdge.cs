namespace Algorithms.Shared
{
    public class UndirectedWeightedEdge : UndirectedEdge
    {
        public UndirectedWeightedEdge(int node1, int node2, int weight) : base(node1, node2) 
        {
            Nodes[0] = new Node(System.Math.Min(node1, node2), 0);
            Nodes[1] = new Node(System.Math.Max(node1, node2), 0);
            Weight = weight;
        }

        public int Weight { get; private set; }

        public bool ContainsNode(int nodeId)
        {
            return Nodes[0].NodeId == nodeId || Nodes[1].NodeId == nodeId;
        }

        public bool ContainsNodes(int id1, int id2)
        {
            return Nodes[0].NodeId == System.Math.Min(id1, id2) && Nodes[0].NodeId == System.Math.Max(id1, id2);
        }
    }
}
