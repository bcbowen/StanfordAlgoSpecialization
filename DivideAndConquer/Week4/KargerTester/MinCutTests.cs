using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

using GraphAlgos;

namespace KargerTester
{
    public class Tests
    {
		private List<TestData> testDataList;
		private const int maxNodes = 4;
		private DirectoryInfo GetTestCaseDirectory() 
		{
			DirectoryInfo currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
			return new DirectoryInfo(Path.Combine(currentDirectory.Parent.Parent.Parent.FullName, "TestCases")); 
		}
		[OneTimeSetUp]
        public void Setup()
        {
			testDataList = new List<TestData>();

			TestData testData;

			DirectoryInfo testCaseDirectory = GetTestCaseDirectory();
			Assert.IsTrue(testCaseDirectory.Exists, $"TestCase directory not found at expected location {testCaseDirectory.FullName}");
			//foreach (FileInfo inputFile in testCaseDirectory.GetFiles().Where(f => f.Name.Contains("_40_200")))
			foreach(FileInfo inputFile in testCaseDirectory.GetFiles().Where(f => f.Name.StartsWith("input_"))) 
			{
				testData = new TestData(inputFile.Name.Replace("input_", ""));
				testData.TestGraph = Graph.LoadGraph(inputFile.FullName);

				FileInfo outputFile = new FileInfo(inputFile.FullName.Replace("input_", "output_"));
				using (StreamReader reader = new StreamReader(outputFile.FullName))
				{
					string line = reader.ReadLine();
					testData.ExpectedResult = int.Parse(line);
					reader.Close();
				}

				testDataList.Add(testData);
			}

			
		}

        [TestCase(0, 4)]
		[TestCase(1, 6)]
		[TestCase(2, 6)]
		[TestCase(3, 6)]
		[TestCase(4, 6)]
		[TestCase(5, 10)]
		[TestCase(6, 10)]
		[TestCase(7, 10)]
		[TestCase(8, 10)]
		[TestCase(9, 25)]
		[TestCase(10, 25)]
		[TestCase(11, 25)]
		[TestCase(12, 25)]
		[TestCase(13, 50)]
		[TestCase(14, 50)]
		[TestCase(15, 50)]
		[TestCase(16, 50)]
		[TestCase(17, 75)]
		[TestCase(18, 75)]
		[TestCase(19, 75)]
		[TestCase(20, 75)]
		[TestCase(21, 100)]
		[TestCase(22, 100)]
		[TestCase(23, 100)]
		[TestCase(24, 100)]
		[TestCase(25, 125)]
		[TestCase(26, 125)]
		[TestCase(27, 125)]
		[TestCase(28, 125)]
		[TestCase(29, 150)]
		[TestCase(30, 150)]
		[TestCase(31, 150)]
		[TestCase(32, 150)]
		[TestCase(33, 175)]
		[TestCase(34, 175)]
		[TestCase(35, 175)]
		[TestCase(36, 175)]
		[TestCase(37, 200)]
		[TestCase(38, 200)]
		[TestCase(39, 200)]
		[TestCase(40, 200)]
		public void MinCutTests(int caseNumber, int nodeCount)
        {
			DateTime startTime;
			string name = $"random_{caseNumber}_{nodeCount}.txt";
			TestData testCase = testDataList.FirstOrDefault(c => c.CaseName == name);
			Assert.NotNull(testCase, $"Test case {name} not found, double check that shit");
			
			startTime = DateTime.Now;
			Console.WriteLine($"Analyzing {testCase.CaseName} with {testCase.TestGraph.NodeCount} nodes and {testCase.TestGraph.EdgeCount} edges");
			int result = Karger.Analyze(testCase.TestGraph);

			Assert.AreEqual(result, testCase.ExpectedResult);

			Console.WriteLine($"Case ran in {DateTime.Now.Subtract(startTime).TotalSeconds} seconds");
			
		}
    }
}