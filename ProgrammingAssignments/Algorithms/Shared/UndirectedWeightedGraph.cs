using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace Algorithms.Shared
{
    public class UndirectedWeightedGraph
    {
        public List<UndirectedWeightedEdge> Edges { get; private set; } = new List<UndirectedWeightedEdge>();
        public List<Node> Nodes { get; private set; } = new List<Node>();

        public static UndirectedWeightedGraph LoadGraph(string fileName) 
        {
            UndirectedWeightedGraph graph = new UndirectedWeightedGraph();
            int edgeCount, nodeCount;

            using (StreamReader reader = new StreamReader(fileName)) 
            {
                
                string line;
                line = reader.ReadLine();
                string[] fields = line.Split(' ');
                nodeCount = int.Parse(fields[0]);
                edgeCount = int.Parse(fields[1]);
                while ((line = reader.ReadLine()) != null) 
                { 
                    fields = line.Split(' ');
                    
                    if (fields.Length > 2)
                    {
                        graph.Edges.Add(new UndirectedWeightedEdge(int.Parse(fields[0]), int.Parse(fields[1]), int.Parse(fields[2])));
                    }
                }
                reader.Close();
            }

            foreach (UndirectedWeightedEdge e in graph.Edges) 
            {
                if (!graph.Nodes.Contains(e.Nodes[0])) graph.Nodes.Add(e.Nodes[0]);
                if (!graph.Nodes.Contains(e.Nodes[1])) graph.Nodes.Add(e.Nodes[1]);

                if (graph.Nodes.Count == nodeCount) break;
            }

            Debug.Assert(graph.Edges.Count == edgeCount);
            Debug.Assert(graph.Nodes.Count == nodeCount);

            return graph;
        }
    }
}
