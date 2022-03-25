using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using Algorithms.SAT;

namespace Algorithms.Tests.TwoSat
{
    [TestFixture]
    public class TwoSatTests
    {
        [TestCase("1 2", 1, true, 2, true)]
        [TestCase("-1 2", 1, false, 2, true)]
        [TestCase("1 -2", 1, true, 2, false)]
        [TestCase("-1 -2", 1, false, 2, false)]
        public void ConditionParsingTests(string line, int expectedValue1, bool expectedIs1, int expectedValue2, bool expectedIs2) 
        { 
            Condition condition = Condition.Parse(line);
            Assert.AreEqual(expectedValue1, condition.Value1);
            Assert.AreEqual(expectedValue2, condition.Value2);
            Assert.AreEqual(expectedIs1, condition.Is1);
            Assert.AreEqual(expectedIs2, condition.Is2);
        }

    }
}
