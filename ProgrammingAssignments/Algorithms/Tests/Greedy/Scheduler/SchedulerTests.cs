using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Threading.Tasks;

using NUnit.Framework;

using Algorithms.Greedy;
using Algorithms.Shared;

namespace Algorithms.Tests.Greedy.SchedulerTests
{
    [TestFixture]
    public class SchedulerTests
    {
        /// <summary>
        /// Jobs should be sorted in decreasing order of weight - length; 
        /// Jobs with equal difference should be sorted with higher weight first
        /// </summary>
        [Test]
        public void TestDifferenceSortComparer() 
        {
            List<Job> jobs = new List<Job>();
            jobs.Add(new Job(4, 2)); // 2 - second
            jobs.Add(new Job(3, 1)); // 2 - third
            jobs.Add(new Job(5, 1)); // 4 - first
            jobs.Add(new Job(2, 1)); // 1 - fourth

            jobs.Sort(Algorithms.Greedy.Scheduler.CompareJobsByDifference);

            Assert.AreEqual(jobs[0].Weight, 5);
            Assert.AreEqual(jobs[1].Weight, 4);
            Assert.AreEqual(jobs[2].Weight, 3);
            Assert.AreEqual(jobs[3].Weight, 2);
        }


        [TestCase(1, 10)]
        [TestCase(2, 10)]
        [TestCase(3, 10)]
        [TestCase(4, 10)]
        [TestCase(5, 20)]
        [TestCase(6, 20)]
        [TestCase(7, 20)]
        [TestCase(8, 20)]
        public void TestSchedulerTiny(int testNumber, int count)
        {
            RunTest(testNumber, count);
        }

        [TestCase(9, 40)]
        [TestCase(10, 40)]
        [TestCase(11, 40)]
        [TestCase(12, 40)]
        [TestCase(13, 80)]
        [TestCase(14, 80)]
        [TestCase(15, 80)]
        [TestCase(16, 80)]
        public void TestSchedulerSmall(int testNumber, int count)
        {
            RunTest(testNumber, count);
        }

        [TestCase(17, 160)]
        [TestCase(18, 160)]
        [TestCase(19, 160)]
        [TestCase(20, 160)]
        [TestCase(21, 320)]
        [TestCase(22, 320)]
        [TestCase(23, 320)]
        [TestCase(24, 320)]
        public void TestSchedulerMedium(int testNumber, int count)
        {
            RunTest(testNumber, count);
        }

        [TestCase(25, 640)]
        [TestCase(26, 640)]
        [TestCase(27, 640)]
        [TestCase(28, 640)]
        [TestCase(29, 1280)]
        [TestCase(30, 1280)]
        [TestCase(31, 1280)]
        [TestCase(32, 1280)]
        public void TestSchedulerLarge(int testNumber, int count)
        {
            RunTest(testNumber, count);
        }

        [TestCase(33, 2560)]
        [TestCase(34, 2560)]
        [TestCase(35, 2560)]
        [TestCase(36, 2560)]
        [TestCase(37, 5120)]
        [TestCase(38, 5120)]
        [TestCase(39, 5120)]
        [TestCase(40, 5120)]
        public void TestSchedulerXL(int testNumber, int count)
        {
            RunTest(testNumber, count);
        }

        [TestCase(41, 10000)]
        [TestCase(42, 10000)]
        [TestCase(43, 10000)]
        [TestCase(44, 10000)]
        public void TestSchedulerHuge(int testNumber, int count)
        {
            RunTest(testNumber, count);
        }

        private void RunTest(int testNumber, int count) 
        {
            string fileName = $"input_random_{testNumber}_{count}.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories("GreedySchedulerData").First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();
            Assert.NotNull(file, "Test file not found");

            List<Job> jobs = Scheduler.ScheduleJobsByDifference(file.FullName);
            long jobTotalByDifference = jobs.Sum(j => j.WeightedCompletionTime);

            jobs = Scheduler.ScheduleJobsByRatio(file.FullName);
            long jobTotalByRatio = jobs.Sum(j => j.WeightedCompletionTime);

            long expectedTotalByDifference; 
            long expectedTotalByRatio;
            (expectedTotalByDifference, expectedTotalByRatio) = GetExpectedOutputs(file.FullName);

            Assert.AreEqual(expectedTotalByDifference, jobTotalByDifference);
            Assert.AreEqual(expectedTotalByRatio, jobTotalByRatio);
        }

        private (long, long) GetExpectedOutputs(string fileName) 
        {
            long time1, time2;
            string outputFileName = fileName.Replace("input_", "output_");
            using (StreamReader reader = new StreamReader(outputFileName)) 
            {
                string line = reader.ReadLine();
                time1 = long.Parse(line);
                line = reader.ReadLine();
                time2 = long.Parse(line);
                reader.Close();
            }

            return (time1, time2);
        }
    }
}
