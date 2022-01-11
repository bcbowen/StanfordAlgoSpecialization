using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Linq;

[assembly: InternalsVisibleTo("ShortestPathTests")]
namespace Graph.DataStructures
{
    public class MinHeap
    {
        internal List<Node> _heap { get; set; } = new List<Node>();
        public int Count { get { return _heap.Count; } }

        public void Enqueue(Node node) 
        {
            _heap.Add(node);
            ReheapUp();
        }

        public Node Dequeue() 
        {
            Node head = null;
            if (_heap.Count > 0) 
            {
                head = _heap[0]; 
                ReplaceWithLast(0);
                ReheapDown(0);
            }

            return head;
        }

        public (int, Node) Find(int value) 
        {
            if (_heap == null || _heap.Count == 0) return (-1, null);

            return Find(0, value);
        }

        private (int, Node) Find(int index, int value) 
        {
            if (index > _heap.Count - 1) return (-1, null);
            if (_heap[index].NodeId == value) return (index, _heap[index]);
            Node node;
            int leftChildIndex = (index * 2) + 1;
            int i;
            (i, node) = Find(leftChildIndex, value);
            if (node != null) return (i, node);
            int rightChildIndex = leftChildIndex + 1;
            return Find(rightChildIndex, value);
        }

        public Node Remove(int value)
        {
            Node node;
            int index; 
            (index, node) = Find(value);
            Debug.Assert(index > -1);
            ReplaceWithLast(index);
            ReheapDown(index);
            return node;
        }

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

        internal void ReheapDown(int index) 
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
                    _heap[leftChildIndex].NodeId < _heap[index].NodeId)
                    minChildIndex = leftChildIndex;

                if (_heap.Count > rightChildIndex && 
                    _heap[rightChildIndex] != null &&
                    _heap[rightChildIndex].NodeId < _heap[index].NodeId &&
                    _heap[rightChildIndex].NodeId < _heap[leftChildIndex].NodeId)
                    minChildIndex = rightChildIndex;

                if (minChildIndex != 0)
                {
                    Node temp = _heap[minChildIndex];
                    _heap[minChildIndex] = _heap[index];
                    _heap[index] = temp;
                    index = minChildIndex;
                }

            } while (minChildIndex != 0);
            
        }

        internal void ReheapUp () 
        {
            int index = _heap.Count - 1;
            if (index <= 0) return;
            int parentIndex;
            do 
            {
                parentIndex = (index - 1) / 2;
                if (_heap[index].NodeId < _heap[parentIndex].NodeId)
                {
                    Node temp = _heap[parentIndex];
                    _heap[parentIndex] = _heap[index];
                    _heap[index] = temp;
                    index = parentIndex;
                }
            } while (index > 0 && index == parentIndex);
        }

        public int[] GetValues() 
        {
            int[] values = _heap.Select(h => h.NodeId).ToArray();

            return values;
        }
    }
}
