namespace Algorithms.Graph.Dijkstra
{
    public class ReferencedNode : DijkstraNode
    {
        public int Distance { get; set; }

        public ReferencedNode(int id, int distance) : base(id)
        {
            Distance = distance;
        }

        public bool Done { get; set; }
    }
}
