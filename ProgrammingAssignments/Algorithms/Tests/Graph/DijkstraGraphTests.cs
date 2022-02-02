using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

using Algorithms.Graph.Dijkstra;


namespace Algorithms.Tests.Graph
{
    [TestFixture]
    public class DijkstraGraphTests
    {
        private const string IgnoreMessage = "Dijkstra tests are currently disabled while the files are broken... need to finish refactoring ";
        /*
        [Test]
        public void GraphUnexploredIsEmptyAfterDijkstra()
        {
            DijkstraGraph graph = LoadTestGraph();
            graph.CalculateShortestPaths();

            Assert.AreEqual(0, graph.UnexploredNodes.Count);
        }

        [Test]
        public void GraphFrontierIsEmptyAfterDijkstra()
        {
            DijkstraGraph graph = LoadTestGraph();
            graph.CalculateShortestPaths();

            Assert.AreEqual(0, graph.Frontier.Count);
        }
        
        [Test]
        public void GraphExploredNodesFullAfterDijkstra()
        {
            DijkstraGraph graph = LoadTestGraph();
            graph.CalculateShortestPaths();

            Assert.AreEqual(4, graph.ExploredNodes.Count);
        }
*/
        [Ignore(IgnoreMessage)]
        [TestCase(1, 0)]
        [TestCase(2, 1)]
        [TestCase(3, 3)]
        [TestCase(4, 6)]
        public void NodesHaveCorrectShortestPathAfterDijkstra(int value, int expectedDistance)
        {
            DijkstraGraph graph = LoadTestGraph();
            List<NodeDistance> result = DijkstraGraph.CalculateShortestPaths(graph);
            //graph.CalculateShortestPaths();

            NodeDistance node = result.FirstOrDefault(n => n.NodeId == value);

            Assert.NotNull(node);

            Assert.AreEqual(expectedDistance, node.Distance);
        }

        /*
        [Test]
        public void PruneFrontierDoesNotPruneNodesEarly()
        {
            DijkstraGraph graph = LoadTestGraph();

            DijkstraNode node = graph.UnexploredNodes.Dequeue();
            graph.Frontier.Add(node);

            graph.PruneFrontier();

            Assert.IsEmpty(graph.ExploredNodes);
            Assert.True(graph.Frontier.Any(n => n.Value == 1));

        }
        
        /// <summary>
        /// After adding first 3 nodes to the frontier, node 1 does not have any referenced nodes that are unexplored, 
        /// so it should be pruned
        /// </summary>
        [Test]
        public void PruneFrontierPrunesNodes()
        {
            DijkstraGraph graph = LoadTestGraph();

            for (int i = 0; i < 3; i++)
            {
                graph.Frontier.Add(graph.UnexploredNodes.Dequeue());
            }

            graph.PruneFrontier();

            Assert.True(graph.ExploredNodes.Any(n => n.Value == 1));
            int[] expectedNodes = { 2, 3 };
            Assert.AreEqual(graph.Frontier.Select(n => n.Value).ToArray(), expectedNodes);
        }
*/
        [Ignore(IgnoreMessage)]
        [Test]
        public void SimpleTest() 
        {
            // https://www.coursera.org/learn/algorithms-graphs-data-structures/discussions/forums/wuLMg3b0EeahUxJTh9j9Fw/threads/BCALP6nMEea_PRKBTIJnuA

            DijkstraGraph graph = new DijkstraGraph();
            // 1	2,1	8,2
            graph.Nodes.Add(new DijkstraNode(1, 2, 1));
            graph.Nodes.Add(new DijkstraNode(1, 8, 2));
            // 2   1,1 3,1
            graph.Nodes.Add(new DijkstraNode(2, 1, 1));
            graph.Nodes.Add(new DijkstraNode(2, 3, 1));
            // 3   2,1 4,1
            graph.Nodes.Add(new DijkstraNode(3, 2, 1));
            graph.Nodes.Add(new DijkstraNode(3, 4, 1));
            // 4   3,1 5,1
            graph.Nodes.Add(new DijkstraNode(4, 3, 1));
            graph.Nodes.Add(new DijkstraNode(4, 5, 1));
            // 5   4,1 6,1
            graph.Nodes.Add(new DijkstraNode(5, 4, 1));
            graph.Nodes.Add(new DijkstraNode(5, 6, 1));
            //6   5,1 7,1
            graph.Nodes.Add(new DijkstraNode(6, 5, 1));
            graph.Nodes.Add(new DijkstraNode(6, 7, 1));
            // 7   6,1 8,1
            graph.Nodes.Add(new DijkstraNode(7, 6, 1));
            graph.Nodes.Add(new DijkstraNode(7, 8, 1));
            // 8   7,1 1,2
            graph.Nodes.Add(new DijkstraNode(8, 7, 1));
            graph.Nodes.Add(new DijkstraNode(8, 1, 2));

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
            graph.StartNodeId = 1;
            List<NodeDistance> result = DijkstraGraph.CalculateShortestPaths(graph);
            Assert.AreEqual(0, result.First(n => n.NodeId == 1).Distance);
            Assert.AreEqual(1, result.First(n => n.NodeId == 2).Distance);
            Assert.AreEqual(2, result.First(n => n.NodeId == 3).Distance);
            Assert.AreEqual(3, result.First(n => n.NodeId == 4).Distance);
            Assert.AreEqual(4, result.First(n => n.NodeId == 5).Distance);
            Assert.AreEqual(4, result.First(n => n.NodeId == 6).Distance);
            Assert.AreEqual(3, result.First(n => n.NodeId == 7).Distance);
            Assert.AreEqual(2, result.First(n => n.NodeId == 8).Distance);
        }

        private DijkstraGraph LoadTestGraph()
        {
            DijkstraGraph graph = new DijkstraGraph();
            graph.StartNodeId = 1;
           
            graph.Nodes.Add(new DijkstraNode(1, 2, 1));
            graph.Nodes.Add(new DijkstraNode(1, 3, 4));
            graph.Nodes.Add(new DijkstraNode(2, 3, 2));
            graph.Nodes.Add(new DijkstraNode(2, 4, 6));
            graph.Nodes.Add(new DijkstraNode(3, 4, 3));

            return graph;
        }
    }
}
