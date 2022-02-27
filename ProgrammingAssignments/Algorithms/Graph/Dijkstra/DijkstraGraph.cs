using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Algorithms.Shared;

namespace Algorithms.Graph.Dijkstra
{
    [Obsolete("This is being replaced, use Algorithm class", true)]
    public class DijkstraGraph 
    {
        public List<DijkstraNode> Nodes { get; private set; } = new List<DijkstraNode>();
        public int StartNodeId { get; set; }
        /*
        public List<DijkstraNode> ExploredNodes { get; private set; } = new List<DijkstraNode>();

        public MinHeap<DijkstraNode> UnexploredNodes { get; private set; } = new MinHeap<DijkstraNode>();
        public List<DijkstraNode> Frontier { get; private set; } = new List<DijkstraNode>();
        */
        public static List<NodeDistance> CalculateShortestPaths(DijkstraGraph graph)
        {
            // add start node to Explored Nodes with distance of 0
            //DijkstraNode node = UnexploredNodes.Dequeue();
            DijkstraHeap frontier = new DijkstraHeap();
            List<NodeDistance> processedNodes = new List<NodeDistance>();
            List<DijkstraNode> transferNodes;
            //AddToFrontier(node, 0);
            const int Unreachable = 1000000;

            transferNodes = graph.Nodes.Where(n => n.Value == graph.StartNodeId).ToList();
            foreach (DijkstraNode node in transferNodes)
            {
                frontier.Enqueue(node);
                //graph.Nodes.Remove(node);
            }
            //processedNodes.Add(new NodeDistance(graph.StartNodeId, 0));

            while (frontier.Count > 0) 
            {
                DijkstraNode node = frontier.Dequeue();
                
                NodeDistance processedNode = processedNodes.FirstOrDefault(n => n.NodeId == node.Value);
                if (processedNode != null)
                {
                    if (node.Value < processedNode.Distance) 
                    {
                        processedNode.Distance = processedNode.Distance;
                        processedNode.Path.Clear();
                        processedNode.Path.AddRange(node.Path); 
                    }
                }
                else
                {
                    NodeDistance nd = new NodeDistance(node.Value, node.Value);
                    nd.Path.AddRange(node.Path);    
                    processedNodes.Add(nd);
                }

                //if (node.ReferencedNode == null) continue; 
                transferNodes = graph.Nodes.Where(n => n.Value == node.ReferencedNode.NodeId).ToList();
                if (transferNodes.Count > 0)
                {
                    foreach (DijkstraNode transferNode in transferNodes)
                    {
                        if (transferNode.Value != Unreachable)
                        {
                            transferNode.Value = node.DijkstraValue;
                            transferNode.Path.AddRange(node.Path);
                            transferNode.Path.Add(node.Value);
                            frontier.Enqueue(transferNode);

                            //DijkstraNode returnNode = graph.Nodes.FirstOrDefault(n => n.Value == transferNode.ReferencedNode.NodeId && n.ReferencedNode.NodeId == transferNode.Value);
                            //if (returnNode != null) returnNode.Distance = Unreachable;
                        }
                        //graph.Nodes.Remove(transferNode);
                    }
                }
                else
                {
                    //frontier.Enqueue(new DijkstraNode(node.ReferencedNode.NodeId, node.Distance + node.ReferencedNode.Distance));
                    processedNode = processedNodes.FirstOrDefault(n => n.NodeId == node.ReferencedNode.NodeId);
                    if (processedNode != null)
                    {
                        if (node.Value < processedNode.Distance) processedNode.Distance = processedNode.Distance;
                    }
                    else
                    {
                        NodeDistance nd = new NodeDistance(node.ReferencedNode.NodeId, node.DijkstraValue);
                        nd.Path.AddRange(node.Path);
                        nd.Path.Add(node.Value);
                        processedNodes.Add(nd);
                    }
                }
                
            }
            return processedNodes;
        }
        
        /*
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
        */

        /*
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
        */

        public static DijkstraGraph LoadGraph(string path)
        {
            DijkstraGraph graph = new DijkstraGraph();
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                DijkstraNode node;
                int nodeId; 
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split('\t');
                    nodeId = int.Parse(fields[0]);
                    if (graph.StartNodeId == 0) 
                    {
                        graph.StartNodeId = nodeId;
                    }
                    for (int i = 1; i < fields.Length; i++)
                    {
                        
                        string[] nodeInfo = fields[i].Split(',');
                        if (nodeInfo.Length == 2)
                        {
                            node = new DijkstraNode(nodeId, int.MaxValue, int.Parse(nodeInfo[0]), int.Parse(nodeInfo[1]));
                            graph.Nodes.Add(node);
                        }
                    }                    
                }
                reader.Close();
            }

            return graph;
        }
    }
}
