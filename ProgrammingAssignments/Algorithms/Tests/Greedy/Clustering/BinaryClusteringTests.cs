using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace Algorithms.Tests.Greedy.Clustering
{
    [TestFixture]
    public class BinaryClusteringTests
    {
        [Test]
        public void TestFileLoaded() 
        {
            string fileName = $"input_random_1_4_14.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories("Clustering2Data").First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();

            Dictionary<string, int> clusters = Algorithms.Greedy.Clustering.LoadBinaryCluster(file.FullName);
            Assert.AreEqual(4, clusters.Keys.Count);
        }
    }
}
