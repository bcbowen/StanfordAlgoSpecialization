using System;
using System.IO;
using System.Linq;

using Algorithms.Graph;
using Algorithms.Tests.Models;
using DataStructures.Kosaraju;
using NUnit.Framework;

namespace Algorithms.Tests.Graph
{
    [TestFixture]
    public class KosarajuSecondPassTests
    {
        private DirectedGraph _graph;
        [OneTimeSetUp]
        public void Setup()
        {
            const string fileName = "input_mostlyCycles_17_128.txt";
            string path = Path.Combine(TestUtils.GetTestCaseDirectory().FullName, "KosarajuData", fileName);
            Assert.IsTrue(File.Exists(path), "Missing test file");
            _graph = DirectedGraph.Load(path);
            Kosaraju.FirstPass(_graph);
            Kosaraju.SecondPass(_graph);
        }

        [Test]
        public void ComponentsCollectionHasExpectedNumberOfComponents()
        {
            const int expectedComponentCount = 2;
            Assert.AreEqual(_graph.Components.Count, expectedComponentCount);
        }

        [Test]
        public void AllNodesSetToSecondPassStatus()
        {
            Assert.True(_graph.Nodes.Values.All(n => n.Status == LastStep.SecondPass));
        }
    }
}
