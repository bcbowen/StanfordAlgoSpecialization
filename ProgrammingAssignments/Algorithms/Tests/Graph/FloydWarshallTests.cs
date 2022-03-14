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
            Assert.AreEqual(2, graph.NodeCount);
            Assert.AreEqual(4, graph.EdgeCount);
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

        [TestCase(17, 32)]
        [TestCase(18, 32)]
        [TestCase(19, 32)]
        [TestCase(20, 32)]
        [TestCase(21, 64)]
        [TestCase(22, 64)]
        [TestCase(23, 64)]
        [TestCase(24, 64)]
        public void MediumFloydWarshallTests(int testNumber, int count)
        {
            RunFloydWarshall(testNumber, count);
        }

        [TestCase(25, 128)]
        [TestCase(26, 128)]
        [TestCase(27, 128)]
        [TestCase(28, 128)]
        [TestCase(29, 256)]
        [TestCase(30, 256)]
        [TestCase(31, 256)]
        [TestCase(32, 256)]
        public void LargeFloydWarshallTests(int testNumber, int count)
        {
            RunFloydWarshall(testNumber, count);
        }

        [TestCase(33, 512)]
        [TestCase(34, 512)]
        [TestCase(35, 512)]
        [TestCase(36, 512)]
        [TestCase(37, 1024)]
        [TestCase(38, 1024)]
        [TestCase(39, 1024)]
        [TestCase(40, 1024)]
        public void XLFloydWarshallTests(int testNumber, int count)
        {
            RunFloydWarshall(testNumber, count);
        }

        [TestCase(41, 2048)]
        [TestCase(42, 2048)]
        [TestCase(43, 2048)]
        [TestCase(44, 2048)]
        public void GargantuanFloydWarshallTests(int testNumber, int count)
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

            long? result = FloydWarshall.CalculateShortestPath(graph);

            long? expected = GetExpectedOutput(file.FullName);

            Assert.AreEqual(expected, result);
        }

        private long? GetExpectedOutput(string fileName)
        {
            long? result;
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
                    result = long.Parse(line);
                }
            }

            return result;
        }

    }
}
