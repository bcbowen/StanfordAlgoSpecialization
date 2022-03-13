using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Algorithms.Shared;

namespace Algorithms.Graph.AllPairsShortestPath
{
    public static class FloydWarshall
    {

        public static int? CalculateShortestPath(DirectedGraph graph) 
        {
            int n = graph.Nodes.Count;
            const int infinity = 10000; // don't use int.MaxValue because yada yada yada
            int[,,] subproblems = new int[n,n,n];
            for (int v = 0; v < n; v++) 
            {
                for (int w = 0; w < n; w++) 
                {
                    if (v == w)
                    {
                        subproblems[0, v, w] = 0;
                    }
                    else if (graph.Nodes.Any(n => n.NodeId == v && n.ReferencedNodeId == w)) 
                    {
                        subproblems[0, v, w] = graph.Nodes.First(n => n.NodeId == v && n.ReferencedNodeId == w).Value;
                    }
                    else 
                    {
                        subproblems[0, v, w] = infinity;
                    }
                }
            }

            // solve all subproblems
            for (int k = 1; k < n; k++) 
            {
                for (int v = 1; v < n; v++) 
                {
                    for (int w = 1; w < n; w++) 
                    {
                        int value = System.Math.Min(subproblems[k - 1, v, w], subproblems[k - 1, v, k] + subproblems[k - 1, k, w]);
                        subproblems[k, v, w] = value;
                        Console.WriteLine($"Setting [{k},{v},{w}] to {value}");
                    }
                }
            }

            // check for negative cycle
            for (int v = 1; v < n; v++) 
            {
                if (subproblems[n - 1, v, v] < 0)
                {
                    return null;
                }
            }

            return subproblems[n - 1, n - 1, n - 1];
        }


        /// <summary>
        /// Sample file (first 3 lines):
        /// 8 9
        /// 1 7 -41
        /// 1 8 -4
        /// ...
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static DirectedGraph LoadGraph(string fileName) 
        {

            DirectedGraph graph = new DirectedGraph();
            using (StreamReader reader = new StreamReader(fileName))
            {
                // first line has counts
                string line = reader.ReadLine();
                while ((line = reader.ReadLine()) != null) 
                {
                    string[] fields = line.Split(' ');
                    if (fields.Length > 2) 
                    {
                        graph.Nodes.Add(new DirectedNode(int.Parse(fields[0]), int.Parse(fields[1]), int.Parse(fields[2])));
                    }
                }
                reader.Close();
            }
            return graph;
        }
    }
}
