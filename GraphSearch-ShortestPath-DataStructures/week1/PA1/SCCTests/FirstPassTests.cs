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
    public class FirstPassTests
    {
        private DirectedGraph _graph;
        [OneTimeSetUp]
        public void Setup()
        {
            const string fileName = "input_ClassExample_1_11.txt";
            string path = Path.Combine(TestUtils.GetTestCaseDirectory().FullName, fileName);
            Assert.IsTrue(File.Exists(path), "Missing test file");
            _graph = DirectedGraph.Load(path);
            _graph.FirstPass();
        }

        [Test]
        public void StatusSetAfterFirstPass() 
        {
            Assert.True(_graph.Nodes.All(n => n.Status == LastStep.FirstPass));
        }

        /// <summary>
        /// Ensure that the times are set for the expected nodes
        /// </summary>
        /// <param name="node">Node id (value in the sorted array)</param>
        /// <param name="expectedTime">Time (key in the sorted array)</param>
        [TestCase(1, 7)]
        [TestCase(2, 3)]
        [TestCase(3, 1)]
        [TestCase(4, 8)]
        [TestCase(5, 2)]
        [TestCase(6, 5)]
        [TestCase(7, 9)]
        [TestCase(8, 4)]
        [TestCase(9, 6)]
        public void FinishTimesSetToExpectedValues(int expectedNode, int time)
        {
            Node node = _graph.FinishTimes[time];
            Assert.AreEqual(expectedNode, node.Value, $"Node {node} does not have the expected f time of {time}");
        }


    }
}
