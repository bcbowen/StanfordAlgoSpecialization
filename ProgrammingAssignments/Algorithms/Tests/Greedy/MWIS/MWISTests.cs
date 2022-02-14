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

        [TestCase(13, 80)]
        [TestCase(14, 80)]
        [TestCase(15, 80)]
        [TestCase(16, 80)]
        [TestCase(17, 160)]
        [TestCase(18, 160)]
        [TestCase(19, 160)]
        [TestCase(20, 160)]
        public void MWISTestsMedium(int testNumber, int count)
        {
            RunTest(testNumber, count);
        }

        [TestCase(21, 320)]
        [TestCase(22, 320)]
        [TestCase(23, 320)]
        [TestCase(24, 320)]
        [TestCase(25, 640)]
        [TestCase(26, 640)]
        [TestCase(27, 640)]
        [TestCase(28, 640)]
        public void MWISTestsLarge(int testNumber, int count)
        {
            RunTest(testNumber, count);
        }

        [TestCase(29, 1000)]
        [TestCase(30, 1000)]
        [TestCase(31, 1000)]
        [TestCase(32, 1000)]
        [TestCase(33, 2000)]
        [TestCase(34, 2000)]
        [TestCase(35, 2000)]
        [TestCase(36, 2000)]
        public void MWISTestsXL(int testNumber, int count)
        {
            RunTest(testNumber, count);
        }

        [TestCase(37, 4000)]
        [TestCase(38, 4000)]
        [TestCase(39, 4000)]
        [TestCase(40, 4000)]
        [TestCase(41, 8000)]
        [TestCase(42, 8000)]
        [TestCase(43, 8000)]
        [TestCase(44, 8000)]
        public void MWISTestsHuge(int testNumber, int count)
        {
            RunTest(testNumber, count);
        }

        [TestCase(45, 10000)]
        [TestCase(46, 10000)]
        [TestCase(47, 10000)]
        [TestCase(48, 10000)]
        public void MWISTestsGinormous(int testNumber, int count)
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
