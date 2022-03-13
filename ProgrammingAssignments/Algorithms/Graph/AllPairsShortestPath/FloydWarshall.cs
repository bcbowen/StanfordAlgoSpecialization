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
            int n = graph.NodeCount;
            const int infinity = 10000; // don't use int.MaxValue because yada yada yada
                
            // initialize jagged array int[2][n][n]
            int[][][] lookup = new int[2][][];
            for (int i = 0; i < 2; i++) 
            {
                lookup[i] = new int[n][];
                for (int j = 0; j < n; j++) 
                {
                    lookup[i][j] = new int[n];
                }
            }

            // Initialize lookup table with appropriate values when i != j.
            for (int i = 0; i < n; i++) 
            {
                for (int j = 0; j < n; j++)
                {
                    if (i != j) lookup[0][i][j] = infinity;
                }
            }

            // Initialize lookup table with appropriate values when there is a directed edge from i to j.
            for (int i = 0; i < n; i++)
            {
                foreach (DirectedNode node in graph.Nodes.Where(n => n.NodeId - 1 == i)) 
                {
                    lookup[0][i][node.ReferencedNodeId - 1] = node.Value;
                }
            }

            // solve all subproblems
            int x = 0;
            int y = 1;
            for (int k = 1; k < n; k++) 
            {
                for (int v = 0; v < n; v++) 
                {
                    for (int w = 0; w < n; w++) 
                    {
                        
                        int value = System.Math.Min(lookup[x][v][w], lookup[x][v][k] + lookup[x][k][w]);
                        lookup[y][v][w] = value;
                        Console.WriteLine($"Setting [{k},{v},{w}] to {value}");
                    }
                }
                x = (x + 1) % 2;
                y = (y + 1) % 2;
            }

            x = (n + 1) % 2;
            // check for negative cycle
            for (int v = 0; v < n; v++) 
            {
                if (lookup[x][v][v] < 0)
                {
                    return null;
                }
            }

            int[][] paths = lookup[(n + 1) % 2];
            int minValue = paths[0][0];
            for (int i = 0; i < paths.Length; i++)
                for (int j = 0; j < paths.Length; j++)
                    if (paths[i][j] < minValue)
                        minValue = paths[i][j];

            return minValue;

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
                string[] fields; 
                string line = reader.ReadLine();
                fields = line.Split(' ');
                graph.NodeCount = int.Parse(fields[0]);
                graph.EdgeCount = int.Parse(fields[1]);
                while ((line = reader.ReadLine()) != null) 
                {
                    fields = line.Split(' ');
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
