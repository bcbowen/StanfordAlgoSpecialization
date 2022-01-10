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
            AddToFrontier(node, 0); 
            
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
                
                node = UnexploredNodes.Remove(minNode.NodeId);
                AddToFrontier(node, minDistance);
                PruneFrontier();

            }
        
        }

        internal void AddToFrontier(Node node, int minDistance) 
        {
            Frontier.Add(node);
            // The new node referencing nodes already in the frontier
            // updateNodes = node.ReferencedNodes.Where(r => Frontier.Any(n => n.NodeId == node.NodeId)).ToList();

            List<ReferencedNode> updateNodes =
                (from r in node.ReferencedNodes
                join f in Frontier on r.NodeId equals f.NodeId
                select r).ToList();
                //.Where(r => Frontier.Any(n => n.NodeId == node.NodeId)).ToList();

            foreach (ReferencedNode updateNode in updateNodes)
            {
                updateNode.Done = true;
            }

            // nodes in frontier referencing the new node
            updateNodes = Frontier.SelectMany(f => f.ReferencedNodes).Where(n => n.NodeId == node.NodeId).ToList();
            foreach (ReferencedNode updateNode in updateNodes)
            {
                updateNode.Done = true;
            }

            // references to nodes already explored (so the arrow is going to wrong way) 
            updateNodes = node.ReferencedNodes.Where(n => ExploredNodes.Any(e => e.NodeId == n.NodeId)).ToList();
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
