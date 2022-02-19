using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Tree.OptimizedBinaryTree
{
    public static class Algorithm
    {
        public static decimal CalculateOptimalSearchTree(List<OptimizedTreeNode>nodes) 
        {
            // ensure nodes are sorted by node id
            nodes.Sort(OptimizedTreeNode.CompareNodeIds);
            return GetOptimalCost(nodes, 0, nodes.Count - 1);
        }

        private static decimal GetOptimalCost(List<OptimizedTreeNode> nodes, int i, int j) 
        {
            // no elements in subarray
            if (j < i) return 0;

            // single element in subarray
            if (j == i) return nodes[i].Weight;

            // Sum of weights from i to j
            decimal weightSum = nodes.Where((n, k) => k >= i && k <= j).Sum(n => n.Weight);

            // initialize min value
            decimal min = decimal.MaxValue;

            // consider each element as root and recursively find cost of BST to find min
            for (int r = i; r <= j; ++r) 
            {
                decimal cost = GetOptimalCost(nodes, i, r - 1) + GetOptimalCost(nodes, r + 1, j);

                if (cost < min) min = cost;
            }

            return min + weightSum;
        }

        public static List<OptimizedTreeNode> LoadNodes(string fileName)
        {
            List<OptimizedTreeNode> nodes = new List<OptimizedTreeNode>();
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                string[] fields;
                while ((line = reader.ReadLine()) != null) 
                {
                    fields = line.Split(' ');
                    if (fields.Length > 1) 
                    {
                        nodes.Add(new OptimizedTreeNode(int.Parse(fields[0]), decimal.Parse(fields[1])));
                    }
                }
                
            }

            return nodes;
        }
    }
}
