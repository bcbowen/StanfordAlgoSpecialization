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
        [TestCase(2, 4, 4)]
        [TestCase(3, 4, 4)]
        [TestCase(4, 4, 4)]
        [TestCase(5, 10, 10)]
        [TestCase(6, 10, 10)]
        [TestCase(7, 10, 10)]
        [TestCase(8, 10, 10)]
        public void KnapsackDataTestsTiny(int testNumber, int capacity, int count)
        {
            RunTest(testNumber, capacity, count);
        }

        [TestCase(9, 100, 10)]
        [TestCase(10, 100, 10)]
        [TestCase(11, 100, 10)]
        [TestCase(12, 100, 10)]
        [TestCase(13, 100, 100)]
        [TestCase(14, 100, 100)]
        [TestCase(15, 100, 100)]
        [TestCase(16, 100, 100)]
        [TestCase(17, 100, 1000)]
        [TestCase(18, 100, 1000)]
        [TestCase(19, 100, 1000)]
        [TestCase(20, 100, 1000)]

        public void KnapsackDataTestsSmall(int testNumber, int capacity, int count)
        {
            RunTest(testNumber, capacity, count);
        }

        [TestCase(21, 1000, 100)]
        [TestCase(22, 1000, 100)]
        [TestCase(23, 1000, 100)]
        [TestCase(24, 1000, 100)]
        [TestCase(25, 1000, 1000)]
        [TestCase(26, 1000, 1000)]
        [TestCase(27, 1000, 1000)]
        [TestCase(28, 1000, 1000)]
        [TestCase(29, 10000, 1000)]
        [TestCase(30, 10000, 1000)]
        [TestCase(31, 10000, 1000)]
        [TestCase(32, 10000, 1000)]

        public void KnapsackDataTestsMedium(int testNumber, int capacity, int count)
        {
            RunTest(testNumber, capacity, count);
        }

        [TestCase(33, 100000, 2000)]
        [TestCase(34, 100000, 2000)]
        [TestCase(35, 100000, 2000)]
        [TestCase(36, 100000, 2000)]
        [TestCase(37, 1000000, 2000)]
        [TestCase(38, 1000000, 2000)]
        [TestCase(39, 1000000, 2000)]
        [TestCase(40, 1000000, 2000)]
        [TestCase(41, 2000000, 2000)]
        [TestCase(42, 2000000, 2000)]
        [TestCase(43, 2000000, 2000)]
        [TestCase(44, 2000000, 2000)]

        public void KnapsackDataTestsLarge(int testNumber, int capacity, int count)
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
