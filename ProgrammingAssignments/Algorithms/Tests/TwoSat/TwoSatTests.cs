using System;
using System.Collections.Generic;
using System.IO;
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
        const string DataDirectory = "TwoSatData";
        const string DataFilePrefix = "input_beaunus"; 


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

        [TestCase("-1 -2;2 2", "1 false;2 true", true)]
        [TestCase("-1 -2;2 2", "1 true;2 true", false)]
        public void IsSatisfiedReturnsExpectedValue(string conditionString, string settingString, bool expectedValue) 
        {
            List<Condition> conditions = new List<Condition>();
            Dictionary<int, bool> settings = new Dictionary<int, bool>();

            string[] fields = conditionString.Split(';');
            foreach (string field in fields) 
            {
                conditions.Add(Condition.Parse(field));
            }

            fields = settingString.Split(';');
            foreach (string field in fields) 
            {
                string[] settingFields = field.Split(' ');
                settings.Add(int.Parse(settingFields[0]), bool.Parse(settingFields[1]));
            }

            (bool returnValue, List<int> unsatisfiedConditions) = SAT.TwoSat.IsSatisfied(settings, conditions); 

            Assert.AreEqual(expectedValue, returnValue);

        }

        [Test]
        public void TestDataInitializedCorrectly() 
        {
            string fileName = $"{DataFilePrefix}_4_4.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories(DataDirectory).First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();
            Assert.NotNull(file, "Test file not found");

            (Dictionary<int, bool> settings, List<Condition> conditions) = SAT.TwoSat.LoadFile(file.FullName);
            // settings has keys 1 to 4, all initialized to true
            Assert.AreEqual(4, settings.Keys.Count);
            for (int i = 1; i < 5; i++) 
            {
                Assert.True(settings[i]);
            }

            Assert.AreEqual(4, conditions.Count);
            Condition condition = conditions[0];
            Assert.AreEqual(3, condition.Value1);
            Assert.AreEqual(4, condition.Value2);
            Assert.False(condition.Is1);
            Assert.True(condition.Is2);

        }


        [TestCase(1, 2)]
        [TestCase(2, 2)]
        [TestCase(3, 4)]
        [TestCase(4, 4)]
        [TestCase(5, 8)]
        [TestCase(6, 8)]
        [TestCase(7, 10)]
        [TestCase(8, 10)]
        public void TinySATTests(int testNumber, int count)
        { 
            RunSATAnalysis(testNumber, count);
        }

        [TestCase(9, 20)]
        [TestCase(10, 20)]
        [TestCase(11, 40)]
        [TestCase(12, 40)]
        [TestCase(13, 80)]
        [TestCase(14, 80)]
        [TestCase(15, 100)]
        [TestCase(16, 100)]
        public void SmallSATTests(int testNumber, int count)
        {
            RunSATAnalysis(testNumber, count);
        }

        [TestCase(17, 200)]
        [TestCase(18, 200)]
        [TestCase(19, 400)]
        [TestCase(20, 400)]
        [TestCase(21, 800)]
        [TestCase(22, 800)]
        [TestCase(23, 1000)]
        [TestCase(24, 1000)]
        public void MediumSATTests(int testNumber, int count)
        {
            RunSATAnalysis(testNumber, count);
        }

        [TestCase(25, 2000)]
        [TestCase(26, 2000)]
        [TestCase(27, 4000)]
        [TestCase(28, 4000)]
        [TestCase(29, 8000)]
        [TestCase(30, 8000)]
        [TestCase(31, 10000)]
        [TestCase(32, 10000)]
        public void LargeSATTests(int testNumber, int count)
        {
            RunSATAnalysis(testNumber, count);
        }

        [TestCase(33, 20000)]
        [TestCase(34, 20000)]
        [TestCase(35, 40000)]
        [TestCase(36, 40000)]
        [TestCase(37, 80000)]
        [TestCase(38, 80000)]
        [TestCase(39, 100000)]
        [TestCase(40, 100000)]
        public void HugeSATTests(int testNumber, int count)
        {
            RunSATAnalysis(testNumber, count);
        }



        /// <summary>
        /// Run the Traveling Salesman Heuristic algorithm
        /// </summary>
        /// <param name="testNumber"></param>
        /// <param name="count"></param>
        private void RunSATAnalysis(int testNumber, int count)
        {
            string fileName = $"{DataFilePrefix}_{testNumber}_{count}.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories(DataDirectory).First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();
            Assert.NotNull(file, "Test file not found");

            bool result = SAT.TwoSat.IsSatisfiable(file.FullName);

            bool expected = GetExpectedOutput(file.FullName);

            Assert.AreEqual(expected, result);
        }

        [TestCase("2sat1", 6, 6)]
        [TestCase("2sat2", 57, 54)]
        [TestCase("2sat3", 295, 288)]
        [TestCase("2sat4", 11, 11)]
        [TestCase("2sat5", 101, 98)]
        [TestCase("2sat6", 26, 25)]
        public void PruneTestConditionsTest(string fileName, int expectedClauseCount, int expectedLiteralCount) 
        {
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories(DataDirectory).First();
            FileInfo file = testDirectory.GetFiles($"{fileName}.txt").FirstOrDefault();
            Assert.NotNull(file, "Test file not found");

            (Dictionary<int, bool> settings, List<Condition> conditions) = SAT.TwoSat.LoadFile(file.FullName);
            SAT.TwoSat.PruneConditions(conditions);
            List<int> literals = conditions.Select(c => c.Value1).Concat(conditions.Select(c => c.Value2)).Distinct().ToList();
            Assert.AreEqual(expectedClauseCount, conditions.Count);
            Assert.AreEqual(expectedLiteralCount, literals.Count);
        }


        private bool GetExpectedOutput(string fileName)
        {
            bool result;
            string outputFileName = fileName.Replace("input_", "output_");
            using (StreamReader reader = new StreamReader(outputFileName))
            {
                string line = reader.ReadLine();
                reader.Close();
                result = line == "1" ? true : false;
            }

            return result;
        }
    }
}
