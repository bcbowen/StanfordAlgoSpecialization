using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Greedy
{
    public static class MWIS
    {
        private static int[] _checkNodes = { 1, 2, 3, 4, 17, 117, 517, 997 };
        // 1, 2, 3, 4, 17, 117, 517, and 997
        public static string RunAlgorithm(string fileName) 
        {
            int[] nodes = LoadNodes(fileName);
            Dictionary<int, int> maxPath = new Dictionary<int, int>();
            int i = nodes.Length;
            StringBuilder result = new StringBuilder();
            while (i >= 1) 
            {
                if (nodes[i - 1] >= nodes[i - 2]) 
                {
                    i -= 1;
                }
                else 
                {
                    maxPath.Add(i + 1, nodes[i - i]);
                    i -= 2;
                }
            }
            if (i == 0) 
            {
                maxPath.Add(i + 1, nodes[i]);
            }

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
