using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Greedy.HuffmanCodes
{
    public static class Algorithm
    {
        public static HuffmanTree BuildTree(MinHeap forest) 
        {
            while (forest.Count > 1) 
            {
                HuffmanTree t1 = forest.Dequeue();
                HuffmanTree t2 = forest.Dequeue();
                HuffmanTree tree = HuffmanTree.Merge(t1, t2);
                forest.Enqueue(tree);
            }

            return forest.Dequeue();
        }

        public static MinHeap LoadCodeWeights(string fileName)
        {

            MinHeap forest = new MinHeap();
            int nodeCount;
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                // first line has nodecount, all other lines have weights
                nodeCount = int.Parse(reader.ReadLine());
                for (int i = 0; i < nodeCount; i++) 
                {
                    line = reader.ReadLine();
                    forest.Enqueue(new HuffmanTree(int.Parse(line)) { Rank = 1});
                }
            }

            return forest;
        }

    }


}
