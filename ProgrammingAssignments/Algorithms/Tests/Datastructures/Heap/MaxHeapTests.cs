using System.Collections.Generic;
using NUnit.Framework;
using Algorithms.Graph.Dijkstra;
using Algorithms.Shared;

namespace Algorithms.Tests.Datastructures.Heap
{
    [TestFixture]
    public class MaxHeapTests
    {
        [Test]
        public void OneNodeHeapInitializes()
        {
            MaxHeap<Node> heap = new MaxHeap<Node>();
            heap.Enqueue(new Node(1, 3));
            int[] values = heap.GetValues();
            Assert.AreEqual(1, values.Length);
            Assert.AreEqual(3, values[0]);
        }

        [TestCase(new[] { 3, 1 }, new[] { 3, 1 })] // insert 1 then 3, nodes stay in same order
        [TestCase(new[] { 1, 3 }, new[] { 3, 1 })] // insert 3 then 1, 3 is reheaped up since it's the max
        [TestCase(new[] { 3, 1, 2 }, new[] { 3, 1, 2 })] // insert nodes in order, they should keep same order
        [TestCase(new[] { 1, 2, 3 }, new[] { 3, 1, 2 })] // 1, then 2 -> replaces 1 (2, 1), 3 -> 2 inserted as right child. 3 and replaces 2 (3, 1, 2)
        [TestCase(new[] { 1, 3, 6, 4, 2, 5 }, new[] { 6, 4, 5, 1, 2, 3 })]
        /*
            1   3    6       6       6          6
               /    / \     / \     / \       /  \
              1    1   3   4   3   4   3     4    5
                          /       / \       / \   / 
                         1       1   2     1   2 3
        */
        public void HeapEnqueueSetsNodesInExpectedPlaces(int[] insertNodes, int[] retrievedValues)
        {
            MaxHeap<Node> heap = new MaxHeap<Node>();
            int nodeId = 1;
            foreach (int value in insertNodes)
            {
                heap.Enqueue(new Node(nodeId++, value));
            }

            int[] values = heap.GetValues();
            Assert.AreEqual(insertNodes.Length, values.Length);
            Assert.AreEqual(retrievedValues, values);
        }

        [Test]
        public void HeapDequeuesValuesInOrder()
        {
            int[] values = GetTestInitValues();
            int nodeId = 1;
            MaxHeap<Node> heap = new MaxHeap<Node>();

            foreach (int value in values)
            {
                heap.Enqueue(new Node(nodeId++, value));
            }

            int expectedValue = 6;
            while (expectedValue > 0)
            {
                Node node = heap.Dequeue();
                Assert.AreEqual(expectedValue, node.Value);
                expectedValue--;
            }
        }

        /*
                 6
               /  \
              4    5
             / \   / 
            1   2 3
        */
        [TestCase(6, 0)]
        [TestCase(4, 1)]
        [TestCase(1, 3)]
        [TestCase(3, 5)]
        [TestCase(5, 2)]
        [TestCase(2, 4)]
        public void HeapFindReturnsExpectedNode(int search, int expectedIndex)
        {
            int[] values = GetTestInitValues();
            MaxHeap<Node> heap = new MaxHeap<Node>();

            int nodeId = 1;
            foreach (int value in values)
            {
                heap.Enqueue(new Node(nodeId, value));
            }

            (int index, Node node) = heap.Find(search);
            Assert.NotNull(node);
            Assert.AreEqual(node.Value, search);
            Assert.AreEqual(expectedIndex, index);
        }

        /*
                 6
               /  \
              4    5
             / \   / 
            1   2 3
        */
        [TestCase(6, new[] { 5, 4, 3, 1, 2 })]
        [TestCase(4, new[] { 6, 3, 5, 1, 2 })]
        [TestCase(1, new[] { 6, 4, 5, 3, 2 })]
        [TestCase(3, new[] { 6, 4, 5, 1, 2 })]
        [TestCase(5, new[] { 6, 4, 3, 1, 2 })]
        [TestCase(2, new[] { 6, 4, 5, 1, 3 })]
        public void HeapRemoveReturnsExpectedNodeAndBalancesHeap(int value, int[] expectedHeapState)
        {
            int[] nodeValues = GetTestInitValues(); 
            MaxHeap<Node> heap = new MaxHeap<Node>();
            int nodeId = 1;
            foreach (int nv in nodeValues)
            {
                heap.Enqueue(new Node(nodeId++, nv));
            }

            Node node = heap.Remove(value);
            Assert.NotNull(node);
            Assert.AreEqual(node.Value, value);
            Assert.AreEqual(expectedHeapState, heap.GetValues());
        }

        /*
                 6
               /  \
              4    5
             / \   / 
            1   2 3
        */
        [TestCase(new[] { 5, 4, 3, 1, 2, 6 }, new[] { 6, 4, 5, 1, 2, 3 })]
        [TestCase(new[] { 6, 4, 3, 1, 2, 5 }, new[] { 6, 4, 5, 1, 2, 3 })]
        public void ReHeapUpLeavesBalancedHeap(int[] beginState, int[] endState)
        {
            MaxHeap<Node> heap = new MaxHeap<Node>();
            // manually add nodes to match test case
            heap._heap = new List<Node>(6);
            int nodeId = 1;
            for (int i = 0; i < beginState.Length; i++)
            {
                heap._heap.Add(new Node(nodeId++, beginState[i]));
            }
            heap.ReheapUp();
            Assert.AreEqual(endState, heap.GetValues());
        }

        /*
                 6
               /  \
              4    5
             / \   / 
            1   2 3
        */
        [TestCase(new[] { 3, 4, 6, 1, 2, 5 }, new[] { 6, 4, 5, 1, 2, 3 }, 0)]
        [TestCase(new[] { 6, 4, 3, 1, 2, 5 }, new[] { 6, 4, 5, 1, 2, 3 }, 2)]
        public void ReHeapDownLeavesBalancedHeap(int[] beginState, int[] endState, int index)
        {
            MaxHeap<Node> heap = new MaxHeap<Node>();
            int nodeId = 1;
            for (int i = 0; i < beginState.Length; i++)
            {
                heap._heap.Add(new Node(nodeId++, beginState[i]));
            }

            heap.ReheapDown(index);
            Assert.AreEqual(endState, heap.GetValues());
        }

        private int[] GetTestInitValues() 
        {
            return new []{ 1, 3, 6, 4, 2, 5 };
        }
    }
}
