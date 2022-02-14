using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using Algorithms.Greedy;

namespace Algorithms.Tests.Greedy.MWIS
{
    [TestFixture]
    public class MWISTests
    {

        [Test]
        public void TestFileLoaded()
        {
            string fileName = $"input_random_1_10.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories("MWISData").First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();

            int[] nodes = Algorithms.Greedy.MWIS.LoadNodes(file.FullName);
            Assert.AreEqual(10, nodes.Length);
        }

        [TestCase(1, 10)]
        [TestCase(2, 10)]
        [TestCase(3, 10)]
        [TestCase(4, 10)]
        public void MWISTestsTiny(int testNumber, int count) 
        {
            RunTest(testNumber, count);
        }

        [TestCase(5, 20)]
        [TestCase(6, 20)]
        [TestCase(7, 20)]
        [TestCase(8, 20)]
        [TestCase(9, 40)]
        [TestCase(10, 40)]
        [TestCase(11, 40)]
        [TestCase(12, 40)]
        public void MWISTestsSmall(int testNumber, int count)
        {
            RunTest(testNumber, count);
        }

        private void RunTest(int testNumber, int count)
        {
            string fileName = $"input_random_{testNumber}_{count}.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories("MWISData").First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();
            Assert.NotNull(file, "Test file not found");

            string result = Algorithms.Greedy.MWIS.RunAlgorithm(file.FullName);
            string expectedOutput = GetExpectedOutput(file.FullName);

            Assert.AreEqual(expectedOutput, result);
        }

        private string GetExpectedOutput(string fileName)
        {
            string outputFileName = fileName.Replace("input_", "output_");
            string output; 
            using (StreamReader reader = new StreamReader(outputFileName))
            {
                output = reader.ReadLine();
                reader.Close();
            }

            return output;
        }
    }
}
