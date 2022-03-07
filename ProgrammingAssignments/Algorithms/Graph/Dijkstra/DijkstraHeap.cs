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

        public List<DijkstraNode> Find(int nodeId = 0, int referencedNodeId = 0)
        {
            List<DijkstraNode> foundNodes = new List<DijkstraNode>();

            if (_heap.Count == 0) return foundNodes;
            if (nodeId == 0 && referencedNodeId == 0) return foundNodes;
            if (nodeId != 0 && referencedNodeId != 0) throw new ArgumentException("Only nodeId or referencedNodeId can be set");

            Queue<DijkstraNode> nodesQueue = new Queue<DijkstraNode>();
            nodesQueue.Enqueue(Peek());

            while (nodesQueue.Count > 0) 
            {
                DijkstraNode node = nodesQueue.Dequeue();
                if (node.NodeId == nodeId || node.ReferencedNode.NodeId == referencedNodeId)
                {
                    foundNodes.Add(node);
                }
                // left node
                int childIndex = node.Index * 2 + 1;
                if (childIndex < _heap.Count) nodesQueue.Enqueue(_heap[childIndex]);

                // right node
                childIndex++;
                if (childIndex < _heap.Count) nodesQueue.Enqueue(_heap[childIndex]);
            }

            return foundNodes;

        }

        public DijkstraNode Remove(int index)
        {
            DijkstraNode node;
            node = _heap[index];
           
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
                    _heap[leftChildIndex] < _heap[index])
                    minChildIndex = leftChildIndex;

                if (_heap.Count > rightChildIndex &&
                    _heap[rightChildIndex] != null &&
                    _heap[rightChildIndex] < _heap[index] &&
                    _heap[rightChildIndex] < _heap[leftChildIndex])
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
                if (_heap[index] < _heap[parentIndex])
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
            int[] values = _heap.Select(h => h.Value).ToArray();

            return values;
        }
    }
}
