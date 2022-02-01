using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

using Algorithms.Greedy;
using Algorithms.Shared;


namespace Algorithms.Tests.Greedy.Clustering
{
    [TestFixture]
    public class EdgeClusteringTests
    {
        [TestCase(1, 8)]
        [TestCase(2, 8)]
        [TestCase(3, 8)]
        [TestCase(4, 8)]
        public void EdgeClusteringTestTiny(int testId, int nodeCount) 
        {
            RunTest(testId, nodeCount);
        }

        [TestCase(5, 16)]
        [TestCase(6, 16)]
        [TestCase(7, 16)]
        [TestCase(8, 16)]
        [TestCase(9, 32)]
        [TestCase(10, 32)]
        [TestCase(11, 32)]
        [TestCase(12, 32)]
        public void EdgeClusteringTestSmall(int testId, int nodeCount)
        {
            RunTest(testId, nodeCount);
        }

        [TestCase(13, 64)]
        [TestCase(14, 64)]
        [TestCase(15, 64)]
        [TestCase(16, 64)]
        [TestCase(17, 128)]
        [TestCase(18, 128)]
        [TestCase(19, 128)]
        [TestCase(20, 128)]
        public void EdgeClusteringTestMedium(int testId, int nodeCount)
        {
            RunTest(testId, nodeCount);
        }

        [TestCase(21, 256)]
        [TestCase(22, 256)]
        [TestCase(23, 256)]
        [TestCase(24, 256)]
        [TestCase(25, 512)]
        [TestCase(26, 512)]
        [TestCase(27, 512)]
        [TestCase(28, 512)]
        public void EdgeClusteringTestLarge(int testId, int nodeCount)
        {
            RunTest(testId, nodeCount);
        }

        [TestCase(29, 1024)]
        [TestCase(30, 1024)]
        [TestCase(31, 1024)]
        [TestCase(32, 1024)]
        public void EdgeClusteringTestXL(int testId, int nodeCount)
        {
            RunTest(testId, nodeCount);
        }

        [Test]
        public void EdgeListLoaded() 
        {
            string fileName = $"input_completeRandom_1_8.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories("Clustering1Data").First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();

            (List<UndirectedWeightedEdge> edges, int clusterCount) = Algorithms.Greedy.Clustering.LoadEdgeCollection(file.FullName);
            Assert.AreEqual(8, clusterCount);
            Assert.AreEqual(28, edges.Count);

        }

        private void RunTest(int testNumber, int count)
        {
            string fileName = $"input_completeRandom_{testNumber}_{count}.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories("Clustering1Data").First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();
            Assert.NotNull(file, "Test file not found");

            (List<UndirectedWeightedEdge> edges, int nodeCount) = Algorithms.Greedy.Clustering.LoadEdgeCollection(file.FullName);
            const int clusterCount = 4;
            int maxDistance = Algorithms.Greedy.Clustering.RunEdgeCluster(edges, clusterCount, nodeCount);
            
            int expectedMaxDistance = GetExpectedOutput(file.FullName);

            Assert.AreEqual(expectedMaxDistance, maxDistance);
        }

        private int GetExpectedOutput(string fileName)
        {
            int maxDistance;
            string outputFileName = fileName.Replace("input_", "output_");
            using (StreamReader reader = new StreamReader(outputFileName))
            {
                string line = reader.ReadLine();
                maxDistance = int.Parse(line);
                reader.Close();
            }

            return maxDistance;
        }
    }
}
