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
                if (Left == null && Right == null) return size; 

                Queue<HuffmanTree> queue = new Queue<HuffmanTree>();
                queue.Enqueue(Left);
                queue.Enqueue(Right);
                HuffmanTree tree;
                while (queue.Count > 0)
                {
                    tree = queue.Dequeue();
                    if (tree.Left == null && tree.Right == null)
                    {
                        size = Rank - tree.Rank;
                        break;
                    }
                    else 
                    {
                        queue.Enqueue(tree.Left);
                        queue.Enqueue(tree.Right);
                    }
                }

                return size;
            }
        }

        public int MaxSize
        {
            get{ return Rank - 1; }
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

            SetRanks(parent);
            
            return parent;

        }

        public static void SetRanks(HuffmanTree tree)
        {
            Queue<HuffmanTree> queue = new Queue<HuffmanTree>();
            if (tree.Left != null) 
            {
                tree.Left.Rank = tree.Rank - 1;
                queue.Enqueue(tree.Left); 
            }

            if (tree.Right != null)
            {
                tree.Right.Rank = tree.Rank - 1;    
                queue.Enqueue(tree.Right); 
            }

            HuffmanTree current; 
            while (queue.Count > 0) 
            {
                current = queue.Dequeue();
                if (current.Left != null) 
                {
                    current.Left.Rank = current.Rank - 1;
                    queue.Enqueue(current.Left);
                }
                if (current.Right != null)
                {
                    current.Right.Rank = current.Rank - 1;
                    queue.Enqueue(current.Right);
                }
            }

        }
    }
}
