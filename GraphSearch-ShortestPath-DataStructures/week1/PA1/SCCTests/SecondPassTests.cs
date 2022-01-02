using System;
using System.IO;

using Graph;
using NUnit.Framework;

namespace SCCTests
{
    [TestFixture]
    public class SecondPassTests
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
            _graph.SecondPass();
        }

        [Test]
        public void ComponentsCollectionHasExpectedNumberOfComponents() 
        {
            const int expectedComponentCount = 3;
            Assert.AreEqual(_graph.Components.Count, expectedComponentCount);
        }



    }
}
