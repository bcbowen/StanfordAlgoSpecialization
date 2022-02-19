using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using Algorithms.Tree.OptimizedBinaryTree;

namespace Algorithms.Tests.Tree
{
    [TestFixture]
    public class OptimizedBinaryTreeTests
    {
        const string DataFileName = "OptimalSearchTreeData";

        [Test]
        public void TestNodesLoadedFromFile() 
        {
            string fileName = $"data_input_1_3.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories(DataFileName).First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();

            List<OptimizedTreeNode> nodes = Algorithm.LoadNodes(file.FullName);
            Assert.AreEqual(3, nodes.Count);
        }

        [TestCase(1, 3)]
        [TestCase(2, 7)]
        [TestCase(3, 7)]
        public void RunDataTests(int testNumber, int count)
        {
            RunTest(testNumber, count);
        }

        private void RunTest(int testNumber, int count)
        {
            string fileName = $"data_input_{testNumber}_{count}.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories(DataFileName).First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();
            Assert.NotNull(file, "Test file not found");

            List<OptimizedTreeNode> nodes = Algorithm.LoadNodes(file.FullName);
            decimal optimalWeight = Algorithm.CalculateOptimalSearchTree(nodes);
            decimal expectedWeight = GetExpectedOutput(file.FullName);

            Assert.AreEqual(expectedWeight, optimalWeight);
        }

        private decimal GetExpectedOutput(string fileName)
        {
            string outputFileName = fileName.Replace("input_", "output_");
            decimal weight;
            using (StreamReader reader = new StreamReader(outputFileName))
            {
                string line = reader.ReadLine();
                weight = decimal.Parse(line);
                reader.Close();
            }

            return weight;
        }
    }
}
