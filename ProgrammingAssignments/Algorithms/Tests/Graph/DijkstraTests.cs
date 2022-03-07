using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;

using Algorithms.Graph.Dijkstra;

namespace Algorithms.Tests.Graph
{
    [TestFixture]
    public class DijkstraTests
    {
        private const string IgnoreMessage = "Dijkstra tests are currently disabled while the files are broken... need to finish refactoring ";
        private int[] _testIds = { 7, 37, 59, 82, 99, 115, 133, 165, 188, 197 };

        [TestCase(1, 4)]
        [TestCase(2, 4)]
        [TestCase(3, 4)]
        [TestCase(4, 4)]
        public void TinyGraphTests(int testNumber, int nodeCount)
        {
            RunTests(testNumber, nodeCount);
        }

        [TestCase(5, 8)]
        [TestCase(6, 8)]
        [TestCase(7, 8)]
        [TestCase(8, 8)]
        public void SmallGraphTests(int testNumber, int nodeCount)
        {
            RunTests(testNumber, nodeCount);
        }

        [TestCase(9, 16)]
        [TestCase(10, 16)]
        [TestCase(11, 16)]
        [TestCase(12, 16)]
        public void MediumGraphTests(int testNumber, int nodeCount)
        {
            RunTests(testNumber, nodeCount);
        }

        [TestCase(13, 32)]
        [TestCase(14, 32)]
        [TestCase(15, 32)]
        [TestCase(16, 32)]
        public void LargeGraphTests(int testNumber, int nodeCount)
        {
            RunTests(testNumber, nodeCount);
        }

        [TestCase(17, 64)]
        [TestCase(18, 64)]
        [TestCase(19, 64)]
        [TestCase(20, 64)]
        public void LargerGraphTests(int testNumber, int nodeCount)
        {
            RunTests(testNumber, nodeCount);
        }

        [TestCase(21, 128)]
        [TestCase(22, 128)]
        [TestCase(23, 128)]
        [TestCase(24, 128)]
        public void HugeGraphTests(int testNumber, int nodeCount)
        {
            RunTests(testNumber, nodeCount);

        }

        //[Ignore(IgnoreMessage)]
        [TestCase(25, 256)]
        [TestCase(26, 256)]
        [TestCase(27, 256)]
        [TestCase(28, 256)]
        public void MassiveGraphTests(int testNumber, int nodeCount)
        {
            RunTests(testNumber, nodeCount);
        }

        private void RunTests(int testNumber, int nodeCount)
        {
            string fileName = $"input_random_{testNumber}_{nodeCount}.txt";
            FileInfo testFile = GetTestFile(fileName);
            Assert.True(File.Exists(testFile.FullName));

            Dictionary<int, int> paths = Algorithm.CalculateShortestPaths(testFile.FullName, 1);
            List<int> expectedResult = GetOutputs(GetOutputFileName(testFile.FullName));

            List<int> result = GetTestResults(_testIds, paths);
            Assert.AreEqual(result.Count(), expectedResult.Count);
            for (int i = 0; i < result.Count(); i++)
            {
                Assert.AreEqual(expectedResult[i], result[i], $"Mismatch on testId {_testIds[i]} expected: {expectedResult[i]} got: {result[i]}");
            }
        }

        private FileInfo GetTestFile(string path)
        {
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories("DijkstraData").First();
            FileInfo[] files = testDirectory.GetFiles(path);
            Assert.Greater(files.Length, 0);
            return files[0];
        }

        private string GetOutputFileName(string inputFileName)
        {
            return inputFileName.Replace("input_", "output_");
        }

        private List<int> GetOutputs(string outputFileName)
        {
            List<int> outputs = new List<int>();
            string line;
            using (StreamReader reader = new StreamReader(outputFileName))
            {
                line = reader.ReadLine();
                reader.Close();
            }

            string[] values = line.Split(',');
            int test;
            foreach (string value in values)
            {
                if (int.TryParse(value, out test)) outputs.Add(test);
            }
            return outputs;
        }

        private List<int> GetTestResults(int[] nodeIds, Dictionary<int, int> paths)
        {
            List<int> results = new List<int>();
            foreach (int nodeId in nodeIds)
            {
                results.Add(paths[nodeId]);
            }

            return results;
        }

        [TestCase(1, 4)]
        [TestCase(3, 4)]
        public void RunOldAlgorithm(int testNumber, int nodeCount)
        {
            string fileName = $"input_random_{testNumber}_{nodeCount}.txt";
            FileInfo testFile = GetTestFile(fileName);
            Assert.True(File.Exists(testFile.FullName));
            List<DijkstraNode> nodes = Algorithm.LoadGraph(testFile.FullName);
            Algorithm.CalculateShortestPaths_old(nodes, 1);
            List<int> expectedResult = GetOutputs(GetOutputFileName(testFile.FullName));

            List<int> results = new List<int>();
            foreach (int nodeId in _testIds)
            {
                results.Add(nodes.First(n => n.NodeId == nodeId).Value);
            }

            //List<int> result = GetTestResults(_testIds, paths);
            //Assert.AreEqual(result.Count(), expectedResult.Count);
            for (int i = 0; i < results.Count(); i++)
            {
                Assert.AreEqual(expectedResult[i], results[i], $"Mismatch on testId {_testIds[i]} expected: {expectedResult[i]} got: {results[i]}");
            }
        }
    }
}
