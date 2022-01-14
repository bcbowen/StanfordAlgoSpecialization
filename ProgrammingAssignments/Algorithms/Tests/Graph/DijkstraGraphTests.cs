using System.Linq;
using NUnit.Framework;

using Algorithms.Graph.Dijkstra;


namespace Algorithms.Tests.Graph
{
    [TestFixture]
    public class DijkstraGraphTests
    {
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

        [TestCase(1, 0)]
        [TestCase(2, 1)]
        [TestCase(3, 3)]
        [TestCase(4, 6)]
        public void NodesHaveCorrectShortestPathAfterDijkstra(int value, int expectedDistance)
        {
            DijkstraGraph graph = LoadTestGraph();
            graph.CalculateShortestPaths();

            DijkstraNode node = graph.ExploredNodes.FirstOrDefault(n => n.Value == value);

            Assert.NotNull(node);

            Assert.AreEqual(expectedDistance, node.MinDistance);

        }

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

        private DijkstraGraph LoadTestGraph()
        {
            DijkstraGraph graph = new DijkstraGraph();

            DijkstraNode node = new DijkstraNode(1);
            node.ReferencedNodes.Add(new ReferencedNode(2, 1));
            node.ReferencedNodes.Add(new ReferencedNode(3, 4));

            graph.UnexploredNodes.Enqueue(node);

            node = new DijkstraNode(2);
            node.ReferencedNodes.Add(new ReferencedNode(3, 2));
            node.ReferencedNodes.Add(new ReferencedNode(4, 6));

            graph.UnexploredNodes.Enqueue(node);

            node = new DijkstraNode(3);
            node.ReferencedNodes.Add(new ReferencedNode(4, 3));

            graph.UnexploredNodes.Enqueue(node);

            node = new DijkstraNode(4);
            graph.UnexploredNodes.Enqueue(node);

            return graph;
        }
    }
}
