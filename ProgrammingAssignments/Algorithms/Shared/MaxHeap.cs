using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Shared
{
    public class MaxHeap<T> : HeapBase<T> where T : NodeBase
    {
        public override void ReheapDown(int index)
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
                    _heap[leftChildIndex].Value > _heap[index].Value)
                    minChildIndex = leftChildIndex;

                if (_heap.Count > rightChildIndex &&
                    _heap[rightChildIndex] != null &&
                    _heap[rightChildIndex].Value > _heap[index].Value &&
                    _heap[rightChildIndex].Value > _heap[leftChildIndex].Value)
                    minChildIndex = rightChildIndex;

                if (minChildIndex != 0)
                {
                    T temp = _heap[minChildIndex];
                    _heap[minChildIndex] = _heap[index];
                    _heap[index] = temp;
                    index = minChildIndex;
                }

            } while (minChildIndex != 0);

        }

        public override void ReheapUp()
        {
            int index = _heap.Count - 1;
            if (index <= 0) return;
            int parentIndex;
            do
            {
                parentIndex = (index - 1) / 2;
                if (_heap[index].Value > _heap[parentIndex].Value)
                {
                    T temp = _heap[parentIndex];
                    _heap[parentIndex] = _heap[index];
                    _heap[index] = temp;
                    index = parentIndex;
                }
            } while (index > 0 && index == parentIndex);
        }
    }
}
