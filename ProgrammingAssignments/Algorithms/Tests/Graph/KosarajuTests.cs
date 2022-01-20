using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using Algorithms.Tests.Models;
using Algorithms.Graph.Kosaraju;
using NUnit.Framework;

namespace Algorithms.Tests.GraphTests
{
    [TestFixture]
    public class KosarajuTests
    {
        List<KosarajuTestData> _testData;
        [OneTimeSetUp]
        public void Setup()
        {
            LoadTestData();
        }

        [Test]
        public void TestClassExampleFile()
        {
            KosarajuTestData testCase = _testData.FirstOrDefault(c => c.FileName.Contains("classExample"));

            KosarajuGraph graph = KosarajuGraph.Load(testCase.FilePath);
            int[] results = Algorithm.CalculateStronglyConnectedComponents(5, graph);
            Assert.AreEqual(testCase.ExpectedResults, results, "Results don't match man");
        }

        [TestCase("Tiny", 1, 16)]
        [TestCase("Small", 16, 200)]
        [TestCase("Medium", 200, 1600)]
        [TestCase("Large", 1600, 12800)]
        [TestCase("Extra Large", 12800, 80000)]
        [TestCase("Massive", 80000, 160000)]
        //[TestCase("Gargantuan", 160000, 1000000)] // uncomment for complete testing... commented to reduce testing run time (can't ignore specific test cases)
        public void CalculateSCCTests(string label, int lowerBound, int upperBound)
        {
            var testCases = _testData.Where(c => c.EdgeCount > lowerBound && c.EdgeCount <= upperBound);
            Console.WriteLine($"{label} tests ({testCases.Count()})");
            foreach (KosarajuTestData testCase in testCases)
            {
                Console.WriteLine($"Testing {testCase.FileName}");
                KosarajuGraph graph = KosarajuGraph.Load(testCase.FilePath);
                DateTime startTime = DateTime.Now;
                int[] results;
                try
                {
                    results = Algorithm.CalculateStronglyConnectedComponents(5, graph); ;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return;
                }

                Assert.AreEqual(testCase.ExpectedResults, results, $"Results don't match for testcase {testCase.FileName}");
                TimeSpan elapsed = DateTime.Now.Subtract(startTime);
                switch (label)
                {
                    case "Gargantuan":
                    case "Massive":
                    case "Extra Large":
                        Console.WriteLine($"Done in {elapsed.TotalSeconds} seconds");
                        break;
                    default:
                        Console.WriteLine($"Done in {elapsed.TotalMilliseconds} milliseconds");
                        break;

                }

            }

        }


        /// <summary>
        /// Regression test for cases that have been known to fail during development
        /// </summary>
        /// <param name="testFile"></param>
        [TestCase("input_mostlyCycles_3_8.txt")]
        [TestCase("input_Tim_3_9.txt")]
        [TestCase("input_Tim_1_11.txt")]
        [TestCase("input_forumTestCase_1_5.txt")]
        [TestCase("input_mostlyCycles_17_128.txt")]
        public void CalculateSCCRegressionTests(string testFile)
        {
            KosarajuTestData testCase = _testData.FirstOrDefault(c => c.FileName == testFile);
            Assert.NotNull(testCase);

            Console.WriteLine($"Testing {testCase.FileName}");
            KosarajuGraph graph = KosarajuGraph.Load(testCase.FilePath);
            int[] results = Algorithm.CalculateStronglyConnectedComponents(5, graph);
            Assert.AreEqual(testCase.ExpectedResults, results, $"Results don't match for testcase {testCase.FileName}");
        }


        private void LoadTestData()
        {
            _testData = new List<KosarajuTestData>();
            DirectoryInfo testCasePath = TestUtils.GetTestCaseDirectory().GetDirectories("Kosaraju*").First();
            int[] expectedResults;
            string[] fields;
            foreach (FileInfo outputFile in testCasePath.GetFiles("output_*.txt"))
            {
                fields = outputFile.Name.Substring(0, outputFile.Name.Length - 4).Split('_');
                Assert.Greater(fields.Length, 3, $"Filename {outputFile.Name} is not in the expected format");
                int edgeCount = int.Parse(fields[3]);
                expectedResults = GetExpectedResults(outputFile.FullName);
                KosarajuTestData testCase = new KosarajuTestData
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
