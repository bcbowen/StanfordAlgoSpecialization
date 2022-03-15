using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

using NUnit.Framework;

using Algorithms.Graph.PrimMst;
using Algorithms.Shared;



namespace Algorithms.Tests.Greedy.PrimMST
{
    [TestFixture]
    public class PrimMstTests
    {
        const string IgnoreMessage = "These take far too long to run the straightforward algorithm, try with the heap based version";
        [Test]
        public void GraphIsLoadedFromTestFile() 
        {
            string fileName = "input_random_1_10.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories("PrimMstData").First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();
            Assert.NotNull(file, "Test file not found");

            UndirectedWeightedGraph graph = UndirectedWeightedGraph.LoadGraph(file.FullName);
            Assert.AreEqual(10, graph.Nodes.Count);
            Assert.AreEqual(10, graph.Edges.Count);
        }

        /// <summary>
        /// Example from Algorithims Illuminated Part 3, pg 58
        /// </summary>
        [Test]
        public void SimplePrimTest() 
        {
            UndirectedWeightedGraph graph = new UndirectedWeightedGraph();
            graph.Nodes.Add(new Node(1, int.MaxValue));
            graph.Nodes.Add(new Node(2, int.MaxValue));
            graph.Nodes.Add(new Node(3, int.MaxValue));
            graph.Nodes.Add(new Node(4, int.MaxValue));

            graph.Edges.Add(new UndirectedWeightedEdge(1, 2, 1));
            graph.Edges.Add(new UndirectedWeightedEdge(1, 3, 4));
            graph.Edges.Add(new UndirectedWeightedEdge(1, 4, 3));
            graph.Edges.Add(new UndirectedWeightedEdge(2, 4, 2));
            graph.Edges.Add(new UndirectedWeightedEdge(3, 4, 5));

            PrimTree tree = Algorithm.RunPrimMstBasic(graph);
            Assert.NotNull(tree.Edges.FirstOrDefault(e => e.ContainsNodes(1, 3)));
            Assert.NotNull(tree.Edges.FirstOrDefault(e => e.ContainsNodes(1, 2)));
            Assert.NotNull(tree.Edges.FirstOrDefault(e => e.ContainsNodes(2, 4)));
            Assert.AreEqual(7, tree.TotalWeight);
        }

        [TestCase(1, 10)]
        [TestCase(2, 10)]
        [TestCase(3, 10)]
        [TestCase(4, 10)]
        public void NonHeapPrimTestsTiny(int testNumber, int count) 
        {
            RunBasicTest(testNumber, count);
        }

        [TestCase(5, 20)]
        [TestCase(6, 20)]
        [TestCase(7, 20)]
        [TestCase(8, 20)]
        [TestCase(9, 40)]
        [TestCase(10, 40)]
        [TestCase(11, 40)]
        [TestCase(12, 40)]
        public void NonHeapPrimTestsSmall(int testNumber, int count)
        {
            RunBasicTest(testNumber, count);
        }

        [TestCase(13, 80)]
        [TestCase(14, 80)]
        [TestCase(15, 80)]
        [TestCase(16, 80)]
        [TestCase(17, 100)]
        [TestCase(18, 100)]
        [TestCase(19, 100)]
        [TestCase(20, 100)]
        public void NonHeapPrimTestsMedium(int testNumber, int count)
        {
            RunBasicTest(testNumber, count);
        }

        [TestCase(21, 200)]
        [TestCase(22, 200)]
        [TestCase(23, 200)]
        [TestCase(24, 200)]
        [TestCase(25, 400)]
        [TestCase(26, 400)]
        [TestCase(27, 400)]
        [TestCase(28, 400)]
        [TestCase(29, 800)]
        [TestCase(30, 800)]
        [TestCase(31, 800)]
        [TestCase(32, 800)]
        [TestCase(33, 1000)]
        [TestCase(34, 1000)]
        [TestCase(35, 1000)]
        [TestCase(36, 1000)]
        public void NonHeapPrimTestsLarge(int testNumber, int count)
        {
            RunBasicTest(testNumber, count);
        }


        [Ignore(IgnoreMessage)]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testNumber"></param>
        /// <param name="count"></param>
        [TestCase(37, 2000)]
        [TestCase(38, 2000)]
        [TestCase(39, 2000)]
        [TestCase(40, 2000)]
        public void NonHeapPrimTestsHuge(int testNumber, int count)
        {
            RunBasicTest(testNumber, count);
        }

        [Ignore(IgnoreMessage)]
        [TestCase(41, 4000)]
        [TestCase(42, 4000)]
        [TestCase(43, 4000)]
        [TestCase(44, 4000)]
        [TestCase(45, 8000)]
        [TestCase(46, 8000)]
        [TestCase(47, 8000)]
        [TestCase(48, 8000)]
        [TestCase(49, 10000)]
        [TestCase(50, 10000)]
        [TestCase(51, 10000)]
        [TestCase(52, 10000)]
        [TestCase(53, 20000)]
        [TestCase(54, 20000)]
        [TestCase(55, 20000)]
        [TestCase(56, 20000)]
        [TestCase(57, 40000)]
        [TestCase(58, 40000)]
        [TestCase(59, 40000)]
        [TestCase(60, 40000)]
        [TestCase(61, 80000)]
        [TestCase(62, 80000)]
        [TestCase(63, 80000)]
        [TestCase(64, 80000)]
        [TestCase(65, 100000)]
        [TestCase(66, 100000)]
        [TestCase(67, 100000)]
        [TestCase(68, 100000)]
        public void NonHeapPrimTestsGargantuan(int testNumber, int count)
        {
            RunBasicTest(testNumber, count);
        }

        /// <summary>
        /// Run the non-heap version
        /// </summary>
        /// <param name="testNumber"></param>
        /// <param name="count"></param>
        private void RunBasicTest(int testNumber, int count)
        {
            string fileName = $"input_random_{testNumber}_{count}.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories("PrimMstData").First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();
            Assert.NotNull(file, "Test file not found");

            UndirectedWeightedGraph graph = UndirectedWeightedGraph.LoadGraph(file.FullName);
            PrimTree tree = Algorithm.RunPrimMstBasic(graph);
            long actual = tree.TotalWeight;

            long expected = GetExpectedOutput(file.FullName);
            
            Assert.AreEqual(expected, actual);
        }

        private long GetExpectedOutput(string fileName)
        {
            long result;
            string outputFileName = fileName.Replace("input_", "output_");
            using (StreamReader reader = new StreamReader(outputFileName))
            {
                string line = reader.ReadLine();
                result = long.Parse(line);
                reader.Close();
            }

            return result;
        }
    }
}
