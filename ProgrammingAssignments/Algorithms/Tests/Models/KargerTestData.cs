using Algorithms.Graph.KargerMinCut;

namespace Algorithms.Tests.Models
{
	public class KargerTestData
	{
		public KargerTestData(string caseName)
		{
			CaseName = caseName;
		}

		public string CaseName { get; set; }
		public KargerGraph TestGraph { get; set; }
		public long ExpectedResult { get; set; }
	}
}
