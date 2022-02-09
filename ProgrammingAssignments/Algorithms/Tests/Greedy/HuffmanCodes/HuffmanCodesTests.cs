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
        public void HuffmanCodesTiny(int testNumber, int count) 
        {
            RunTest(testNumber, count);
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

            Assert.AreEqual(expectedMin, tree.MinSize);
            Assert.AreEqual(expectedMax, tree.MaxSize);
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
