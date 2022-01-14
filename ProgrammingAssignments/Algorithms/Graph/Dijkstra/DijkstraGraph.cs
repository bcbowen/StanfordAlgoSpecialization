using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Algorithms.Shared;

namespace Algorithms.Graph.Dijkstra
{
    public class DijkstraGraph 
    {
        public List<DijkstraNode> ExploredNodes { get; private set; } = new List<DijkstraNode>();

        public MinHeap<DijkstraNode> UnexploredNodes { get; private set; } = new MinHeap<DijkstraNode>();
        public List<DijkstraNode> Frontier { get; private set; } = new List<DijkstraNode>();

        public void CalculateShortestPaths()
        {
            // add start node to Explored Nodes with distance of 0
            DijkstraNode node = UnexploredNodes.Dequeue();
            AddToFrontier(node, 0);

            int minDistance;
            ReferencedNode minNode = node.ReferencedNodes[0];
            int distance;
            while (Frontier.Count > 0)
            {
                minDistance = 1000000;
                for (int i = 0; i < Frontier.Count; i++)
                {
                    DijkstraNode fromNode = Frontier[i];

                    foreach (ReferencedNode toNode in fromNode.ReferencedNodes.Where(n => !n.Done))
                    {

                        distance = fromNode.MinDistance + toNode.Distance;
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            minNode = toNode;
                        }
                    }
                }

                node = UnexploredNodes.Remove(minNode.Value);
                AddToFrontier(node, minDistance);
                PruneFrontier();

            }

        }

        internal void AddToFrontier(DijkstraNode node, int minDistance)
        {
            Frontier.Add(node);
            // The new node referencing nodes already in the frontier
            // updateNodes = node.ReferencedNodes.Where(r => Frontier.Any(n => n.NodeId == node.NodeId)).ToList();

            List<ReferencedNode> updateNodes =
                (from r in node.ReferencedNodes
                 join f in Frontier on r.Value equals f.Value
                 select r).ToList();
            //.Where(r => Frontier.Any(n => n.NodeId == node.NodeId)).ToList();

            foreach (ReferencedNode updateNode in updateNodes)
            {
                updateNode.Done = true;
            }

            // nodes in frontier referencing the new node
            updateNodes = Frontier.SelectMany(f => f.ReferencedNodes).Where(n => n.Value == node.Value).ToList();
            foreach (ReferencedNode updateNode in updateNodes)
            {
                updateNode.Done = true;
            }

            // references to nodes already explored (so the arrow is going to wrong way) 
            updateNodes = node.ReferencedNodes.Where(n => ExploredNodes.Any(e => e.Value == n.Value)).ToList();
            foreach (ReferencedNode updateNode in updateNodes)
            {
                updateNode.Done = true;
            }

            node.MinDistance = minDistance;

        }

        /// <summary>
        /// 
        /// </summary>
        internal void PruneFrontier()
        {
            List<int> nodeIds = ExploredNodes.Select(n => n.Value).ToList().Concat(Frontier.Select(n => n.Value)).ToList();
            for (int i = Frontier.Count - 1; i >= 0; i--)
            {
                DijkstraNode node = Frontier[i];
                // referenced nodes where the id is not in nodeids
                if (node.ReferencedNodes.All(r => nodeIds.Any(n => n == r.Value)))
                {
                    // all referenced nodes are in explored nodes or the frontier
                    ExploredNodes.Add(node);
                    Frontier.Remove(node);
                }
            }
        }

        public static DijkstraGraph LoadGraph(string path)
        {
            DijkstraGraph graph = new DijkstraGraph();
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                DijkstraNode node;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split('\t');
                    node = new DijkstraNode(int.Parse(fields[0]));

                    for (int i = 1; i < fields.Length; i++)
                    {
                        string[] nodeInfo = fields[i].Split(',');
                        if (nodeInfo.Length == 2)
                        {
                            node.ReferencedNodes.Add(new ReferencedNode(int.Parse(nodeInfo[0]), int.Parse(nodeInfo[1])));
                        }

                    }

                    graph.UnexploredNodes.Enqueue(node);
                }
                reader.Close();
            }

            return graph;
        }
    }
}
