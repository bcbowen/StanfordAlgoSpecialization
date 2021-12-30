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
            const string fileName = "input_Tim_2_14.txt";
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

        public void FinishTimesSetCorrectlyOnFirstPass() 
        {
        
        }


    }
}
