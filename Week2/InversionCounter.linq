<Query Kind="Program">
  <Namespace>System.Numerics</Namespace>
</Query>

void Main()
{
	InversionCountTester.RunTests();
	string path = Path.Combine(GetQueryDirectory(), "PA2Data.txt");
	int[] data = LoadData(path);
	long result = InversionCounter.CountInversions(data);
	Console.WriteLine($"Recursive Result: {result}"); 
	result = InversionCounter.CountInversionsBrute(data);
	Console.WriteLine($"Brute Result: {result}"); 
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

private string GetQueryDirectory()
{
	FileInfo file = new FileInfo(Util.CurrentQueryPath);
	return file.DirectoryName;
}

static class InversionCountTester 
{
	public static void RunTests() 
	{
		List<InversionTestData> testData = LoadTestData();
		foreach(InversionTestData testCase in testData) 
		{
			long recursiveResult = InversionCounter.CountInversions(testCase.TestValues);
			long bruteResult = InversionCounter.CountInversionsBrute(testCase.TestValues);
			if (recursiveResult == testCase.ExpectedResult)
			{
				Console.WriteLine($"Test {testCase.CaseName} PASSED. Recursive result: {recursiveResult} brute result: {bruteResult}");
			} 
			else
			{
				Console.WriteLine($"Test {testCase.CaseName} FAILED. Expected {testCase.ExpectedResult}; Recursive result: {recursiveResult} brute result: {bruteResult}");
			}
		}
	}

	private static List<InversionTestData> LoadTestData() 
	{
		List<InversionTestData> testData = new List<InversionTestData>();
		testData.Add(new InversionTestData("TEST CASE - 1", new int[] { 1, 3, 5, 2, 4, 6 }, 3));
		testData.Add(new InversionTestData("TEST CASE - 2", new int[] { 1,5,3,2,4 }, 4));
		testData.Add(new InversionTestData("TEST CASE - 3", new int[] {5,4,3,2,1}, 10));
		testData.Add(new InversionTestData("TEST CASE - 4", new int[] {1,6,3,2,4,5}, 5));
		testData.Add(new InversionTestData("15 numbers", new int[] {9, 12, 3, 1, 6, 8, 2, 5, 14, 13, 11, 7, 10, 4, 0}, 56));
		testData.Add(new InversionTestData("50 numbers", new int[] {37, 7, 2, 14, 35, 47, 10, 24, 44, 17, 34, 11, 16, 48, 1, 39, 6, 33, 43, 26, 40, 4, 28, 5, 38, 41, 42, 12, 13, 21, 29, 18, 3, 19, 0, 32, 46, 27, 31, 25, 15, 36, 20, 8, 9, 49, 22, 23, 30, 45}, 590));
		testData.Add(new InversionTestData("100 numbers", new int[] {4, 80, 70, 23, 9, 60, 68, 27, 66, 78, 12, 40, 52, 53, 44, 8, 49, 28, 18, 46, 21, 39, 51, 7, 87, 99, 69, 62, 84, 6, 79, 67, 14, 98, 83, 0, 96, 5, 82, 10, 26, 48, 3, 2, 15, 92, 11, 55, 63, 97, 43, 45, 81, 42, 95, 20, 25, 74, 24, 72, 91, 35, 86, 19, 75, 58, 71, 47, 76, 59, 64, 93, 17, 50, 56, 94, 90, 89, 32, 37, 34, 65, 1, 73, 41, 36, 57, 77, 30, 22, 13, 29, 38, 16, 88, 61, 31, 85, 33, 54}, 2372));

		return testData;
	}

}

class InversionTestData
{
	public InversionTestData(string caseName, int[] values, long expected) 
	{
		CaseName = caseName; 
		TestValues = values;
		ExpectedResult = expected ;
	}
	
	public string CaseName { get; set; }
	public int[] TestValues { get; set; }
	public long ExpectedResult {get; set; }
	
}


// You can define other methods, fields, classes and namespaces here

static class InversionCounter 
{

	public static long CountInversionsBrute(int[] values) 
	{
		long count = 0;
		for (int i = 0; i < values.Length - 1; i++)
		{
			for (int j = i + 1; j < values.Length; j++) 
			{
				if (values[i] > values[j]) count++;
			}
		}
		
		return count;
	}

	public static long CountInversions(int[] values)
	{
		(int[] sortedValues, long inversionCount) = SortAndCount(values);
		return inversionCount;
	}

	private static (int[] sortedValues, long inversionCount) SortAndCount(int[] values)
	{
		long inversionCount = 0;
		if (values.Length < 2) 
		{
			return (values, inversionCount);
		}	
		else 
		{
			int n = values.Length / 2; 
			long inversions = 0;
			int[] leftValues, rightValues, sortedValues;
			(leftValues, inversions) = SortAndCount(values.Take(n).ToArray());
			inversionCount += inversions;
			(rightValues, inversions) = SortAndCount(values.Skip(n).Take(values.Length -n).ToArray());
			inversionCount += inversions;
			
			(sortedValues, inversions) = MergeAndCount(leftValues, rightValues);
			inversionCount += inversions;
			
			return (sortedValues, inversionCount);
		}
	}

	private static (int[] mergedValues, long splitCount) MergeAndCount(int[] leftValues, int[] rightValues) 
	{
		int[] mergedValues = new int[leftValues.Length + rightValues.Length];

		long i = 0, j = 0, k = 0, splitCount = 0;
		try 
		{
			while (k < mergedValues.Length)
			{
				if (leftValues[i] < rightValues[j])
				{ 
					mergedValues[k] = leftValues[i];
 					i++;
					k++;
				}
				else if (j <= rightValues.Length - 1)
				{
					mergedValues[k] = rightValues[j]; 
					j++; 
					k++;
					splitCount += leftValues.Length - i;
				}

				if (i == leftValues.Length)
				{
					// if we've reached the end of leftArray, copy remaining items from right
					while (j < rightValues.Length) 
					{
						mergedValues[k++] = rightValues[j++];
					}
				}
				else if (j == rightValues.Length)
				{
					// if we've reached the end of rightArray, copy remaining items from left
					while (i < leftValues.Length)
					{
						mergedValues[k++] = leftValues[i++];
					}
				}
			}
		}
		catch(IndexOutOfRangeException ex) 
		{
			ex.Dump();
		}
		
		
		return (mergedValues, splitCount);
	}

}