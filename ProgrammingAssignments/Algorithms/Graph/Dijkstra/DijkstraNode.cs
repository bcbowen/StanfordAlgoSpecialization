﻿using System.Collections.Generic;

using Algorithms.Shared;

namespace Algorithms.Graph.Dijkstra
{
    public class DijkstraNode : NodeBase
    {
        public DijkstraNode(int nodeId, int distance) : base(nodeId, distance)
        {
            ReferencedNode = null;
        }

        public DijkstraNode(int nodeId, int distance, int referencedNodeId, int referencedNodeDistance) : base(nodeId, distance) 
        {
            ReferencedNode = new NodeDistance(referencedNodeId, referencedNodeDistance);
        }

        public NodeDistance ReferencedNode { get; set; }

        public int DijkstraValue 
        {
            get 
            {
                return ReferencedNode != null ? ReferencedNode.Distance + Value : Value;
            }
        }

        public bool Processed { get; set; }

        public List<int> Path { get; private set; } = new List<int>();

        public int Index { get; set; }
    }
}
