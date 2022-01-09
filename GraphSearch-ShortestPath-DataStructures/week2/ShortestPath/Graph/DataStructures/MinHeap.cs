using System.Collections.Generic;
using System.Linq;

namespace Graph.DataStructures
{
    public class MinHeap
    {
        private List<Node> _heap { get; set; } = new List<Node>();
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
                _heap[0] = null;
                ReheapDown();
            }

            return head;
        }

        public Node Find(int value) 
        {
            if (_heap == null || _heap.Count == 0) return null;

            return Find(0, value);
        }

        private Node Find(int index, int value) 
        {
            if (index > _heap.Count - 1) return null;
            if (_heap[index].NodeId == value) return _heap[index];
            Node node = null;
            int leftChildIndex = (index * 2) + 1;
            node = Find(leftChildIndex, value);
            if (node != null) return node;
            int rightChildIndex = leftChildIndex + 1;
            node = Find(rightChildIndex, value);

            return node;
        }

        internal void ReheapDown() 
        {
            int last = _heap.Count - 1;
            if (last <= 0) return;

            _heap[0] = _heap[last];
            _heap.RemoveAt(last);

            int index = 0, minChildIndex, leftChildIndex, rightChildIndex;
            do
            {
                minChildIndex = 0;
                leftChildIndex = index * 2 + 1;
                rightChildIndex = leftChildIndex + 1;

                if (_heap.Count > leftChildIndex && _heap[leftChildIndex] != null && _heap[leftChildIndex].NodeId < _heap[index].NodeId)
                    minChildIndex = leftChildIndex;

                if (_heap.Count > rightChildIndex && _heap[rightChildIndex] != null && _heap[rightChildIndex].NodeId < _heap[leftChildIndex].NodeId)
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
