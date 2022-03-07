using System.Collections.Generic;
using System.Linq;
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
            heap.Enqueue(new DijkstraNode(1, 0, 2, 1));
            int[] values = heap.GetValues();
            Assert.AreEqual(1, values.Length);
            Assert.AreEqual(0, values[0]);
        }

        /// <summary>
        /// I'd like to have arrays of DijkstraNode but it doesn't seem you can do that 
        /// </summary>
        /// <param name="insertNodes"></param>
        /// <param name="distances"></param>
        /// <param name="retrievedValues"></param>
        [TestCase(new[] { 1, 2}, new[] { 0, 1 }, new[] { 0, 1})] // insert 0 then 1, nodes stay in same order
        [TestCase(new[] { 2, 1 }, new[] { 1, 0 }, new[] { 0, 1 })] // insert 1 then 0, 0 is reheaped up since it's the min
        [TestCase(new[] { 1, 2, 3 }, new[] { 0, 1, 2}, new[] { 0, 1, 2 })] // insert nodes in order, they should keep same order
        [TestCase(new[] { 3, 2, 1 }, new[] { 2, 1, 0 }, new[] { 0, 2, 1 })] // 2, then 1 -> replaces 2, 0 -> inserted as right child and replaces 1 (0, 2, 1)
        [TestCase(new[] { 6, 4, 1, 3, 5, 2 }, new[] { 5, 3, 0, 2, 4, 1 }, new[] { 0, 2, 1, 5, 4, 3 })]
        /* nodes depicted like 6,5: node id 6, distance 5
         
            6,5      4,3         1,0           1,0           1,0             1,0
                     /          /   \         /   \          /  \          /     \
                   6,5        6,5   4,3     3,2   4,3     3,2    4,3     3,2      2,1
                                            /             /   \         /   \     / 
                                          6,5           6,5   5,4      6,5  5,4  4,3
        */
        public void HeapEnqueueSetsNodesInExpectedPlaces(int[] nodeIds, int[] distances, int[] expectedNodeIdsFromHeap)
        {
            DijkstraHeap heap = new DijkstraHeap();
            DijkstraNode node;
            for (int i = 0; i < nodeIds.Length; i++) 
            {
                node = new DijkstraNode(nodeIds[i], distances[i]); 
                heap.Enqueue(node);
            }

            int[] values = heap.GetValues();
            Assert.AreEqual(nodeIds.Length, values.Length);
            Assert.AreEqual(expectedNodeIdsFromHeap, values);
        }

        /* nodes depicted like 6,5: node id 6, distance 5
         
            6,5      4,3         1,0           1,0           1,0             1,0
                     /          /   \         /   \          /  \          /     \
                   6,5        6,5   4,3     3,2   4,3     3,2    4,3     3,2      2,1
                                            /             /   \         /   \     / 
                                          6,5           6,5   5,4      6,5  5,4  4,3
        */
        [Test]
        public void HeapDequeuesValuesInOrder()
        {

            DijkstraHeap heap = new DijkstraHeap();

            // setup: Enqueue nodes in order. We don't care about referenced nodes for this test, just the distances.
            heap.Enqueue(new DijkstraNode(6, 5));
            heap.Enqueue(new DijkstraNode(4, 3));
            heap.Enqueue(new DijkstraNode(1, 0));
            heap.Enqueue(new DijkstraNode(3, 2));
            heap.Enqueue(new DijkstraNode(5, 4));
            heap.Enqueue(new DijkstraNode(2, 1));

            int expectedValue = 0;
            while (expectedValue < 6)
            {
                DijkstraNode node = heap.Dequeue();
                Assert.AreEqual(expectedValue, node.Value);
                expectedValue++;
            }
        }

        [Test]
        /* nodes depicted like 6,5,3 node 6, total distance 5, 3 from parent node
         
                    1,0,0
                   /     \
                3,2,2     2,1,1
              /   \         / 
           6,5,3  5,4,2  4,3,2
        */
        public void ReHeapUpLeavesBalancedHeap()
        {
            DijkstraHeap heap = new DijkstraHeap();
            
            // setup: enqueue all nodes except 2

            heap.Enqueue(new DijkstraNode(1, 0, 3, 2));
            heap.Enqueue(new DijkstraNode(1, 0, 2, 1));
            heap.Enqueue(new DijkstraNode(3, 2, 6, 5));
            heap.Enqueue(new DijkstraNode(4, 3));
            heap.Enqueue(new DijkstraNode(3, 2, 5, 4));
            heap.Enqueue(new DijkstraNode(6, 5));
            heap.Enqueue(new DijkstraNode(5, 4));

            // setup: add 2 manually to end
            DijkstraNode node = new DijkstraNode(2, 1, 4, 2);
            heap._heap.Add(node);
            heap.ReheapUp();
            int[] expectedValues = new int[] { 0, 0, 2, 1, 2, 5, 4, 3 };
            Assert.AreEqual(expectedValues, heap.GetValues());
        }


        [Test]
        /* nodes depicted like 6,5,3 node 6, total distance 5, 3 from parent node
         
                    1,0,0
                   /     \
                3,2,2     2,1,1
              /   \         / 
           6,5,3  5,4,2  4,3,2
        */
        public void ReHeapUpLeavesIndexesCorrectlySet()
        {
            DijkstraHeap heap = new DijkstraHeap();
            heap.Enqueue(new DijkstraNode(1, 0, 3, 2));
            heap.Enqueue(new DijkstraNode(1, 0, 2, 1));
            heap.Enqueue(new DijkstraNode(3, 2, 6, 5));
            heap.Enqueue(new DijkstraNode(4, 3));
            heap.Enqueue(new DijkstraNode(3, 2, 5, 4));
            heap.Enqueue(new DijkstraNode(6, 5));
            heap.Enqueue(new DijkstraNode(5, 4));

            // setup: add 2 manually to end
            DijkstraNode node = new DijkstraNode(2, 1, 4, 2);
            heap._heap.Add(node);
            heap.ReheapUp();

            for (int i = 0; i < heap.Count; i++)
            {
                Assert.AreEqual(heap._heap[i].Index, i);
            }
        }

        [Test]
        /* nodes depicted like 6,5,3 node 6, total distance 5, 3 from parent node
         
                    1,0,0
                   /     \
                3,2,2     2,1,1
              /   \         / 
           6,5,3  5,4,2  4,3,2
        */
        public void ReHeapDownFromTopLeavesBalancedHeap()
        {
            const int index = 0;
            // setup: enqueue nodes in heap, missing node 4
            DijkstraHeap heap = new DijkstraHeap();
            heap.Enqueue(new DijkstraNode(1, 0, 3, 2));
            heap.Enqueue(new DijkstraNode(1, 0, 2, 1));
            heap.Enqueue(new DijkstraNode(3, 2, 6, 5));
            heap.Enqueue(new DijkstraNode(3, 2, 5, 4));
            heap.Enqueue(new DijkstraNode(6, 5));
            heap.Enqueue(new DijkstraNode(5, 4));

            // setup: manually put node 4 in index position 
            DijkstraNode node = new DijkstraNode(4, 3);
            heap._heap.Insert(index, node);

            heap.ReheapDown(index);
            int[] heapDValues = heap.GetValues();
            int[] expectedValues = new int[] { 0, 2, 0, 3, 2, 5, 4 };
            Assert.AreEqual(expectedValues, heapDValues);
        }

        [TestCase(0)]
        [TestCase(4)]
        /* nodes depicted like 6,5,3 node 6, total distance 5, 3 from parent node
         
                    1,0,0
                   /     \
                3,2,2     2,1,1
              /   \         / 
           6,5,3  5,4,2  4,3,2
        */
        public void ReHeapDownLeavesIndexesCorrectlySet(int index)
        {
            // setup: enqueue nodes in heap, missing node 4
            DijkstraHeap heap = new DijkstraHeap();
            heap.Enqueue(new DijkstraNode(1, 0, 3, 2));
            heap.Enqueue(new DijkstraNode(1, 0, 2, 1));
            heap.Enqueue(new DijkstraNode(3, 2, 6, 5));
            heap.Enqueue(new DijkstraNode(3, 2, 5, 4));
            heap.Enqueue(new DijkstraNode(6, 5));
            heap.Enqueue(new DijkstraNode(5, 4));

            // setup: manually put node 4 in index position
            DijkstraNode node = new DijkstraNode(4, 3);
            heap._heap.Insert(index, node);
            // reset indexes since we manually added to the array
            for (int i = 0; i < heap.Count; i++)
            {
               heap._heap[i].Index = i;
            }

            heap.ReheapDown(index);
            for (int i = 0; i < heap.Count; i++)
            {
                Assert.AreEqual(heap._heap[i].Index, i);
            }
        }


        [Test]
        public void HeapMaintainsCorrectIndexesWhenInsertingNodes()
        {
            List<DijkstraNode> nodes = new List<DijkstraNode>
            {
                new DijkstraNode(6, 5, 1, 1),
                new DijkstraNode(4, 3, 5, 1),
                new DijkstraNode(1, 0, 2, 1),
                new DijkstraNode(3, 2, 4, 1),
                new DijkstraNode(5, 4, 6, 1),
                new DijkstraNode(2, 1, 3, 1)
            };

            DijkstraHeap heap = new DijkstraHeap();
            foreach (DijkstraNode node in nodes)
            {
                heap.Enqueue(node);
            }

            for (int i = 0; i < heap.Count; i++) 
            {
                Assert.AreEqual(heap._heap[i].Index, i);
            }
        }

        /* nodes depicted like 6-5+1: node 6, distance 5, referenced Node distance 1
         
                    1-0+1
                 /        \
              3-2+1      2-1+1
              /   \        / 
           6-5+1  5-4+1  4-3+1
        */
        [TestCase(new[] { 4, 3, 2, 6, 5 }, new[] { 3, 2, 1, 5, 4 }, new[] { 5, 4, 3, 1, 6 }, new[] { 1, 1, 1, 1, 1 }, new[] { 2, 3, 4, 6, 5 }, 0)]
        [TestCase(new[] { 1, 3, 4, 6, 5, 2 }, new[] { 0, 2, 3, 5, 4, 1 }, new[] { 2, 4, 5, 1, 6, 3 }, new[] { 1, 1, 1, 1, 1, 1 }, new[] { 1, 3, 2, 6, 5, 4 }, 2)]
        public void FindReturnsCorrectNode(int[] nodeIds, int[] distances, int[] referencedNodeIds, int[] referencedNodeDistances, int[] expectedDijkstraValuesFromHeap, int index)
        {
            DijkstraHeap heap = new DijkstraHeap();
            DijkstraNode node;

            for (int i = 0; i < nodeIds.Length; i++)
            {
                node = new DijkstraNode(nodeIds[i], distances[i], referencedNodeIds[i], referencedNodeDistances[i]);
                heap._heap.Add(node);
                heap._heap[i].Index = i;
            }

            heap.ReheapDown(index);
            foreach (int nodeId in nodeIds)
            {
                node = heap.Find(nodeId: nodeId).FirstOrDefault();
                Assert.NotNull(node);
                Assert.AreEqual(nodeId, node.NodeId);
            }
        }


        /* nodes depicted like 6-5+1: node 6, distance 5, referenced Node distance 1
         
                    1-0+1
                 /        \
              3-2+1      2-1+1
              /   \        / 
           6-5+1  5-4+1  4-3+1
        */
        [TestCase(new[] { 4, 3, 2, 6, 5 }, new[] { 3, 2, 1, 5, 4 }, new[] { 5, 4, 3, 1, 6 }, new[] { 1, 1, 1, 1, 1 }, new[] { 2, 3, 4, 6, 5 }, 0)]
        [TestCase(new[] { 1, 3, 4, 6, 5, 2 }, new[] { 0, 2, 3, 5, 4, 1 }, new[] { 2, 4, 5, 1, 6, 3 }, new[] { 1, 1, 1, 1, 1, 1 }, new[] { 1, 3, 2, 6, 5, 4 }, 2)]
        public void FindByReferencedNodeReturnsCorrectNode(int[] nodeIds, int[] distances, int[] referencedNodeIds, int[] referencedNodeDistances, int[] expectedDijkstraValuesFromHeap, int index)
        {
            DijkstraHeap heap = new DijkstraHeap();
            DijkstraNode node;

            for (int i = 0; i < nodeIds.Length; i++)
            {
                node = new DijkstraNode(nodeIds[i], distances[i], referencedNodeIds[i], referencedNodeDistances[i]);
                heap._heap.Add(node);
                heap._heap[i].Index = i;
            }

            heap.ReheapDown(index);
            for (int i = 0; i < nodeIds.Length; i++)
            {
                node = heap.Find(referencedNodeId: referencedNodeIds[i]).FirstOrDefault();
                Assert.NotNull(node);
                Assert.AreEqual(node.NodeId, nodeIds[i]);
            }
        }

        [Test]
        /* nodes depicted like 6-5+1: node 6, distance 5, referenced Node distance 1
                1-0+1
              /       \
           3-2+1    2-1+1
           /   \      / 
        6-5+1  5-4+1  4-3+1
        */
        public void RemoveAffectsExpectedNode()
        {
            List<DijkstraNode> nodes = new List<DijkstraNode>
            {
                new DijkstraNode(6, 5, 1, 1),
                new DijkstraNode(4, 3, 5, 1),
                new DijkstraNode(1, 0, 2, 1),
                new DijkstraNode(3, 2, 4, 1),
                new DijkstraNode(5, 4, 6, 1),
                new DijkstraNode(2, 1, 3, 1)
            };

            DijkstraHeap heap = new DijkstraHeap();
            foreach (DijkstraNode node in nodes)
            {
                heap.Enqueue(node);
            }

            DijkstraNode found = heap.Find(nodeId: 5).FirstOrDefault();
            Assert.NotNull(found);
            DijkstraNode returnNode = heap.Remove(found.Index);
            Assert.NotNull(returnNode);
            Assert.False(heap._heap.Any(n => n.NodeId == 5));
        }

        [Test]
        /* nodes depicted like 6-5+1: node 6, distance 5, referenced Node distance 1
                1-0+1
              /       \
           3-2+1    2-1+1
           /   \      / 
        6-5+1  5-4+1  4-3+1
        */
        public void RemoveLeavesIndexesCorrect()
        {
            List<DijkstraNode> nodes = new List<DijkstraNode>
            {
                new DijkstraNode(6, 5, 1, 1),
                new DijkstraNode(4, 3, 5, 1),
                new DijkstraNode(1, 0, 2, 1),
                new DijkstraNode(3, 2, 4, 1),
                new DijkstraNode(5, 4, 6, 1),
                new DijkstraNode(2, 1, 3, 1)
            };

            DijkstraHeap heap = new DijkstraHeap();
            foreach (DijkstraNode node in nodes)
            {
                heap.Enqueue(node);
            }

            heap.Remove(5);

            for (int i = 0; i < heap.Count; i++)
            {
                Assert.AreEqual(heap._heap[i].Index, i);
            }
        }
    }
}
