using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Greedy.HuffmanCodes
{
    public class MinHeap 
    {
        public void ReheapDown(int index)
        {
            int last = _heap.Count - 1;
            if (last <= index) return;

            int minChildIndex, leftChildIndex, rightChildIndex;
            do
            {
                minChildIndex = 0;
                leftChildIndex = index * 2 + 1;
                rightChildIndex = leftChildIndex + 1;

                if (_heap.Count > leftChildIndex &&
                    _heap[leftChildIndex] != null &&
                    _heap[leftChildIndex].Value < _heap[index].Value)
                    minChildIndex = leftChildIndex;

                if (_heap.Count > rightChildIndex &&
                    _heap[rightChildIndex] != null &&
                    _heap[rightChildIndex].Value < _heap[index].Value &&
                    _heap[rightChildIndex].Value < _heap[leftChildIndex].Value)
                    minChildIndex = rightChildIndex;

                if (minChildIndex != 0)
                {
                    HuffmanTree temp = _heap[minChildIndex];
                    _heap[minChildIndex] = _heap[index];
                    _heap[index] = temp;
                    index = minChildIndex;
                }

            } while (minChildIndex != 0);

        }

        public void ReheapUp()
        {
            int index = _heap.Count - 1;
            if (index <= 0) return;
            int parentIndex;
            do
            {
                parentIndex = (index - 1) / 2;
                if (_heap[index].Value < _heap[parentIndex].Value)
                {
                    HuffmanTree temp = _heap[parentIndex];
                    _heap[parentIndex] = _heap[index];
                    _heap[index] = temp;
                    index = parentIndex;
                }
            } while (index > 0 && index == parentIndex);
        }

        internal List<HuffmanTree> _heap { get; set; } = new List<HuffmanTree>();
        public int Count { get { return _heap.Count; } }

        public void Enqueue(HuffmanTree node)
        {
            _heap.Add(node);
            ReheapUp();
        }

        public long Peek()
        {
            if (_heap.Count == 0) return -1;

            return _heap[0].Value;
        }

        public HuffmanTree Dequeue()
        {
            HuffmanTree head = null;
            if (_heap.Count > 0)
            {
                head = _heap[0];
                ReplaceWithLast(0);
                ReheapDown(0);
            }

            return head;
        }

        /*
        public (long, HuffmanTree) Find(long value)
        {
            if (_heap == null || _heap.Count == 0) return (-1, null);

            return Find(0, value);
        }

        private (int, HuffmanTree) Find(int index, int nodeId)
        {
            if (index > _heap.Count - 1) return (-1, null);
            if (_heap[index].NodeId == nodeId) return (index, _heap[index]);
            T node;
            int leftChildIndex = (index * 2) + 1;
            int i;
            (i, node) = Find(leftChildIndex, nodeId);
            if (node != null) return (i, node);
            int rightChildIndex = leftChildIndex + 1;
            return Find(rightChildIndex, nodeId);
        }
        

        public T Remove(int nodeId)
        {
            T node;
            int index;
            (index, node) = Find(nodeId);
            Debug.Assert(index > -1);
            ReplaceWithLast(index);
            ReheapDown(index);
            return node;
        }
        */

        /// <summary>
        ///  Replace the given index in the heap with the last element, reducing the number of elements. 
        /// </summary>
        /// <param name="index"></param>
        /// <returns>true if the index is value, false otherwise</returns>
        public bool ReplaceWithLast(int index)
        {
            int last = _heap.Count - 1;
            if (index > last) return false;

            _heap[index] = _heap[last];
            _heap.RemoveAt(last);
            return true;
        }

        public long[] GetValues()
        {
            long[] values = _heap.Select(h => h.Value).ToArray();

            return values;
        }
    }
}
