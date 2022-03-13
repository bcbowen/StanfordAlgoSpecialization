using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Algorithms.Graph.AllPairsShortestPath;
using Algorithms.Shared;


namespace Algorithms.Tests.Graph
{
    [TestFixture]
    public class FloydWarshallTests
    {
        const string DataDirectory = "AllPairsShortestPathData"; 

        [Test]
        public void GraphLoaded() 
        {
            string fileName = "input_random_1_2.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories(DataDirectory).First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();
            Assert.NotNull(file, "Test file not found");

            DirectedGraph graph = FloydWarshall.LoadGraph(file.FullName);
            Assert.AreEqual(4, graph.Nodes.Count);
        }

        [TestCase(1, 2)]
        [TestCase(2, 2)]
        [TestCase(3, 2)]
        [TestCase(4, 2)]
        [TestCase(5, 4)]
        [TestCase(6, 4)]
        [TestCase(7, 4)]
        [TestCase(8, 4)]
        public void TinyFloydWarshallTests(int testNumber, int count) 
        {
            RunFloydWarshall(testNumber, count);
        }

        [TestCase(9, 8)]
        [TestCase(10, 8)]
        [TestCase(11, 8)]
        [TestCase(12, 8)]
        [TestCase(13, 16)]
        [TestCase(14, 16)]
        [TestCase(15, 16)]
        [TestCase(16, 16)]
        public void SmallFloydWarshallTests(int testNumber, int count)
        {
            RunFloydWarshall(testNumber, count);
        }

        /// <summary>
        /// Run the Floyd Warshall algorithm
        /// </summary>
        /// <param name="testNumber"></param>
        /// <param name="count"></param>
        private void RunFloydWarshall(int testNumber, int count)
        {
            string fileName = $"input_random_{testNumber}_{count}.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories(DataDirectory).First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();
            Assert.NotNull(file, "Test file not found");

            DirectedGraph graph = FloydWarshall.LoadGraph(file.FullName);

            int? result = FloydWarshall.CalculateShortestPath(graph);

            int? expected = GetExpectedOutput(file.FullName);

            Assert.AreEqual(expected, result);
        }

        private int? GetExpectedOutput(string fileName)
        {
            int? result;
            string outputFileName = fileName.Replace("input_", "output_");
            using (StreamReader reader = new StreamReader(outputFileName))
            {
                string line = reader.ReadLine();
                reader.Close();

                if (line == "NULL")
                {
                    result = null;
                }
                else
                {
                    result = int.Parse(line);
                }
            }

            return result;
        }

    }
}
