using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Graph.DataStructures; 

namespace Graph
{
    public class UndirectedGraph
    {
        public List<Node> ExploredNodes { get; private set; } = new List<Node>();

        public MinHeap UnexploredNodes { get; private set; } = new MinHeap();
        public List<Node> Frontier { get; private set; } = new List<Node>();

        public void CalculateShortestPaths() 
        {
            // add start node to Explored Nodes with distance of 0
            Node node = UnexploredNodes.Dequeue();
            node.MinDistance = 0;
            Frontier.Add(node);
            int minDistance;
            ReferencedNode minNode = node.ReferencedNodes[0];
            int distance; 
            while (Frontier.Count > 0) 
            {
                minDistance = 1000000;
                for (int i = 0; i < Frontier.Count; i++) 
                {
                    Node fromNode = Frontier[i];
                    
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
                List<ReferencedNode> updateNodes = Frontier.SelectMany(f => f.ReferencedNodes).Where(n => n.NodeId == minNode.NodeId).ToList();
                foreach (ReferencedNode updateNode in updateNodes) 
                {
                    updateNode.Done = true;
                }
                node = UnexploredNodes.Remove(minNode.NodeId);
                node.MinDistance = minDistance;
                Frontier.Add(node);
                PruneFrontier();

            }
        
        }

        /// <summary>
        /// 
        /// </summary>
        internal void PruneFrontier() 
        {
            List<int> nodeIds = ExploredNodes.Select(n => n.NodeId).ToList().Concat(Frontier.Select(n => n.NodeId)).ToList();
            for (int i = Frontier.Count - 1; i >= 0; i--) 
            {
                Node node = Frontier[i];
                // referenced nodes where the id is not in nodeids
                if (node.ReferencedNodes.All(r => nodeIds.Any(n => n == r.NodeId)))
                {
                    // all referenced nodes are in explored nodes or the frontier
                    ExploredNodes.Add(node);
                    Frontier.Remove(node);
                }
            }
        }

        public static UndirectedGraph LoadGraph(string path)
        {
            UndirectedGraph graph = new UndirectedGraph();
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                Node node;
                while ((line = reader.ReadLine()) != null) 
                {
                    string[] fields = line.Split('\t');
                    node = new Node(int.Parse(fields[0]));

                    for (int i = 1; i < fields.Length; i++) 
                    {
                        string[] nodeInfo = fields[i].Split(',');
                        node.ReferencedNodes.Add(new ReferencedNode(int.Parse(nodeInfo[0]), int.Parse(nodeInfo[1])));
                    }

                    graph.UnexploredNodes.Enqueue(node);
                }
                reader.Close();
            }

            return graph; 
        }
    }
}
