using System.Linq;

using NUnit.Framework;
using Graph;


namespace ShortestPathTests
{
    [TestFixture]
    public class GraphTests
    {
        [Test]
        public void GraphUnexploredIsEmptyAfterDijkstra()
        {
            UndirectedGraph graph = LoadTestGraph();
            graph.CalculateShortestPaths();

            Assert.AreEqual(0, graph.UnexploredNodes.Count);

        }

        [Test]
        public void GraphFrontierIsEmptyAfterDijkstra()
        {
            UndirectedGraph graph = LoadTestGraph();
            graph.CalculateShortestPaths();

            Assert.AreEqual(0, graph.Frontier.Count);
        }

        [Test]
        public void GraphExploredNodesFullAfterDijkstra()
        {
            UndirectedGraph graph = LoadTestGraph();
            graph.CalculateShortestPaths();

            Assert.AreEqual(4, graph.ExploredNodes.Count);
        }

        [TestCase(1, 0)]
        [TestCase(2, 1)]
        [TestCase(3, 3)]
        [TestCase(4, 6)]
        public void NodesHaveCorrectShortestPathAfterDijkstra(int nodeId, int expectedDistance) 
        {
            UndirectedGraph graph = LoadTestGraph();
            graph.CalculateShortestPaths();

            Node node = graph.ExploredNodes.FirstOrDefault(n => n.NodeId == nodeId);

            Assert.NotNull(node);

            Assert.AreEqual(expectedDistance, node.MinDistance);

        }

        [Test]
        public void PruneFrontierDoesNotPruneNodesEarly() 
        {
            UndirectedGraph graph = LoadTestGraph();

            Node node = graph.UnexploredNodes.Dequeue();
            graph.Frontier.Add(node);

            graph.PruneFrontier();

            Assert.IsEmpty(graph.ExploredNodes);
            Assert.True(graph.Frontier.Any(n => n.NodeId == 1));

        }

        /// <summary>
        /// After adding first 3 nodes to the frontier, node 1 does not have any referenced nodes that are unexplored, 
        /// so it should be pruned
        /// </summary>
        [Test]
        public void PruneFrontierPrunesNodes() 
        {
            UndirectedGraph graph = LoadTestGraph();

            for (int i = 0; i < 3; i++) 
            {
                graph.Frontier.Add(graph.UnexploredNodes.Dequeue());
            }

            graph.PruneFrontier();

            Assert.True(graph.ExploredNodes.Any(n => n.NodeId == 1));
            int[] expectedNodes = { 2, 3 };
            Assert.AreEqual(graph.Frontier.Select(n => n.NodeId).ToArray(), expectedNodes);
        }

        private UndirectedGraph LoadTestGraph()
        {
            UndirectedGraph graph = new UndirectedGraph();

            Node node = new Node(1);
            node.ReferencedNodes.Add(new ReferencedNode(2, 1));
            node.ReferencedNodes.Add(new ReferencedNode(3, 4));

            graph.UnexploredNodes.Enqueue(node);

            node = new Node(2);
            node.ReferencedNodes.Add(new ReferencedNode(3, 2));
            node.ReferencedNodes.Add(new ReferencedNode(4, 6));

            graph.UnexploredNodes.Enqueue(node);

            node = new Node(3);
            node.ReferencedNodes.Add(new ReferencedNode(4, 3));

            graph.UnexploredNodes.Enqueue(node);

            node = new Node(4);
            graph.UnexploredNodes.Enqueue(node);

            return graph; 
        }
    }


   

}
