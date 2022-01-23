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
        public void TestSchedulerTiny(int testNumber, int count)
        {
            RunTest(testNumber, count);
        }

        private void RunTest(int testNumber, int count) 
        {
            string fileName = $"input_random{testNumber}_{count}.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories("GreedySchedulerData").First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();
            Assert.NotNull(file, "Test file not found");

            List<Job> jobs = Scheduler.ScheduleJobsByDifference(file.FullName);

        }
    }
}
