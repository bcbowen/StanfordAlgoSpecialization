using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

using NUnit.Framework;

using Algorithms.Graph;
using Algorithms.Shared;



namespace Algorithms.Tests.Greedy.PrimMST
{
    [TestFixture]
    public class PrimMstTests
    {
        [Test]
        public void GraphIsLoadedFromTestFile() 
        {
            string fileName = "input_random_1_10.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories("PrimMstData").First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();
            Assert.NotNull(file, "Test file not found");

            UndirectedWeightedGraph graph = UndirectedWeightedGraph.LoadGraph(file.FullName);
            Assert.AreEqual(10, graph.Nodes.Count);
            Assert.AreEqual(10, graph.Edges.Count);
        }
    }
}
