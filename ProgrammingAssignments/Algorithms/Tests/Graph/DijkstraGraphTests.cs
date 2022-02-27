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
        public void NodesHaveCorrectShortestPathAfterDijkstra(int value, int expectedDistance)
        {
            List<DijkstraNode> nodes= LoadTestNodes();
            Algorithm.CalculateShortestPaths(nodes, 1);

            DijkstraNode node = nodes.FirstOrDefault(n => n.NodeId == value);

            Assert.NotNull(node);

            Assert.AreEqual(expectedDistance, node.Value);
        }

       
        [Test]
        public void SimpleTest() 
        {
            // https://www.coursera.org/learn/algorithms-graphs-data-structures/discussions/forums/wuLMg3b0EeahUxJTh9j9Fw/threads/BCALP6nMEea_PRKBTIJnuA

            List<DijkstraNode> nodes = new List<DijkstraNode>();
            // 1	2,1	8,2
            nodes.Add(new DijkstraNode(1, int.MaxValue, 2, 1));
            nodes.Add(new DijkstraNode(1, int.MaxValue, 8, 2));
            // 2   1,1 3,1
            nodes.Add(new DijkstraNode(2, int.MaxValue, 1, 1));
            nodes.Add(new DijkstraNode(2, int.MaxValue, 3, 1));
            // 3   2,1 4,1
            nodes.Add(new DijkstraNode(3, int.MaxValue, 2, 1));
            nodes.Add(new DijkstraNode(3, int.MaxValue, 4, 1));
            // 4   3,1 5,1
            nodes.Add(new DijkstraNode(4, int.MaxValue, 3, 1));
            nodes.Add(new DijkstraNode(4, int.MaxValue, 5, 1));
            // 5   4,1 6,1
            nodes.Add(new DijkstraNode(5, int.MaxValue, 4, 1));
            nodes.Add(new DijkstraNode(5, int.MaxValue, 6, 1));
            //6   5,1 7,1
            nodes.Add(new DijkstraNode(6, int.MaxValue, 5, 1));
            nodes.Add(new DijkstraNode(6, int.MaxValue, 7, 1));
            // 7   6,1 8,1
            nodes.Add(new DijkstraNode(7, int.MaxValue, 6, 1));
            nodes.Add(new DijkstraNode(7, int.MaxValue, 8, 1));
            // 8   7,1 1,2
            nodes.Add(new DijkstraNode(8, int.MaxValue, 7, 1));
            nodes.Add(new DijkstraNode(8, int.MaxValue, 1, 2));

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

            Algorithm.CalculateShortestPaths(nodes, 1);
            Assert.AreEqual(0, nodes.First(n => n.NodeId == 1).Value);
            Assert.AreEqual(1, nodes.First(n => n.NodeId == 2).Value);
            Assert.AreEqual(2, nodes.First(n => n.NodeId == 3).Value);
            Assert.AreEqual(3, nodes.First(n => n.NodeId == 4).Value);
            Assert.AreEqual(4, nodes.First(n => n.NodeId == 5).Value);
            Assert.AreEqual(4, nodes.First(n => n.NodeId == 6).Value);
            Assert.AreEqual(3, nodes.First(n => n.NodeId == 7).Value);
            Assert.AreEqual(2, nodes.First(n => n.NodeId == 8).Value);
        }

        private List<DijkstraNode> LoadTestNodes()
        {
            List<DijkstraNode> nodes = new List<DijkstraNode>();

            nodes.Add(new DijkstraNode(1, int.MaxValue, 2, 1));
            nodes.Add(new DijkstraNode(1, int.MaxValue, 3, 4));
            nodes.Add(new DijkstraNode(2, int.MaxValue, 3, 2));
            nodes.Add(new DijkstraNode(2, int.MaxValue, 4, 6));
            nodes.Add(new DijkstraNode(3, int.MaxValue, 4, 3));
            return nodes;
        }
    }
}
