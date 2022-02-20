using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Algorithms.Shared;

namespace Algorithms.Greedy
{
    public static class Knapsack
    {
        public static int RunAlgorithm(List<Node> nodes, int capacity) 
        {
            int[][] table = new int[nodes.Count + 1][];
            for (int i = 0; i < table.Length; i++)
            {
                table[i] = new int[capacity + 1];
            }

            for (int i = 1; i <= nodes.Count; i++)
            {
                for (int c = 0; c <= capacity; c++) 
                {
                    if ( i == 0 || c == 0) table[i][c] = 0;

                    if (nodes[i - 1].Value > c)
                    {
                        table[i][c] = table[i - 1][c];
                    }
                    else
                    {
                        table[i][c] = System.Math.Max(table[i - 1][c], table[i - 1][c - nodes[i - 1].Value] + nodes[i - 1].NodeId);
                    }
                }
            }

            return table[nodes.Count][capacity];
        }

        public static int RunAlgorithm(string fileName) 
        {
            (List<Node> nodes, int capacity) = LoadKnapsackData(fileName);
            return RunAlgorithm(nodes, capacity);
        }

        public static (List<Node> nodes, int capacity) LoadKnapsackData(string fileName) 
        {
            int capacity = 0;
            List<Node> nodes = new List<Node>();

            using (StreamReader reader = new StreamReader(fileName)) 
            {
                string[] fields;
                // first line has capacity and number of items (count)
                string line = reader.ReadLine();
                fields = line.Split(' ');
                capacity = int.Parse(fields[0]);
                while ((line = reader.ReadLine()) != null)
                {
                    fields = line.Split(' ');
                    nodes.Add(new Node(int.Parse(fields[0]), int.Parse(fields[1])));
                }
                reader.Close();
            }
            return (nodes, capacity);
        }
    }
}
