using System;
using System.IO;

using Algorithms.Graph;
using Algorithms.Tests.Models;
using DataStructures;
using NUnit.Framework;

namespace Algorithms.Tests.Graph
{
    [TestFixture]
    public class KargerMinCutTests
    {
		[TestCase("input_random_0_4.txt")]
		[TestCase("input_random_1_6.txt")]
		[TestCase("input_random_2_6.txt")]
		[TestCase("input_random_3_6.txt")]
		[TestCase("input_random_4_6.txt")]
		[TestCase("input_random_5_10.txt")]
		[TestCase("input_random_6_10.txt")]
		[TestCase("input_random_7_10.txt")] 
		[TestCase("input_random_8_10.txt")] 
		public void MinCutTestsTiny(string fileName)
		{
			DateTime startTime;

			KargerTestData testCase = LoadTestData(fileName);
			Assert.NotNull(testCase, $"Test not found for {fileName} not found, double check that shit");

			startTime = DateTime.Now;
			Console.WriteLine($"Analyzing {testCase.CaseName} with {testCase.TestGraph.NodeCount} nodes and {testCase.TestGraph.EdgeCount} edges");
			int result = KargerMinCut.Analyze(testCase.TestGraph);

			Assert.AreEqual(result, testCase.ExpectedResult);

			Console.WriteLine($"Case ran in {DateTime.Now.Subtract(startTime).TotalSeconds} seconds.");

		}

		[TestCase("input_random_9_25.txt")]
		[TestCase("input_random_10_25.txt")]
		[TestCase("input_random_11_25.txt")]
		[TestCase("input_random_12_25.txt")]
		[TestCase("input_random_13_50.txt")]
		[TestCase("input_random_14_50.txt")]
		[TestCase("input_random_15_50.txt")]
		[TestCase("input_random_16_50.txt")]
		[TestCase("input_random_17_75.txt")]
		[TestCase("input_random_18_75.txt")]
		[TestCase("input_random_19_75.txt")]
		[TestCase("input_random_20_75.txt")]
		public void MinCutTestsSmall(string fileName)
		{
			DateTime startTime;

			KargerTestData testCase = LoadTestData(fileName);
			Assert.NotNull(testCase, $"Test not found for {fileName} not found, double check that shit");

			startTime = DateTime.Now;
			Console.WriteLine($"Analyzing {testCase.CaseName} with {testCase.TestGraph.NodeCount} nodes and {testCase.TestGraph.EdgeCount} edges");
			int result = KargerMinCut.Analyze(testCase.TestGraph);

			Assert.AreEqual(result, testCase.ExpectedResult);

			Console.WriteLine($"Case ran in {DateTime.Now.Subtract(startTime).TotalSeconds} seconds.");

		}

		[TestCase("input_random_21_100.txt")]
		[TestCase("input_random_22_100.txt")]
		[TestCase("input_random_23_100.txt")]
		[TestCase("input_random_24_100.txt")]
		[TestCase("input_random_25_125.txt")]
		[TestCase("input_random_26_125.txt")]
		[TestCase("input_random_27_125.txt")]
		[TestCase("input_random_28_125.txt")]
		public void MinCutTestsMedium(string fileName)
		{
			DateTime startTime;

			KargerTestData testCase = LoadTestData(fileName);
			Assert.NotNull(testCase, $"Test not found for {fileName} not found, double check that shit");

			startTime = DateTime.Now;
			Console.WriteLine($"Analyzing {testCase.CaseName} with {testCase.TestGraph.NodeCount} nodes and {testCase.TestGraph.EdgeCount} edges");
			int result = KargerMinCut.Analyze(testCase.TestGraph);

			Assert.AreEqual(result, testCase.ExpectedResult);

			Console.WriteLine($"Case ran in {DateTime.Now.Subtract(startTime).TotalSeconds} seconds.");

		}

		[TestCase("input_random_29_150.txt")]
		[TestCase("input_random_30_150.txt")]
		[TestCase("input_random_31_150.txt")]
		[TestCase("input_random_32_150.txt")]
		[TestCase("input_random_33_175.txt")]
		[TestCase("input_random_34_175.txt")]
		[TestCase("input_random_35_175.txt")]
		[TestCase("input_random_36_175.txt")]
		[TestCase("input_random_37_200.txt")]
		[TestCase("input_random_38_200.txt")]
		[TestCase("input_random_39_200.txt")]
		[TestCase("input_random_40_200.txt")]
		public void MinCutTestsLarge(string fileName)
		{
			DateTime startTime;

			KargerTestData testCase = LoadTestData(fileName);
			Assert.NotNull(testCase, $"Test not found for {fileName} not found, double check that shit");

			startTime = DateTime.Now;
			Console.WriteLine($"Analyzing {testCase.CaseName} with {testCase.TestGraph.NodeCount} nodes and {testCase.TestGraph.EdgeCount} edges");
			int result = KargerMinCut.Analyze(testCase.TestGraph);

			Assert.AreEqual(result, testCase.ExpectedResult);

			Console.WriteLine($"Case ran in {DateTime.Now.Subtract(startTime).TotalSeconds} seconds.");

		}

		private KargerTestData LoadTestData(string fileName) 
		{
			FileInfo testCaseFile = new FileInfo(Path.Combine(TestUtils.GetTestCaseDirectory().FullName, "KargerMinCutData", fileName));
			Assert.True(testCaseFile.Exists);


			KargerTestData testData = new KargerTestData(testCaseFile.Name.Replace("input_", ""));
			testData.TestGraph = KargerGraph.LoadGraph(testCaseFile.FullName);

			FileInfo outputFile = new FileInfo(testCaseFile.FullName.Replace("input_", "output_"));
			using (StreamReader reader = new StreamReader(outputFile.FullName))
			{
				string line = reader.ReadLine();
				testData.ExpectedResult = int.Parse(line);
				reader.Close();
			}		

			return testData;
		}
	}
}
