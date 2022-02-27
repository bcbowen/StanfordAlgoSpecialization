using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Graph.Dijkstra
{
    public static class Algorithm
    {
        public static void CalculateShortestPaths(List<DijkstraNode> nodes, int startNodeId) 
        {
            DijkstraHeap heap = new DijkstraHeap();
            DijkstraNode node = nodes.First(n => n.NodeId == startNodeId);
            AddNodesToHeap(startNodeId, nodes, heap, 0);
            while (heap.Count > 0) 
            {
                node = heap.Dequeue();
                if (nodes.Any(n => n.NodeId == node.ReferencedNode.NodeId && n.ReferencedNode != null))
                {
                    AddNodesToHeap(node.ReferencedNode.NodeId, nodes, heap, node.DijkstraValue);
                    node.Processed = true;
                }
                else 
                {
                    if (!nodes.Any(n => n.NodeId == node.ReferencedNode.NodeId))
                    {
                        nodes.Add(new DijkstraNode(node.ReferencedNode.NodeId, int.MaxValue)); 
                    }

                    DijkstraNode leaf = nodes.First(n => n.NodeId == node.ReferencedNode.NodeId);
                    if (leaf.Value > node.DijkstraValue) leaf.Value = node.DijkstraValue;
                }
            }

        }

        private static void AddNodesToHeap(int nodeId, List<DijkstraNode> nodes, DijkstraHeap heap, int distance) 
        {
            DijkstraNode[] sourceNodes = nodes.Where(n => n.NodeId == nodeId && !n.Processed).ToArray();
            foreach (DijkstraNode sourceNode in sourceNodes)
            {
                if (sourceNode.Value > distance) sourceNode.Value = distance;
                heap.Enqueue(sourceNode);
            }            
        }

        public static List<DijkstraNode> CalculateShortestPaths(string path, int startNodeId)
        {
            List<DijkstraNode> nodes = LoadGraph(path);
            CalculateShortestPaths(nodes, startNodeId);
            return nodes;
        }

        public static List<DijkstraNode> LoadGraph(string path)
        {
            List<DijkstraNode> nodes = new List<DijkstraNode>();
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                DijkstraNode node;
                int nodeId;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split('\t');
                    nodeId = int.Parse(fields[0]);

                    for (int i = 1; i < fields.Length; i++)
                    {

                        string[] nodeInfo = fields[i].Split(',');
                        if (nodeInfo.Length == 2)
                        {
                            node = new DijkstraNode(nodeId, int.MaxValue, int.Parse(nodeInfo[0]), int.Parse(nodeInfo[1]));
                            nodes.Add(node);
                        }
                    }
                }
                reader.Close();
            }

            return nodes;
        }
    }
}
