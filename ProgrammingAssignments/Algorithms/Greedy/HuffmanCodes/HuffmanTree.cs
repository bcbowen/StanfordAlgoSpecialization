using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Greedy.HuffmanCodes
{
    public class HuffmanTree
    {
        public HuffmanTree() { }

        public HuffmanTree(long value)
        {
            Value = value;
        }

        public long Value { get; set; }
        public int ChildCount { get; set; }
        public int Rank { get; set; }

        public int MinSize
        {
            get
            {
                int size = 0;
                HuffmanTree tree = Left;
                while (tree != null)
                {
                    size++;
                    tree = tree.Left;
                }
                return size;
            }
        }

        public int MaxSize
        {
            get{ return Rank; }
        }

        public HuffmanTree Left { get; set; } = null;

        public HuffmanTree Right { get; set; } = null;

        public static HuffmanTree Merge(HuffmanTree t1, HuffmanTree t2) 
        {
            HuffmanTree parent = new HuffmanTree()
            {
                ChildCount = t1.ChildCount + t2.ChildCount,
                Value = t1.Value + t2.Value,
                Rank = System.Math.Max(t1.Rank, t2.Rank) + 1
            };

            if (t1.Value < t2.Value)
            {
                parent.Left = t1;
                parent.Right = t2;
            }
            else 
            {
                parent.Left = t2;
                parent.Right = t1;
            }

            return parent;

        }
    }
}
