using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


using NUnit.Framework;

using Algorithms.Greedy.HuffmanCodes;

namespace Algorithms.Tests.Greedy.HuffmanCodes
{
    [TestFixture]
    public class HuffmanCodesTests
    {
        [Test] 
        public void TestFileLoaded() 
        {
            string fileName = $"input_random_1_10.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories("HuffmanCodeData").First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();

            MinHeap forest = Algorithm.LoadCodeWeights(file.FullName);
            Assert.AreEqual(10, forest.Count);
        }

        [TestCase(1, 10)]
        [TestCase(2, 10)]
        [TestCase(3, 10)]
        [TestCase(4, 10)]
        [TestCase(5, 20)]
        [TestCase(6, 20)]
        [TestCase(7, 20)]
        [TestCase(8, 20)]
        public void HuffmanCodesTiny(int testNumber, int count) 
        {
            RunTest(testNumber, count);
        }

        [TestCase(9, 40)]
        [TestCase(10, 40)]
        [TestCase(11, 40)]
        [TestCase(12, 40)]
        [TestCase(13, 80)]
        [TestCase(14, 80)]
        [TestCase(15, 80)]
        [TestCase(16, 80)]
        public void HuffmanCodesSmall(int testNumber, int count)
        {
            RunTest(testNumber, count);
        }

        [TestCase(17, 160)]
        [TestCase(18, 160)]
        [TestCase(19, 160)]
        [TestCase(20, 160)]
        [TestCase(21, 320)]
        [TestCase(22, 320)]
        [TestCase(23, 320)]
        [TestCase(24, 320)]
        public void HuffmanCodesMedium(int testNumber, int count)
        {
            RunTest(testNumber, count);
        }

        [TestCase(25, 640)]
        [TestCase(26, 640)]
        [TestCase(27, 640)]
        [TestCase(28, 640)]
        [TestCase(29, 1000)]
        [TestCase(30, 1000)]
        [TestCase(31, 1000)]
        [TestCase(32, 1000)]
        public void HuffmanCodesLarge(int testNumber, int count)
        {
            RunTest(testNumber, count);
        }

        [TestCase(33, 2000)]
        [TestCase(34, 2000)]
        [TestCase(35, 2000)]
        [TestCase(36, 2000)]
        [TestCase(37, 4000)]
        [TestCase(38, 4000)]
        [TestCase(39, 4000)]
        [TestCase(40, 4000)]
        public void HuffmanCodesXL(int testNumber, int count)
        {
            RunTest(testNumber, count);
        }

        [TestCase(41, 8000)]
        [TestCase(42, 8000)]
        [TestCase(43, 8000)]
        [TestCase(44, 8000)]
        [TestCase(45, 10000)]
        [TestCase(46, 10000)]
        [TestCase(47, 10000)]
        [TestCase(48, 10000)]
        public void HuffmanCodesHuge(int testNumber, int count)
        {
            RunTest(testNumber, count);
        }

        [Test]
        public void CheckFinal() 
        {
            string fileName = $"input_final_49_5.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories("HuffmanCodeData").First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();
            Assert.NotNull(file, "Test file not found");

            MinHeap forest = Algorithm.LoadCodeWeights(file.FullName);
            HuffmanTree tree = Algorithm.BuildTree(forest);
            (int expectedMax, int expectedMin) = GetExpectedOutput(file.FullName);

            Assert.AreEqual(expectedMin, tree.MinSize, "Min size is off, man");
            Assert.AreEqual(expectedMax, tree.MaxSize, "Max size is off, dude");
        }

        private void RunTest(int testNumber, int count)
        {
            string fileName = $"input_random_{testNumber}_{count}.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories("HuffmanCodeData").First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();
            Assert.NotNull(file, "Test file not found");

            MinHeap forest = Algorithm.LoadCodeWeights(file.FullName);
            HuffmanTree tree = Algorithm.BuildTree(forest);
            (int expectedMax, int expectedMin) = GetExpectedOutput(file.FullName);

            Assert.AreEqual(expectedMin, tree.MinSize, "Min size is off, man");
            Assert.AreEqual(expectedMax, tree.MaxSize, "Max size is off, dude");
        }

        private (int, int) GetExpectedOutput(string fileName)
        {
            string outputFileName = fileName.Replace("input_", "output_");
            int maxSize, minSize;
            using (StreamReader reader = new StreamReader(outputFileName))
            {
                string line = reader.ReadLine();
                maxSize = int.Parse(line);
                line = reader.ReadLine();
                minSize = int.Parse(line);
                reader.Close();
            }

            return (maxSize, minSize);
        }
    }
}
