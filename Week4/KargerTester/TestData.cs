using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphAlgos;

namespace KargerTester
{
    class TestData
    {
		public TestData(string caseName)
		{
			CaseName = caseName;
		}

		public string CaseName { get; set; }
		public Graph TestGraph { get; set; }
		public long ExpectedResult { get; set; }
	}
}
