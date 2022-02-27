using System;
using System.Collections.Generic;
using System.Collections;
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
            node.Index = _heap.Count - 1;

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

        public DijkstraNode Find(int nodeId)
        {
            if (_heap == null || _heap.Count == 0) return null;

            return Find(0, nodeId);
        }
        

        private DijkstraNode Find(int index, int nodeId)
        {
            if (index > _heap.Count - 1) return null;

            Queue<DijkstraNode> nodesQueue = new Queue<DijkstraNode>();
            nodesQueue.Enqueue(_heap[index]);

            while (nodesQueue.Count > 0) 
            {
                DijkstraNode node = nodesQueue.Dequeue();
                if (node.NodeId == nodeId)
                {
                    return node;
                }
                // left node
                int childIndex = node.Index * 2 + 1;
                if (childIndex < _heap.Count) nodesQueue.Enqueue(_heap[childIndex]);

                // right node
                childIndex++;
                if (childIndex < _heap.Count) nodesQueue.Enqueue(_heap[childIndex]);
            }

            return null;

        }

        public DijkstraNode Remove(int value)
        {
            DijkstraNode node;
            node = Find(value);
           
            ReplaceWithLast(node.Index);
            ReheapDown(node.Index);
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
            _heap[index].Index = index;
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
                    _heap[minChildIndex].Index = minChildIndex;
                    _heap[index] = temp;
                    _heap[index].Index = index;
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
                    _heap[parentIndex].Index = parentIndex;
                    _heap[index] = temp;
                    _heap[index].Index = index;
                    index = parentIndex;
                }
            } while (index > 0 && index == parentIndex);
        }

        public int[] GetValues()
        {
            int[] values = _heap.Select(h => h.DijkstraValue).ToArray();

            return values;
        }
    }
}
