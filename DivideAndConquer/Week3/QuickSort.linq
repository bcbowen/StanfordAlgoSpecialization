<Query Kind="Program">
  <Namespace>System.Numerics</Namespace>
</Query>

void Main()
{
	QuickSortTester.RunTests();
	
	string path = Path.Combine(GetQueryDirectory(), "PA3Data.txt");
	
	int[] data = LoadData(path);
	int comparisons = QuickSort.Sort(data, PivotType.First);
	Console.WriteLine($"Comparisons from First: {comparisons}");
	
	data = LoadData(path);
	comparisons = QuickSort.Sort(data, PivotType.Last);
	Console.WriteLine($"Comparisons from Last: {comparisons}");

	data = LoadData(path);
	comparisons = QuickSort.Sort(data, PivotType.Median);
	Console.WriteLine($"Comparisons from Median: {comparisons}");
	
}



private int[] LoadData(string path)
{
	List<int> data = new List<int>();
	
	using (StreamReader reader = new StreamReader(path))
	{
		string line;
		while((line = reader.ReadLine()) != null) 
		{
			int value = int.Parse(line);
			data.Add(value);
		}
		reader.Close();
	}
	Console.WriteLine($"Data loaded {data.Count() } rows");
	return data.ToArray();
}

public static string GetQueryDirectory()
{
	FileInfo file = new FileInfo(Util.CurrentQueryPath);
	return file.DirectoryName;
}

public enum PivotType 
{
	First, 
	Last, 
	Median
	
}

static class QuickSortTester 
{
	public static void RunTests(bool goCrazy = false) 
	{
		List<TestData> testData = LoadTestData();
		bool allPassed; 
		foreach(TestData testCase in testData) 
		{
			if (testCase.TestValues.Length > 100000 && !goCrazy) continue;
			
			testCase.Init();
			int firstComparisons = QuickSort.Sort(testCase.TestValuesWorking, PivotType.First);
			testCase.Init();
			int lastComparisons = QuickSort.Sort(testCase.TestValuesWorking, PivotType.Last);				
			testCase.Init();
			int medianComparisons = QuickSort.Sort(testCase.TestValuesWorking, PivotType.Median);
			
			allPassed = firstComparisons == testCase.ExpectedResultFirst && 
				lastComparisons == testCase.ExpectedResultLast &&
				medianComparisons == testCase.ExpectedResultMedian;
			if (allPassed)
			{
				Console.WriteLine($"Test {testCase.CaseName} passed, dude!");
			} 
			else
			{
				StringBuilder output = new StringBuilder();
				
				output.Append($"Test {testCase.CaseName} FAILED: ");

				if (firstComparisons == testCase.ExpectedResultFirst)
				{
					output.Append($"First Comparisons match;");
				}
				else
				{
					output.Append($"First Comparisons expected {testCase.ExpectedResultFirst} got {firstComparisons};");
				}

				if (lastComparisons == testCase.ExpectedResultLast)
				{
					output.Append($"Last Comparisons match;");
				}
				else
				{
					output.Append($"Last Comparisons expected {testCase.ExpectedResultLast} got {lastComparisons};");
				}

				if (medianComparisons == testCase.ExpectedResultMedian)
				{
					output.Append($"Median Comparisons match;");
				}
				else
				{
					output.Append($"Median Comparisons expected {testCase.ExpectedResultMedian} got {medianComparisons};");
				}

				Console.WriteLine(output.ToString());
				break;
			}
		}
	}

	private static List<TestData> LoadTestData() 
	{
		List<TestData> testDataList = new List<TestData>();
		
		TestData testData;

		DirectoryInfo testCaseDirectory = new DirectoryInfo(Path.Combine(GetQueryDirectory(), "TestCases"));
		foreach(FileInfo inputFile in testCaseDirectory.GetFiles().Where(f => f.Name.StartsWith("input_"))) 
		{
			testData = new TestData(inputFile.Name.Replace("input_", ""));
			List<int> testValues = new List<int>();
			using (StreamReader reader = new StreamReader(inputFile.FullName)) 
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					testValues.Add(int.Parse(line));
				}
				reader.Close();
			}
			testData.TestValues = testValues.ToArray();

			FileInfo outputFile = new FileInfo(inputFile.FullName.Replace("input_", "output_"));
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
			
			testDataList.Add(testData);
		}

		return testDataList;
	}

}

class TestData
{
	public TestData(string caseName) 
	{
		CaseName = caseName; 
	}
	
	public string CaseName { get; set; }
	public int[] TestValues { get; set; }
	public int[] TestValuesWorking { get; set; }
	public long ExpectedResultFirst {get; set; }
	public long ExpectedResultLast {get; set; }
	public long ExpectedResultMedian {get; set; }

	public void Init()
	{
		TestValuesWorking = new int[TestValues.Length];
		for(int i = 0; i < TestValues.Length; i++) 
		{
			TestValuesWorking[i] = TestValues[i];
		}
	}
}


// You can define other methods, fields, classes and namespaces here

static class QuickSort 
{
	public static int Sort(int[] values, PivotType pivotType) 
	{
		return Sort(values, 0, values.Length - 1, pivotType);
	}
	
	private static int Sort(int[] values, int l, int r, PivotType pivotType)
	{
		if (l >= r) return 0;
		int comparisonCount = 0;
		int i = ChoosePivotIndex(values, pivotType, l, r);
		Swap(l, i, values);
		
		(int pivotPosition, int count) = Partition(values, l, r);
		comparisonCount += count; 
		comparisonCount += Sort(values, l, pivotPosition - 1, pivotType);
		comparisonCount += Sort(values, pivotPosition + 1, r, pivotType);
		
		return comparisonCount;
	}

	private static (int pivotIndex, int comparisonCount) Partition(int[] values, int l, int r)
	{
		int pivot = values[l];
		int i = l + 1;
		int j = l + 1;
		int comparisonCount = r -l;
		
		while (j <= r)
		{
			if (values[j] < pivot)
			{
				Swap(i, j, values);
				i++;
			}
			j++;
		}
		int pivotIndex = i - 1;
		Swap(l, pivotIndex, values);
		return (pivotIndex, comparisonCount);
	}

	private static void Swap(int i, int j, int[] values)
	{
		if (i != j)
		{
			int temp = values[i];
			values[i] = values[j];
			values[j] = temp;
		}
	}

	private static int ChoosePivotIndex(int[] values, PivotType pivotType, int l, int r)
	{
		switch (pivotType) 
		{
			case PivotType.First: 
				return l;
			case PivotType.Last: 
				return r;
			case PivotType.Median:
			default:
				int medianIndex = l + (r - l) / 2;
				
				List<int> a = new List<int> { values[l], values[medianIndex], values[r] };
				a.Sort(); 
				int medianValue = a[1];
				return Array.IndexOf(values, medianValue);
		}
	}

}