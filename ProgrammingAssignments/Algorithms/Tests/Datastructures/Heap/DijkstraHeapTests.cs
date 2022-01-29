using NUnit.Framework;
using Algorithms.Graph.Dijkstra;
using Algorithms.Shared;

namespace Algorithms.Tests.Datastructures.Heap
{
    [TestFixture]
    public class DijkstraHeapTests
    {
        [Test]
        public void OneNodeHeapInitializes()
        {
            DijkstraHeap heap = new DijkstraHeap();
            heap.Enqueue(new DijkstraNode(1, 2, 1));
            int[] values = heap.GetValues();
            Assert.AreEqual(1, values.Length);
            Assert.AreEqual(1, values[0]);
        }

        /// <summary>
        /// I'd like to have arrays of DijkstraNode but it doesn't seem you can do that 
        /// </summary>
        /// <param name="insertNodes"></param>
        /// <param name="distances"></param>
        /// <param name="retrievedValues"></param>
        [TestCase(new[] { 1, 2}, new[] { 0, 1 }, new[] { 2, 3 }, new[] { 1, 1 }, new[] { 1, 2})] // insert 1 then 2, nodes stay in same order
        [TestCase(new[] { 2, 1 }, new[] { 1, 0 }, new[] { 3, 2 }, new[] { 1, 1 }, new[] { 1, 2 })] // insert 2 then 1, 1 is reheaped up since it's the min
        [TestCase(new[] { 1, 2, 3 }, new[] { 0, 1, 2}, new[] {  2, 3, 4 }, new[] { 1, 1, 1 }, new[] { 1, 2, 3 })] // insert nodes in order, they should keep same order
        [TestCase(new[] { 3, 2, 1 }, new[] { 2, 1, 0 }, new[] { 4, 3, 2 }, new[] { 1, 1, 1 }, new[] { 1, 3, 2 })] // 3, then 2 -> replaces 3 (2, 3), 1 -> inserted as right child and replaces 2 (1, 3, 2)
        [TestCase(new[] { 6, 4, 1, 3, 5, 2 }, new[] { 5, 3, 0, 2, 4, 1 }, new[] { 1, 5, 2, 4, 6, 3}, new[] { 1, 1, 1, 1, 1, 1}, new[] { 1, 3, 2, 6, 5, 4 })]
        /* nodes depicted like 6-5+1: node 6, distance 5, referenced Node distance 1
         
            6-5+1      4-3+1        1-0+1           1-0+1              1-0+1              1-0+1
                      /             /   \           /   \             /    \            /       \
                   6-5+1         6-5+1  4-3+1     3-2+1  4-3+1      3-2+1  4-3+1      3-2+1    2-1+1
                                                  /                /   \             /   \      / 
                                                6-5+1            6-5+1 5-4+1     6-5+1  5-4+1  4-3+1
        */
        public void HeapEnqueueSetsNodesInExpectedPlaces(int[] nodeIds, int[] distances, int[] referencedNodeIds, int[] referencedNodeDistances, int[] expectedNodeIdsFromHeap)
        {
            DijkstraHeap heap = new DijkstraHeap();
            DijkstraNode node;
            for (int i = 0; i < nodeIds.Length; i++) 
            {
                node = new DijkstraNode(nodeIds[i], referencedNodeIds[i], referencedNodeDistances[i]); 
                node.Distance = distances[i];
                heap.Enqueue(node);
            }

            int[] values = heap.GetValues();
            Assert.AreEqual(nodeIds.Length, values.Length);
            Assert.AreEqual(expectedNodeIdsFromHeap, values);
        }

