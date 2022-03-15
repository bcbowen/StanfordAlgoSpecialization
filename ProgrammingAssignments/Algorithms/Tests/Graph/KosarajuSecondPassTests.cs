using System.IO;
using System.Linq;

using Algorithms.Graph.Kosaraju;
using NUnit.Framework;

namespace Algorithms.Tests.GraphTests
{
    [TestFixture]
    public class KosarajuSecondPassTests
    {
        const string IgnoreMessage = "Need to fix broken algo... see note in first pass"; 

        private KosarajuGraph _graph;
        [OneTimeSetUp]
        public void Setup()
        {
            const string fileName = "input_mostlyCycles_17_128.txt";
            string path = Path.Combine(TestUtils.GetTestCaseDirectory().FullName, "KosarajuData", fileName);
            Assert.IsTrue(File.Exists(path), "Missing test file");
            _graph = KosarajuGraph.Load(path);
            // TODO: Fix First Pass
            //Algorithm.FirstPass(_graph);
            //Algorithm.SecondPass(_graph);
        }

        [Ignore(IgnoreMessage)]
        [Test]
        public void ComponentsCollectionHasExpectedNumberOfComponents()
        {
            const int expectedComponentCount = 2;
            Assert.AreEqual(_graph.Components.Count, expectedComponentCount);
        }

        [Ignore(IgnoreMessage)]
        [Test]
        public void AllNodesSetToSecondPassStatus()
        {
            Assert.True(_graph.Nodes.Values.All(n => n.Status == LastStep.SecondPass));
        }
    }
}
