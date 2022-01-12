namespace Algorithms.Tests.Models
{
	class QuickSortTestData
	{
		public QuickSortTestData(string caseName)
		{
			CaseName = caseName;
		}

		public string CaseName { get; set; }
		public int[] TestValues { get; set; }
		
		/// <summary>
		/// Since we sort for each pivot type, we store the data in it's original order so we can compare results
		/// </summary>
		public int[] TestValuesWorking { get; set; }
		
		public long ExpectedResultFirst { get; set; }
		public long ExpectedResultLast { get; set; }
		public long ExpectedResultMedian { get; set; }

		
		public void Init()
		{
			TestValuesWorking = new int[TestValues.Length];
			for (int i = 0; i < TestValues.Length; i++)
			{
				TestValuesWorking[i] = TestValues[i];
			}
		}
		
	}

}
