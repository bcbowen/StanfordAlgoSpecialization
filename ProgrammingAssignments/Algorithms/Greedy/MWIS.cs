using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Algorithms.Greedy
{
    public static class MWIS
    {
        private static int[] _checkNodes = { 1, 2, 3, 4, 17, 117, 517, 997 };
        // 1, 2, 3, 4, 17, 117, 517, and 997
        public static string RunAlgorithm(string fileName) 
        {
            int[] nodes = LoadNodes(fileName);
            List<int> maxWeights = new List<int>();

            maxWeights.Add(0);
            maxWeights.Add(nodes[0]);
            for (int i = 1; i < nodes.Length; i++) 
            {
                maxWeights.Add(System.Math.Max(maxWeights[i], maxWeights[i - 1] + nodes[i]));
            }

            Dictionary<int, int> maxPath = new Dictionary<int, int>();

            for (int i = maxWeights.Count - 1; i >= 1;) 
            {
                if (maxWeights[i] > maxWeights[i - 1])
                {
                    maxPath.Add(i, nodes[i - 1]);
                    i -= 2;
                }
                else
                {
                    i -= 1;
                }
            }
            
            StringBuilder result = new StringBuilder();
            foreach (int checkNode in _checkNodes) 
            {
                if (maxPath.ContainsKey(checkNode))
                {
                    result.Append('1');
                }
                else 
                {
                    result.Append('0');
                }
            }

            return result.ToString();
        }

        public static int[] LoadNodes(string fileName)
        {
            List<int> nodes = new List<int>();
            using (StreamReader reader = new StreamReader(fileName)) 
            {                
                string line = reader.ReadLine();
                int nodeCount = int.Parse(line);
                for (int i = 0; i < nodeCount; i++) 
                {
                    line = reader.ReadLine();
                    nodes.Add(int.Parse(line));
                }

                reader.Close();
            }
            return nodes.ToArray();
        }
    }
}
