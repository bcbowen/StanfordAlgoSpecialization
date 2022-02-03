using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace Algorithms.Tests.Greedy.Clustering
{
    [TestFixture]
    public class BinaryClusteringTests
    {
        [Test]
        public void TestFileLoaded() 
        {
            string fileName = $"input_random_1_4_14.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories("Clustering2Data").First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();

            Dictionary<string, int> clusters = Algorithms.Greedy.Clustering.LoadBinaryCluster(file.FullName);
            Assert.AreEqual(4, clusters.Keys.Count);
        }

        [TestCase(1, 4, 14)]
        [TestCase(2, 4, 10)]
        [TestCase(3, 4, 8)]
        [TestCase(4, 4, 6)]
        [TestCase(5, 4, 4)]
        [TestCase(6, 8, 12)]
        [TestCase(7, 8, 10)]
        [TestCase(8, 8, 8)]
        [TestCase(9, 8, 6)]
        public void BinaryClusterTestTiny(int testNumber, int count, int bits) 
        {
            RunTest(testNumber, count, bits);   
        }

        [TestCase(83, 262144, 24)]
        public void BinaryClusterTestGinormous(int testNumber, int count, int bits)
        {
            RunTest(testNumber, count, bits);
        }

        private void RunTest(int testNumber, int count, int bits)
        {
            string fileName = $"input_random_{testNumber}_{count}_{bits}.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories("Clustering2Data").First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();
            Assert.NotNull(file, "Test file not found");

            Dictionary<string, int> clusters = Algorithms.Greedy.Clustering.LoadBinaryCluster(file.FullName);
            
            int kValue = Algorithms.Greedy.Clustering.RunBinaryCluster(clusters);

            int expectedKValue = GetExpectedOutput(file.FullName);

            Assert.AreEqual(expectedKValue, kValue);
        }

        private int GetExpectedOutput(string fileName)
        {
            int kValue;
            string outputFileName = fileName.Replace("input_", "output_");
            using (StreamReader reader = new StreamReader(outputFileName))
            {
                string line = reader.ReadLine();
                kValue = int.Parse(line);
                reader.Close();
            }

            return kValue;
        }
    }
}
