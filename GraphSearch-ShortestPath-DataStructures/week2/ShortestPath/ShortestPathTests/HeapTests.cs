using NUnit.Framework;
using Graph;
using Graph.DataStructures;

namespace ShortestPathTests
{
    [TestFixture]
    public class HeapTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void OneNodeHeapInitializes()
        {
            MinHeap heap = new MinHeap();
            heap.Enqueue(new Node(3));
            int[] values = heap.GetValues();
            Assert.AreEqual(1, values.Length);
            Assert.AreEqual(3, values[0]);
        }

        [TestCase(new[] { 1, 3 }, new[] { 1, 3 })] // insert 1 then 3, nodes stay in same order
        [TestCase(new [] { 3, 1}, new[] { 1, 3 })] // insert 3 then 1, 1 is reheaped up since it's the min
        [TestCase(new[] { 1, 2, 3 }, new[] { 1, 2, 3 })] // insert nodes in order, they should keep same order
        [TestCase(new[] { 3, 2, 1 }, new[] { 1, 3, 2 })] // 3, then 2 -> replaces 3 (2, 3), 1 -> inserted as right child and replaces 2 (1, 3, 2)
        [TestCase(new[] { 6, 4, 1, 3, 5, 2 }, new[] { 1, 3, 2, 6, 5, 4 })]
        /*
            6   4    1       1       1          1
               /    / \     / \     / \       /  \
              6    6   4   3   4   3   4     3     2
                          /       / \       / \   / 
                         6       6   5     6   5 4
        */
        public void HeapEnqueueSetsNodesInExpectedPlaces(int[] insertNodes, int[] retrievedValues )
        {
            MinHeap heap = new MinHeap();
            foreach (int value in insertNodes) 
            {
                heap.Enqueue(new Node(value));
            }
            
            int[] values = heap.GetValues();
            Assert.AreEqual(insertNodes.Length, values.Length);
            Assert.AreEqual(retrievedValues, values);
        }

        [Test]
        public void HeapDequeuesValuesInOrder() 
        {
            int[] values = new[] { 6, 4, 1, 3, 5, 2 };
            MinHeap heap = new MinHeap();

            foreach (int value in values)
            {
                heap.Enqueue(new Node(value));
            }

            int expectedValue = 1;
            while (expectedValue < 7) 
            {
                Node node = heap.Dequeue();
                Assert.AreEqual(expectedValue, node.NodeId);
                expectedValue++;
            }
        }

        /*
                 1
               /  \
              3     2
             / \   / 
            6   5 4
        */
        [TestCase(new[] { 6, 4, 1, 3, 5, 2 }, 6, 3)]
        [TestCase(new[] { 6, 4, 1, 3, 5, 2 }, 4, 5)]
        [TestCase(new[] { 6, 4, 1, 3, 5, 2 }, 1, 0)]
        [TestCase(new[] { 6, 4, 1, 3, 5, 2 }, 3, 1)]
        [TestCase(new[] { 6, 4, 1, 3, 5, 2 }, 5, 4)]
        [TestCase(new[] { 6, 4, 1, 3, 5, 2 }, 2, 2)]
        public void HeapFindReturnsExpectedNode(int[] values, int search, int expectedIndex)
        {
            MinHeap heap = new MinHeap();

            foreach (int value in values)
            {
                heap.Enqueue(new Node(value));
            }

            (int index, Node node) = heap.Find(search);
            Assert.NotNull(node);
            Assert.AreEqual(node.NodeId, search);
            Assert.AreEqual(expectedIndex, index);
        }

        /*
                 1
               /  \
              3     2
             / \   / 
            6   5 4
        */
        [TestCase(new[] { 6, 4, 1, 3, 5, 2 }, 6, new[] { 1, 3, 2, 4, 5 })]
        [TestCase(new[] { 6, 4, 1, 3, 5, 2 }, 4, new[] { 1, 3, 2, 6, 5 })]
        [TestCase(new[] { 6, 4, 1, 3, 5, 2 }, 1, new[] { 2, 3, 4, 6, 5 })]
        [TestCase(new[] { 6, 4, 1, 3, 5, 2 }, 3, new[] { 1, 4, 2, 6, 5 })]
        [TestCase(new[] { 6, 4, 1, 3, 5, 2 }, 5, new[] { 1, 3, 2, 6, 4 })]
        [TestCase(new[] { 6, 4, 1, 3, 5, 2 }, 2, new[] { 1, 3, 4, 6, 5 })]
        public void HeapRemoveReturnsExpectedNodeAndBalancesHeap(int[] nodeValues, int value, int[] expectedHeapState)
        {
            MinHeap heap = new MinHeap();

            foreach (int nv in nodeValues)
            {
                heap.Enqueue(new Node(nv));
            }

            Node node = heap.Remove(value);
            Assert.NotNull(node);
            Assert.AreEqual(node.NodeId, value);
            Assert.AreEqual(expectedHeapState, heap.GetValues());
        }

        /*
                 1
               /  \
              3     2
             / \   / 
            6   5 4
        */
        [TestCase(new[] { 2, 3, 4, 6, 5, 1 }, new[] { 1, 3, 2, 6, 5, 4 })]
        [TestCase(new[] { 1, 3, 4, 6, 5, 2 }, new[] { 1, 3, 2, 6, 5, 4 })]
        public void ReHeapUpLeavesBalancedHeap(int[] beginState, int[] endState)
        {
            MinHeap heap = new MinHeap();

            foreach (int nv in beginState)
            {
                heap.Enqueue(new Node(nv));
            }

            heap.ReheapUp();
            Assert.AreEqual(endState, heap.GetValues());
        }

        /*
                 1
               /  \
              3     2
             / \   / 
            6   5 4
        */
        [TestCase(new[] { 4, 3, 2, 6, 5 }, new[] { 2, 3, 4, 6, 5 }, 0)]
        [TestCase(new[] { 1, 3, 4, 6, 5, 2 }, new[] { 1, 3, 2, 6, 5, 4 }, 2)]
        public void ReHeapDownLeavesBalancedHeap(int[] beginState, int[] endState, int index)
        {
            MinHeap heap = new MinHeap();

            for (int i = 0; i < beginState.Length; i++)
            {
                heap._heap.Add(new Node(beginState[i]));
            }

            heap.ReheapDown(index);
            Assert.AreEqual(endState, heap.GetValues());
        }
    }
}