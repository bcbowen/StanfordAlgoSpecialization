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

        [TestCase(10, 16, 18)]
        [TestCase(11, 16, 16)]
        [TestCase(12, 16, 14)]
        [TestCase(13, 16, 12)]
        [TestCase(14, 16, 10)]
        [TestCase(15, 16, 8)]
        [TestCase(16, 32, 20)]
        [TestCase(17, 32, 18)]
        [TestCase(18, 32, 16)]
        [TestCase(19, 32, 14)]
        public void BinaryClusterTestSmall(int testNumber, int count, int bits)
        {
            RunTest(testNumber, count, bits);
        }

        [TestCase(20, 32, 12)]
        [TestCase(21, 32, 10)]
        [TestCase(22, 32, 8)]
        [TestCase(23, 64, 22)]
        [TestCase(24, 64, 20)]
        [TestCase(25, 64, 18)]
        [TestCase(26, 64, 16)]
        [TestCase(27, 64, 14)]
        [TestCase(28, 64, 12)]
        [TestCase(29, 64, 10)]
        public void BinaryClusterTestMedium(int testNumber, int count, int bits)
        {
            RunTest(testNumber, count, bits);
        }

        [TestCase(30, 128, 24)]
        [TestCase(31, 128, 22)]
        [TestCase(32, 128, 20)]
        [TestCase(33, 128, 18)]
        [TestCase(34, 128, 16)]
        [TestCase(35, 128, 14)]
        [TestCase(36, 128, 12)]
        [TestCase(37, 128, 10)]
        [TestCase(38, 256, 24)]
        [TestCase(39, 256, 22)]
        [TestCase(40, 256, 20)]
        [TestCase(41, 256, 18)]
        [TestCase(42, 256, 16)]
        [TestCase(43, 256, 14)]
        [TestCase(44, 256, 12)]
        [TestCase(45, 512, 24)]
        [TestCase(46, 512, 22)]
        [TestCase(47, 512, 20)]
        [TestCase(48, 512, 18)]
        [TestCase(49, 512, 16)]
        public void BinaryClusterTestLarge(int testNumber, int count, int bits)
        {
            RunTest(testNumber, count, bits);
        }

        [TestCase(50, 512, 14)]
        [TestCase(51, 512, 12)]
        [TestCase(52, 1024, 24)]
        [TestCase(53, 1024, 22)]
        [TestCase(54, 1024, 20)]
        [TestCase(55, 1024, 18)]
        [TestCase(56, 1024, 16)]
        [TestCase(57, 1024, 14)]
        [TestCase(58, 2048, 24)]
        [TestCase(59, 2048, 22)]
        [TestCase(60, 2048, 20)]
        [TestCase(61, 2048, 18)]
        [TestCase(62, 2048, 16)]
        [TestCase(63, 4096, 24)]
        [TestCase(64, 4096, 22)]
        [TestCase(65, 4096, 20)]
        [TestCase(66, 4096, 18)]
        [TestCase(67, 4096, 16)]
        [TestCase(68, 8192, 24)]
        [TestCase(69, 8192, 22)]
        public void BinaryClusterTestXL(int testNumber, int count, int bits)
        {
            RunTest(testNumber, count, bits);
        }

        [TestCase(70, 8192, 20)]
        [TestCase(71, 8192, 18)]
        [TestCase(72, 16384, 24)]
        [TestCase(73, 16384, 22)]
        [TestCase(74, 16384, 20)]
        [TestCase(75, 16384, 18)]
        [TestCase(76, 32768, 24)]
        [TestCase(77, 32768, 22)]
        [TestCase(78, 32768, 20)]
        [TestCase(79, 65536, 24)]
        [TestCase(80, 65536, 22)]
        public void BinaryClusterTestHuge(int testNumber, int count, int bits)
        {
            RunTest(testNumber, count, bits);
        }

        [TestCase(81, 131072, 24)]
        [TestCase(82, 131072, 22)]
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
