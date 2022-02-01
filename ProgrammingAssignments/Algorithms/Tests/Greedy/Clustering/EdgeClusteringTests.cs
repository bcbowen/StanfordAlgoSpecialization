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
        public void EdgeClusteringTestTiny(int testId, int nodeCount) 
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
