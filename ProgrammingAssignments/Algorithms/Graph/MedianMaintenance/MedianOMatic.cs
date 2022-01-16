using System;
using Algorithms.Shared;

namespace Algorithms.Graph.MedianMaintenance
{
    public class MedianOMatic
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

        /// <summary>
        ///  For assignment we need the sum of all of the medians: 
        ///  In the box below you should type the sum of these 10000 medians, modulo 10000 (i.e., only the last 4 digits).  
        ///  That is, you should compute(m_1 + m_2 + m_3 + ... + m_{ 10000}).
        /// </summary>
        public int RunningMedianTotalRaw { get; private set; }

        /// <summary>
        /// Final 4 digits of median history per assignment (course 2 week 3)
        /// </summary>
        public int RunningMedianTotalMod
        {
            get 
            {
                return RunningMedianTotalRaw % 10000;
            } 
        }


        public void Insert(int value) 
        {
            if (Count == 0 || value < LoHeap.Peek()) 
            {
                LoHeap.Enqueue(new Node(value));
            }
            else if (value > HiHeap.Peek())
            {
                HiHeap.Enqueue(new Node(value));
            }
            else
            {
                InsertMiddle(value);
            }

            RebalanceHeaps();
            RunningMedianTotalRaw += GetMedian();
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
            decimal k = System.Math.Ceiling((decimal)Count / 2);
            
            return k == LoHeap.Count ? LoHeap.Peek() : HiHeap.Peek();
        }

    }
}
