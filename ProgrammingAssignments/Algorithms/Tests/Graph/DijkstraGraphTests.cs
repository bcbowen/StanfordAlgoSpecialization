using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

using Algorithms.Graph.Dijkstra;


namespace Algorithms.Tests.Graph
{
    [TestFixture]
    public class DijkstraGraphTests
    {
       
        [TestCase(1, 0)]
        [TestCase(2, 1)]
        [TestCase(3, 3)]
        [TestCase(4, 6)]
        public void NodesHaveCorrectShortestPathAfterDijkstra(int nodeId, int expectedDistance)
        {
            List<DijkstraNode> nodes= LoadTestNodes();
            Dictionary<int, int> paths = Algorithm.CalculateShortestPaths(nodes, 1);
            Assert.True(paths.ContainsKey(nodeId));

            int length = paths[nodeId];

            Assert.AreEqual(expectedDistance, length);
        }

       
        [Test]
        public void SimpleTest() 
        {
            // https://www.coursera.org/learn/algorithms-graphs-data-structures/discussions/forums/wuLMg3b0EeahUxJTh9j9Fw/threads/BCALP6nMEea_PRKBTIJnuA

            List<DijkstraNode> nodes = new List<DijkstraNode>();
            // 1	2,1	8,2
            nodes.Add(new DijkstraNode(1, DijkstraNode.NoPathDistance, 2, 1));
            nodes.Add(new DijkstraNode(1, DijkstraNode.NoPathDistance, 8, 2));
            // 2   1,1 3,1
            nodes.Add(new DijkstraNode(2, DijkstraNode.NoPathDistance, 1, 1));
            nodes.Add(new DijkstraNode(2, DijkstraNode.NoPathDistance, 3, 1));
            // 3   2,1 4,1
            nodes.Add(new DijkstraNode(3, DijkstraNode.NoPathDistance, 2, 1));
            nodes.Add(new DijkstraNode(3, DijkstraNode.NoPathDistance, 4, 1));
            // 4   3,1 5,1
            nodes.Add(new DijkstraNode(4, DijkstraNode.NoPathDistance, 3, 1));
            nodes.Add(new DijkstraNode(4, DijkstraNode.NoPathDistance, 5, 1));
            // 5   4,1 6,1
            nodes.Add(new DijkstraNode(5, DijkstraNode.NoPathDistance, 4, 1));
            nodes.Add(new DijkstraNode(5, DijkstraNode.NoPathDistance, 6, 1));
            //6   5,1 7,1
            nodes.Add(new DijkstraNode(6, DijkstraNode.NoPathDistance, 5, 1));
            nodes.Add(new DijkstraNode(6, DijkstraNode.NoPathDistance, 7, 1));
            // 7   6,1 8,1
            nodes.Add(new DijkstraNode(7, DijkstraNode.NoPathDistance, 6, 1));
            nodes.Add(new DijkstraNode(7, DijkstraNode.NoPathDistance, 8, 1));
            // 8   7,1 1,2
            nodes.Add(new DijkstraNode(8, DijkstraNode.NoPathDistance, 7, 1));
            nodes.Add(new DijkstraNode(8, DijkstraNode.NoPathDistance, 1, 2));

            /*
                output:
                1 0[]
                2 1[2]
                3 2[2, 3]
                4 3[2, 3, 4]
                5 4[2, 3, 4, 5]
                6 4[8, 7, 6]
                7 3[8, 7]
                8 2[8]
           */

            Dictionary<int, int> paths = Algorithm.CalculateShortestPaths(nodes, 1);
            for (int i = 1; i <= 8; i++) 
            {
                Assert.True(paths.ContainsKey(i));
            }

            Assert.AreEqual(0, paths[1]);
            Assert.AreEqual(1, paths[2]);
            Assert.AreEqual(2, paths[3]);
            Assert.AreEqual(3, paths[4]);
            Assert.AreEqual(4, paths[5]);
            Assert.AreEqual(4, paths[6]);
            Assert.AreEqual(3, paths[7]);
            Assert.AreEqual(2, paths[8]);
        }

        private List<DijkstraNode> LoadTestNodes()
        {
            List<DijkstraNode> nodes = new List<DijkstraNode>();

            nodes.Add(new DijkstraNode(1, DijkstraNode.NoPathDistance, 2, 1));
            nodes.Add(new DijkstraNode(1, DijkstraNode.NoPathDistance, 3, 4));
            nodes.Add(new DijkstraNode(2, DijkstraNode.NoPathDistance, 3, 2));
            nodes.Add(new DijkstraNode(2, DijkstraNode.NoPathDistance, 4, 6));
            nodes.Add(new DijkstraNode(3, DijkstraNode.NoPathDistance, 4, 3));
            return nodes;
        }
    }
}
