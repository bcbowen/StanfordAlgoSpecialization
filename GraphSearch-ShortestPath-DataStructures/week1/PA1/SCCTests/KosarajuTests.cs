using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using NUnit.Framework;

using Graph;

namespace SCCTests
{
    public class Tests
    {
        List<TestData> _testData;
        [OneTimeSetUp]
        public void Setup()
        {
            LoadTestData();
        }

        [Test]
        public void TestClassExampleFile() 
        {
            TestData testCase = _testData.FirstOrDefault(c => c.FileName.Contains("classExample"));

            DirectedGraph graph = DirectedGraph.Load(testCase.FilePath);
            int[] results = graph.DoTheKosaraju(5);
            Assert.AreEqual(testCase.ExpectedResults, results, "Results don't match man");
        }

        [Test]
        public void SmallKosarajuTests()
        {
            var testCases = _testData.Where(c => c.EdgeCount <= 16);

            foreach (TestData testCase in testCases) 
            {
                Console.WriteLine($"Testing {testCase.FileName}");
                DirectedGraph graph = DirectedGraph.Load(testCase.FilePath);
                int[] results = graph.DoTheKosaraju(5);
                Assert.AreEqual(testCase.ExpectedResults, results, $"Results don't match for testcase {testCase.FileName}");
            }
            
        }

        /// <summary>
        /// Regression test for cases that have been known to fail during development
        /// </summary>
        /// <param name="testFile"></param>
        [TestCase("input_mostlyCycles_3_8.txt")]
        [TestCase("input_Tim_3_9.txt")]
        public void SmallKosarajuTests(string testFile)
        {
            TestData testCase = _testData.FirstOrDefault(c => c.FileName == testFile);
            Assert.NotNull(testCase);

            Console.WriteLine($"Testing {testCase.FileName}");
            DirectedGraph graph = DirectedGraph.Load(testCase.FilePath);
            int[] results = graph.DoTheKosaraju(5);
            Assert.AreEqual(testCase.ExpectedResults, results, $"Results don't match for testcase {testCase.FileName}");
        }

        private void LoadTestData() 
        {
            _testData = new List<TestData>();
            DirectoryInfo testCasePath = TestUtils.GetTestCaseDirectory();
            int[] expectedResults;
            string[] fields;
            foreach (FileInfo outputFile in testCasePath.GetFiles("output_*.txt")) 
            {
                fields = outputFile.Name.Substring(0, outputFile.Name.Length - 4).Split('_');
                Assert.Greater(fields.Length, 3, $"Filename {outputFile.Name} is not in the expected format");
                int edgeCount = int.Parse(fields[3]);
                expectedResults = GetExpectedResults(outputFile.FullName); 
                TestData testCase = new TestData
                {
                    FileName = outputFile.Name.Replace("output_", "input_"),
                    FilePath = outputFile.FullName.Replace("output_", "input_"),
                    EdgeCount = edgeCount,
                    ExpectedResults = expectedResults
                };
                _testData.Add(testCase);
                
            }
        }

        private int[] GetExpectedResults(string path) 
        {
            int[] expectedResults = new int[5];

            string line;             
            using (StreamReader reader = new StreamReader(path)) 
            {
                line = reader.ReadLine();
                reader.Close();
            }

            string[] values = line.Split(',');
            Debug.Assert(values.Length == 5, "Output not in expected format");

            for (int i = 0; i < 5; i++) 
            {
                expectedResults[i] = int.Parse(values[i]);
            }


            return expectedResults;
        }
    }
}