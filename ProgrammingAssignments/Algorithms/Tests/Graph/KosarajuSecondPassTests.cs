using System.IO;
using System.Linq;

using Algorithms.Graph.Kosaraju;
using NUnit.Framework;

namespace Algorithms.Tests.GraphTests
{
    [TestFixture]
    public class KosarajuSecondPassTests
    {
        private KosarajuGraph _graph;
        [OneTimeSetUp]
        public void Setup()
        {
            const string fileName = "input_mostlyCycles_17_128.txt";
            string path = Path.Combine(TestUtils.GetTestCaseDirectory().FullName, "KosarajuData", fileName);
            Assert.IsTrue(File.Exists(path), "Missing test file");
            _graph = KosarajuGraph.Load(path);
            Algorithm.FirstPass(_graph);
            Algorithm.SecondPass(_graph);
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
