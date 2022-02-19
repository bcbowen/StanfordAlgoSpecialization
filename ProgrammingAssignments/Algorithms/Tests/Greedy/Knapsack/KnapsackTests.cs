using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using Algorithms.Shared;
using Algorithms.Greedy;

namespace Algorithms.Tests.Greedy.Knapsack
{
    [TestFixture]
    public class KnapsackTests
    {
        const string DataFileName = "KnapsackData";

        [Test]
        public void TestNodesLoadedFromFile()
        {
            string fileName = $"input_random_17_100_1000.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories(DataFileName).First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();

            (List<Node> nodes, int capacity) = Algorithms.Greedy.Knapsack.LoadKnapsackData(file.FullName);
            Assert.AreEqual(1000, nodes.Count);
            Assert.AreEqual(100, capacity);
        }

    }
}
