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
        [TestCase("input_random_1_4.txt")]
        [TestCase("input_random_2_4.txt")]
        [TestCase("input_random_3_4.txt")]
        [TestCase("input_random_4_4.txt")]
        public void SmallGraphTests(string fileName) 
        {
            FileInfo testFile = GetTestFile(fileName);
            Assert.True(File.Exists(testFile.FullName));

            UndirectedGraph graph = UndirectedGraph.LoadGraph(testFile.FullName);
            graph.CalculateShortestPaths();
            List<int> expectedResult = GetOutputs(GetOutputFileName(testFile.FullName));

            var result = graph.ExploredNodes.OrderBy(n => n.NodeId).ToList();
            Assert.AreEqual(result.Count(), expectedResult.Count);
            for (int i = 0; i < result.Count(); i++) 
            {
                Assert.AreEqual(expectedResult[i], result[i].MinDistance);
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

        private List<int> GetTestResults(DirectedGraph graph) 
        {

            List<int> results = new List<int>();
        }
    }
}