        /* nodes depicted like 6-5+1: node 6, distance 5, referenced Node distance 1
         
            6-5+1      4-3+1        1-0+1           1-0+1              1-0+1              1-0+1
                      /             /   \           /   \             /    \            /       \
                   6-5+1         6-5+1  4-3+1     3-2+1  4-3+1      3-2+1  4-3+1      3-2+1    2-1+1
                                                  /                /   \             /   \      / 
                                                6-5+1            6-5+1 5-4+1     6-5+1  5-4+1  4-3+1
        */
        [TestCase(new[] { 6, 4, 1, 3, 5, 2 }, new[] { 5, 3, 0, 2, 4, 1 }, new[] { 1, 5, 2, 4, 6, 3 }, new[] { 1, 1, 1, 1, 1, 1 })]
        public void HeapDequeuesValuesInOrder(int[] nodeIds, int[] distances, int[] referencedNodeIds, int[] referencedNodeDistances)
        {

            DijkstraHeap heap = new DijkstraHeap();
            DijkstraNode node;
            for (int i = 0; i < nodeIds.Length; i++)
            {
                node = new DijkstraNode(nodeIds[i], referencedNodeIds[i], referencedNodeDistances[i]);
                node.Distance = distances[i];
                heap.Enqueue(node);
            }

            int expectedValue = 1;
            while (expectedValue < 7)
            {
                node = heap.Dequeue();
                Assert.AreEqual(expectedValue, node.Value);
                expectedValue++;
            }
        }


        /*
         
                    1-0+1
                 /        \
              3-2+1      2-1+1
              /   \        / 
           6-5+1  5-4+1  4-3+1
        */
        [TestCase(new[] { 2, 3, 4, 6, 5, 1 }, new[] { 1, 2, 3, 5, 4, 0}, new[] { 3, 4, 5, 1, 6, 2 }, new[] { 1, 1, 1, 1, 1, 1 }, new[] { 1, 3, 2, 6, 5, 4 })]
        [TestCase(new[] { 1, 3, 4, 6, 5, 2 }, new[] { 0, 2, 3, 5, 4, 1 }, new[] { 2, 4, 5, 1, 6, 3 }, new[] { 1, 1, 1, 1, 1, 1 }, new[] { 1, 3, 2, 6, 5, 4 })]
        public void ReHeapUpLeavesBalancedHeap(int[] nodeIds, int[] distances, int[] referencedNodeIds, int[] referencedNodeDistances, int[] expectedNodeIdsFromHeap)
        {
            DijkstraHeap heap = new DijkstraHeap();
            DijkstraNode node;

            for (int i = 0; i < nodeIds.Length; i++)
            {
                node = new DijkstraNode(nodeIds[i], referencedNodeIds[i], referencedNodeDistances[i]);
                node.Distance = distances[i];
                heap._heap.Add(node);
            }

            heap.ReheapUp();
            Assert.AreEqual(expectedNodeIdsFromHeap, heap.GetValues());
        }

        /*
         
                    1-0+1
                 /        \
              3-2+1      2-1+1
              /   \        / 
           6-5+1  5-4+1  4-3+1
        */
        [TestCase(new[] { 4, 3, 2, 6, 5 }, new[] { 3, 2, 1, 5, 4}, new[] { 5, 4, 3, 1, 6 }, new[] { 1, 1, 1, 1, 1}, new[] { 2, 3, 4, 6, 5 }, 0)]
        [TestCase(new[] { 1, 3, 4, 6, 5, 2 }, new[] { 0, 2, 3, 5, 4, 1 }, new[] { 2, 4, 5, 1, 6, 3 }, new[] { 1, 1, 1, 1, 1, 1 }, new[] { 1, 3, 2, 6, 5, 4 }, 2)]
        public void ReHeapDownLeavesBalancedHeap(int[] nodeIds, int[] distances, int[] referencedNodeIds, int[] referencedNodeDistances, int[] expectedNodeIdsFromHeap, int index)
        {
            DijkstraHeap heap = new DijkstraHeap();
            DijkstraNode node;

            for (int i = 0; i < nodeIds.Length; i++)
            {
                node = new DijkstraNode(nodeIds[i], referencedNodeIds[i], referencedNodeDistances[i]);
                node.Distance = distances[i];
                heap._heap.Add(node);
            }

            heap.ReheapDown(index);
            Assert.AreEqual(expectedNodeIdsFromHeap, heap.GetValues());
        }
    }
}
