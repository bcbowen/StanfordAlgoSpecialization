using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Algorithms.Sort;

using Algorithms.Tests.Models;

using NUnit.Framework;

namespace Algorithms.Tests.Sort
{
	[TestFixture]
    class QuickSortTests
    {

		[TestCase("input_dgrcode_01_5.txt")]
		[TestCase("input_dgrcode_02_5.txt")]
		[TestCase("input_dgrcode_03_5.txt")]
		[TestCase("input_dgrcode_04_5.txt")]
		[TestCase("input_dgrcode_05_5.txt")]
		[TestCase("input_dgrcode_06_10.txt")]
		[TestCase("input_dgrcode_07_10.txt")]
		[TestCase("input_dgrcode_08_10.txt")]
		[TestCase("input_dgrcode_09_10.txt")]
		[TestCase("input_dgrcode_10_10.txt")]
		[TestCase("input_dgrcode_11_20.txt")]
		[TestCase("input_dgrcode_12_20.txt")]
		[TestCase("input_dgrcode_13_20.txt")]
		[TestCase("input_dgrcode_14_20.txt")]
		[TestCase("input_dgrcode_15_20.txt")]
		public void QuickSortTestsSmall(string fileName) 
		{
			QuickSortTestData testData = LoadTestData(fileName);

			testData.Init();
			int firstComparisons = QuickSort.Sort(testData.TestValuesWorking, PivotType.First);
			testData.Init();
			int lastComparisons = QuickSort.Sort(testData.TestValuesWorking, PivotType.Last);
			testData.Init();
			int medianComparisons = QuickSort.Sort(testData.TestValuesWorking, PivotType.Median);

			Assert.AreEqual(testData.ExpectedResultFirst, firstComparisons, $"{fileName} test failed for pivot type first");
			Assert.AreEqual(testData.ExpectedResultMedian, medianComparisons, $"{fileName} test failed for pivot type median");
			Assert.AreEqual(testData.ExpectedResultLast, lastComparisons, $"{fileName} test failed for pivot type last");			
		}

		[TestCase("input_dgrcode_16_100000.txt")]
		[TestCase("input_dgrcode_17_100000.txt")]
		[TestCase("input_dgrcode_18_100000.txt")]
		[TestCase("input_dgrcode_19_1000000.txt")]
		[TestCase("input_dgrcode_20_1000000.txt")]
		public void QuickSortTestsBig(string fileName)
		{
			QuickSortTestData testData = LoadTestData(fileName);

			testData.Init();
			int firstComparisons = QuickSort.Sort(testData.TestValuesWorking, PivotType.First);
			testData.Init();
			int lastComparisons = QuickSort.Sort(testData.TestValuesWorking, PivotType.Last);
			testData.Init();
			int medianComparisons = QuickSort.Sort(testData.TestValuesWorking, PivotType.Median);

			Assert.AreEqual(testData.ExpectedResultFirst, firstComparisons, $"{fileName} test failed for pivot type first");
			Assert.AreEqual(testData.ExpectedResultMedian, medianComparisons, $"{fileName} test failed for pivot type median");
			Assert.AreEqual(testData.ExpectedResultLast, lastComparisons, $"{fileName} test failed for pivot type last");
		}

		private QuickSortTestData LoadTestData(string fileName)
		{
			FileInfo testCaseFile = new FileInfo(Path.Combine(TestUtils.GetTestCaseDirectory().FullName, "QuickSortData", fileName));
			Assert.True(testCaseFile.Exists);

			QuickSortTestData testData = new QuickSortTestData(testCaseFile.Name.Replace("input_", ""));
			List<int> testValues = new List<int>();
			using (StreamReader reader = new StreamReader(testCaseFile.FullName))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					testValues.Add(int.Parse(line));
				}
				reader.Close();
			}
			testData.TestValues = testValues.ToArray();

			FileInfo outputFile = new FileInfo(testCaseFile.FullName.Replace("input_", "output_"));
			using (StreamReader reader = new StreamReader(outputFile.FullName))
			{
				string line = reader.ReadLine();
				testData.ExpectedResultFirst = int.Parse(line);
				line = reader.ReadLine();
				testData.ExpectedResultLast = int.Parse(line);
				line = reader.ReadLine();
				testData.ExpectedResultMedian = int.Parse(line);

				reader.Close();
			}		

			return testData;
		}
	}
}
