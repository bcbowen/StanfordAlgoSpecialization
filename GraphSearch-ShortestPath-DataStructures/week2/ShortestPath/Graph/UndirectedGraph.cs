using System;
using System.Collections.Generic;
using System.IO;

using Graph.DataStructures; 

namespace Graph
{
    public class UndirectedGraph
    {
        public MinHeap ExploredNodes { get; private set; } = new MinHeap();

        public MinHeap UnexploredNodes { get; private set; } = new MinHeap();


        public void CalculateShortestPaths() 
        {
            // add start node to Explored Nodes with distance of 0
            Node node = UnexploredNodes.Dequeue();
            node.MinDistance = 0;
            ExploredNodes.Enqueue(node);
            int minDistance;
            ReferencedNode minNode;
            int distance; 
            while (UnexploredNodes.Count > 0) 
            {
                foreach (ReferencedNode toNode in node.ReferencedNodes) 
                {
                    minDistance = 1000000;
                    distance = node.MinDistance + toNode.Distance;
                    if (distance < minDistance) 
                    {
                        minDistance = distance;
                        minNode = toNode;
                    }
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
