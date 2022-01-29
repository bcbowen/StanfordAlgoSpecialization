using System;
using System.Collections.Generic;
using System.Linq;


namespace Algorithms.Graph.Dijkstra
{
    public class DijkstraHeap
    {
        internal List<DijkstraNode> _heap { get; set; } = new List<DijkstraNode>();
        public int Count { get { return _heap.Count; } }

        public void Enqueue(DijkstraNode node)
        {
            _heap.Add(node);
            ReheapUp();
        }

        public DijkstraNode Peek()
        {
            if (_heap.Count == 0) return null;

            return _heap[0];
        }

        public DijkstraNode Dequeue()
        {
            DijkstraNode head = null;
            if (_heap.Count > 0)
            {
                head = _heap[0];
                ReplaceWithLast(0);
                ReheapDown(0);
            }

            return head;
        }

        /*
        public (int, DijkstraNode) Find(int value)
        {
            if (_heap == null || _heap.Count == 0) return (-1, null);


            return Find(0, value);
        }
        

        private (int, T) Find(int index, int value)
        {
            if (index > _heap.Count - 1) return (-1, null);
            if (_heap[index].Value == value) return (index, _heap[index]);
            T node;
            int leftChildIndex = (index * 2) + 1;
            int i;
            (i, node) = Find(leftChildIndex, value);
            if (node != null) return (i, node);
            int rightChildIndex = leftChildIndex + 1;
            return Find(rightChildIndex, value);
        }


        public T Remove(int value)
        {
            T node;
            int index;
            (index, node) = Find(value);
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
                    _heap[leftChildIndex].DijkstraValue < _heap[index].DijkstraValue)
                    minChildIndex = leftChildIndex;

                if (_heap.Count > rightChildIndex &&
                    _heap[rightChildIndex] != null &&
                    _heap[rightChildIndex].DijkstraValue < _heap[index].DijkstraValue &&
                    _heap[rightChildIndex].DijkstraValue < _heap[leftChildIndex].DijkstraValue)
                    minChildIndex = rightChildIndex;

                if (minChildIndex != 0)
                {
                    DijkstraNode temp = _heap[minChildIndex];
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
                if (_heap[index].DijkstraValue < _heap[parentIndex].DijkstraValue)
                {
                    DijkstraNode temp = _heap[parentIndex];
                    _heap[parentIndex] = _heap[index];
                    _heap[index] = temp;
                    index = parentIndex;
                }
            } while (index > 0 && index == parentIndex);
        }

        public int[] GetValues()
        {
            int[] values = _heap.Select(h => h.Value).ToArray();

            return values;
        }
    }
}
