using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Algorithms.Shared;

namespace Algorithms.Graph.MedianMaintenance
{
    public class MaintenanceMan
    {
        internal MinHeap<Node> HiHeap { get; set; } = new MinHeap<Node>();
        internal MaxHeap<Node> LoHeap { get; set; } = new MaxHeap<Node>();

        public int Count 
        {
            get { return HiHeap.Count + LoHeap.Count; }
        }
    
        public bool IsEven 
        {
            get { return Count % 2 == 0; }
        }
        

        public void Insert(int value) 
        {
            if (value > HiHeap.Peek())
            {
                HiHeap.Enqueue(new Node(value));
            }
            else if (value < LoHeap.Peek())
            {
                LoHeap.Enqueue(new Node(value));
            }
            else
            {
                InsertMiddle(value);
            }

            RebalanceHeaps();
        }

        private void InsertMiddle(int value) 
        {
            Node node = new Node(value);
            if (HiHeap.Count > LoHeap.Count)
            {
                LoHeap.Enqueue(node);
            }
            else 
            {
                HiHeap.Enqueue(node);
            }
        }

        internal void RebalanceHeaps() 
        {
            int diff = HiHeap.Count - LoHeap.Count;
            int limit = IsEven ? 0 : 1;
            if (diff < -1) 
            {
                while (HiHeap.Count - LoHeap.Count < -limit) 
                {
                    HiHeap.Enqueue(LoHeap.Dequeue());   
                }
            } else if (diff > 1) 
            {
                while (HiHeap.Count - LoHeap.Count > limit)
                {
                    LoHeap.Enqueue(HiHeap.Dequeue());
                }
            }
        }

        public int GetMedian() 
        {
            return 3;
        }

    }
}
