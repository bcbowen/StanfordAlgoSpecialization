using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Graph;

using NUnit.Framework;

namespace SCCTests
{
    public class LoadGraphTests
    {
        private DirectedGraph _graph;
        [OneTimeSetUp]
        public void Setup() 
        {
            const string fileName = "input_Tim_2_14.txt";
            string path = Path.Combine(TestUtils.GetTestCaseDirectory().FullName, fileName);
            Assert.IsTrue(File.Exists(path), "Missing test file");
            _graph = DirectedGraph.Load(path);
        }

        [Test]
        /// <summary>
        /// If an input file is missing nodes it means the missing nodes are not connected to anything, but they still exist (they are a component of 1) 
        /// </summary>
        public void LoadedGraphLoadsUnconnectedNodes() 
        {
            // 3_8 is missing node 6
            const string fileName = "input_mostlyCycles_3_8.txt";
            string path = Path.Combine(TestUtils.GetTestCaseDirectory().FullName, fileName);
            Assert.IsTrue(File.Exists(path), "Missing test file");
            DirectedGraph graph = DirectedGraph.Load(path);
            Assert.True(graph.Nodes.Values.Any(n => n.Value == 6));
        }

        [Test]
        public void LoadedGraphLoadsConnectedNodes()
        {
            Assert.AreEqual(8, _graph.Nodes.Count);
        }

        [TestCase(1, new []{ 2 })]
        [TestCase(2, new[] { 6, 3, 4 })]
        [TestCase(3, new[] { 1, 4 })]
        [TestCase(4, new[] { 5 })]
        [TestCase(5, new[] { 4 })]
        [TestCase(6, new[] { 5, 7 })]
        [TestCase(7, new[] { 6, 8 })]
        [TestCase(8, new[] { 5, 7 })]
        public void LoadedGraphHasNextNodesSet(int value, int[] nextNodes)
        {
            Node node = _graph.Nodes.Values.FirstOrDefault(n => n.Value == value);
            Assert.NotNull(node, "Node not found in collection!");
            Assert.AreEqual(nextNodes.Length, node.NextNodes.Count);
            foreach (int id in nextNodes) 
            {
                Assert.True(node.NextNodes.Count(n => n.Value == id) == 1, $"Node {id} not found in collection"); 
            }
        }

        [TestCase(1, new[] { 3 })]
        [TestCase(2, new[] { 1 })]
        [TestCase(3, new[] { 2 })]
        [TestCase(4, new[] { 2, 3, 5 })]
        [TestCase(5, new[] { 4, 6, 8 })]
        [TestCase(6, new[] { 2, 7 })]
        [TestCase(7, new[] { 6, 8 })]
        [TestCase(8, new[] { 7 })]
        public void LoadedGraphHasPreviousNodesSet(int value, int[] previousNodes)
        {
            Node node = _graph.Nodes.Values.FirstOrDefault(n => n.Value == value);
            Assert.NotNull(node, "Node not found in collection!");
            Assert.AreEqual(previousNodes.Length, node.PreviousNodes.Count);
            foreach (int id in previousNodes)
            {
                Assert.True(node.PreviousNodes.Count(n => n.Value == id) == 1, $"Node {id} not found in collection");
            }
        }
    }
}
