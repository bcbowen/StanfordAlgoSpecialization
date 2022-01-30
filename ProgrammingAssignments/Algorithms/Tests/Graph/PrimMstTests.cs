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
