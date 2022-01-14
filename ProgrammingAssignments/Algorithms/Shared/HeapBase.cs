﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Algorithms.Shared
{
    public abstract class HeapBase<T> where T : NodeBase
    {
        internal List<T> _heap { get; set; } = new List<T>();
        public int Count { get { return _heap.Count; } }

        public void Enqueue(T node)
        {
            _heap.Add(node);
            ReheapUp();
        }

        public T Dequeue()
        {
            T head = null;
            if (_heap.Count > 0)
            {
                head = _heap[0];
                ReplaceWithLast(0);
                ReheapDown(0);
            }

            return head;
        }

        public (int, T) Find(int value)
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

        protected abstract void ReheapDown(int index);


        protected abstract void ReheapUp();

        public int[] GetValues()
        {
            int[] values = _heap.Select(h => h.Value).ToArray();

            return values;
        }
    }
}
