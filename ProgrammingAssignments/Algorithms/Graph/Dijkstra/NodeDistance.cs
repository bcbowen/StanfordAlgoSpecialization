namespace Algorithms.Graph.Dijkstra
{
    public class NodeDistance
    {
        public NodeDistance(int nodeId, int distance) 
        {
            NodeId = nodeId;
            Distance = distance;
        }

        public int NodeId { get; set; }
        public int Distance { get; set; }
    }
}
