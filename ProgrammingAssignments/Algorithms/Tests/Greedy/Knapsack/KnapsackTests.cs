using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using Algorithms.Shared;
using Algorithms.Greedy;

namespace Algorithms.Tests.Greedy.Knapsack
{
    [TestFixture]
    public class KnapsackTests
    {
        const string DataFileName = "KnapsackData";

        [Test]
        public void TestNodesLoadedFromFile()
        {
            string fileName = $"input_random_17_100_1000.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories(DataFileName).First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();

            (List<Node> nodes, int capacity) = Algorithms.Greedy.Knapsack.LoadKnapsackData(file.FullName);
            Assert.AreEqual(1000, nodes.Count);
            Assert.AreEqual(100, capacity);
        }

        /// <summary>
        /// Example from Algorithms Illuminated 3 page 128 (16.5.5) 
        /// </summary>
        [Test]
        public void TestExampleFromBook() 
        {
            string fileName = $"BookExample.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories(DataFileName).First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();

            long result = Algorithms.Greedy.Knapsack.RunAlgorithm(file.FullName);
            long expectedValue = 8;
            Assert.AreEqual(expectedValue, result);
        }

        [TestCase(1, 4, 4)]
        public void KnapsackDataTestsTiny(int testNumber, int capacity, int count)
        {
            RunTest(testNumber, capacity, count);
        }


        private void RunTest(int testNumber, int capacity, int count)
        {
            string fileName = $"input_random_{testNumber}_{capacity}_{count}.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories(DataFileName).First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();
            Assert.NotNull(file, "Test file not found");

            int result = Algorithms.Greedy.Knapsack.RunAlgorithm(file.FullName);
            int expectedResult = GetExpectedOutput(file.FullName);

            Assert.AreEqual(expectedResult, result);
        }

        private int GetExpectedOutput(string fileName)
        {
            string outputFileName = fileName.Replace("input_", "output_");
            int result;
            using (StreamReader reader = new StreamReader(outputFileName))
            {
                string line = reader.ReadLine();
                result = int.Parse(line);
                reader.Close();
            }

            return result;
        }

    }
}
