using System.Collections.Generic;

using Algorithms.Shared;

namespace Algorithms.Graph.Dijkstra
{
    public class DijkstraNode : NodeBase
    {
        /*
        public DijkstraNode(int nodeId, int distance) : base(nodeId)
        {
            Distance = distance;
            ReferencedNode = null;
        }
        */

        public DijkstraNode(int nodeId, int referencedNodeId, int distance) : base(nodeId) 
        {
            ReferencedNode = new NodeDistance(referencedNodeId, distance);
        }

        public NodeDistance ReferencedNode { get; set; }
        public int Distance { get; set; }

        public int DijkstraValue 
        {
            get 
            {
                return ReferencedNode != null ? ReferencedNode.Distance + Distance : Distance;
            }
        }

        public List<int> Path { get; private set; } = new List<int>();
    }
}
