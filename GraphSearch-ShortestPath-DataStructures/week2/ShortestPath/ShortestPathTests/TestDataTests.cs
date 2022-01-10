using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;
using Graph;
using Graph.DataStructures;

namespace ShortestPathTests
{
    [TestFixture]
    public class TestDataTests
    {
        private int[] _testIds = { 7, 37, 59, 82, 99, 115, 133, 165, 188, 197 };

        [TestCase("input_random_1_4.txt")]
        [TestCase("input_random_2_4.txt")]
        [TestCase("input_random_3_4.txt")]
        [TestCase("input_random_4_4.txt")]
        public void TinyGraphTests(string fileName) 
        {
            FileInfo testFile = GetTestFile(fileName);
            Assert.True(File.Exists(testFile.FullName));

            UndirectedGraph graph = UndirectedGraph.LoadGraph(testFile.FullName);
            graph.CalculateShortestPaths();
            List<int> expectedResult = GetOutputs(GetOutputFileName(testFile.FullName));

            List<int> result = GetTestResults(_testIds, graph);
            Assert.AreEqual(result.Count(), expectedResult.Count);
            for (int i = 0; i < result.Count(); i++) 
            {
                Assert.AreEqual(expectedResult[i], result[i]);
            }

        }

        [TestCase("input_random_5_8.txt")]
        [TestCase("input_random_6_8.txt")]
        [TestCase("input_random_7_8.txt")]
        [TestCase("input_random_8_8.txt")]
        public void SmallGraphTests(string fileName)
        {
            FileInfo testFile = GetTestFile(fileName);
            Assert.True(File.Exists(testFile.FullName));

            UndirectedGraph graph = UndirectedGraph.LoadGraph(testFile.FullName);
            graph.CalculateShortestPaths();
            List<int> expectedResult = GetOutputs(GetOutputFileName(testFile.FullName));

            List<int> result = GetTestResults(_testIds, graph);
            Assert.AreEqual(result.Count(), expectedResult.Count);
            for (int i = 0; i < result.Count(); i++)
            {
                Assert.AreEqual(expectedResult[i], result[i]);
            }

        }

        [TestCase("input_random_9_16.txt")]
        [TestCase("input_random_10_16.txt")]
        [TestCase("input_random_11_16.txt")]
        [TestCase("input_random_12_16.txt")]
        public void MediumGraphTests(string fileName)
        {
            FileInfo testFile = GetTestFile(fileName);
            Assert.True(File.Exists(testFile.FullName));

            UndirectedGraph graph = UndirectedGraph.LoadGraph(testFile.FullName);
            graph.CalculateShortestPaths();
            List<int> expectedResult = GetOutputs(GetOutputFileName(testFile.FullName));

            List<int> result = GetTestResults(_testIds, graph);
            Assert.AreEqual(result.Count(), expectedResult.Count);
            for (int i = 0; i < result.Count(); i++)
            {
                Assert.AreEqual(expectedResult[i], result[i]);
            }

        }

        [TestCase("input_random_13_32.txt")]
        [TestCase("input_random_14_32.txt")]
        [TestCase("input_random_15_32.txt")]
        [TestCase("input_random_16_32.txt")]
        public void LargeGraphTests(string fileName)
        {
            FileInfo testFile = GetTestFile(fileName);
            Assert.True(File.Exists(testFile.FullName));

            UndirectedGraph graph = UndirectedGraph.LoadGraph(testFile.FullName);
            graph.CalculateShortestPaths();
            List<int> expectedResult = GetOutputs(GetOutputFileName(testFile.FullName));

            List<int> result = GetTestResults(_testIds, graph);
            Assert.AreEqual(result.Count(), expectedResult.Count);
            for (int i = 0; i < result.Count(); i++)
            {
                Assert.AreEqual(expectedResult[i], result[i]);
            }

        }

        [TestCase("input_random_17_64.txt")]
        [TestCase("input_random_18_64.txt")]
        [TestCase("input_random_19_64.txt")]
        [TestCase("input_random_20_64.txt")]
        public void LargerGraphTests(string fileName)
        {
            FileInfo testFile = GetTestFile(fileName);
            Assert.True(File.Exists(testFile.FullName));

            UndirectedGraph graph = UndirectedGraph.LoadGraph(testFile.FullName);
            graph.CalculateShortestPaths();
            List<int> expectedResult = GetOutputs(GetOutputFileName(testFile.FullName));

            List<int> result = GetTestResults(_testIds, graph);
            Assert.AreEqual(result.Count(), expectedResult.Count);
            for (int i = 0; i < result.Count(); i++)
            {
                Assert.AreEqual(expectedResult[i], result[i]);
            }

        }

        [TestCase("input_random_21_128.txt")]
        [TestCase("input_random_22_128.txt")]
        [TestCase("input_random_23_128.txt")]
        [TestCase("input_random_24_128.txt")]
        public void HugeGraphTests(string fileName)
        {
            FileInfo testFile = GetTestFile(fileName);
            Assert.True(File.Exists(testFile.FullName));

            UndirectedGraph graph = UndirectedGraph.LoadGraph(testFile.FullName);
            graph.CalculateShortestPaths();
            List<int> expectedResult = GetOutputs(GetOutputFileName(testFile.FullName));

            List<int> result = GetTestResults(_testIds, graph);
            Assert.AreEqual(result.Count(), expectedResult.Count);
            for (int i = 0; i < result.Count(); i++)
            {
                Assert.AreEqual(expectedResult[i], result[i]);
            }

        }

        [TestCase("input_random_25_256.txt")]
        [TestCase("input_random_26_256.txt")]
        [TestCase("input_random_27_256.txt")]
        [TestCase("input_random_28_256.txt")]
        public void MassiveGraphTests(string fileName)
        {
            FileInfo testFile = GetTestFile(fileName);
            Assert.True(File.Exists(testFile.FullName));

            UndirectedGraph graph = UndirectedGraph.LoadGraph(testFile.FullName);
            graph.CalculateShortestPaths();
            List<int> expectedResult = GetOutputs(GetOutputFileName(testFile.FullName));

            List<int> result = GetTestResults(_testIds, graph);
            Assert.AreEqual(result.Count(), expectedResult.Count);
            for (int i = 0; i < result.Count(); i++)
            {
                Assert.AreEqual(expectedResult[i], result[i]);
            }

        }

        private FileInfo GetTestFile(string path) 
        {
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory();
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

        private List<int> GetTestResults(int[] nodeIds, UndirectedGraph graph) 
        {
            List<int> results = new List<int>();
            foreach (int id in nodeIds) 
            {
                results.Add(graph.ExploredNodes.First(n => n.NodeId == id).MinDistance);
            }

            return results;
        }
    }
}
