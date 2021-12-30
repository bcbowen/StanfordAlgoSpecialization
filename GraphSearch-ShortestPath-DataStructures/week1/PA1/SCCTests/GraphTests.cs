using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using Graph;

namespace SCCTests
{

    public class GraphTests
    {     

        #region Find

        [TestCase(new []{1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, 2)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 9)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 1)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 10)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 5)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 6)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 2)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 8)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 1)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 9)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 4)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 5)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 6)]
        public void FindExistingValueInGraph(int[] values, int value) 
        {
            DirectedGraph graph = InitGraphValues(values);
            Node node = graph.Find(value: value);

            Assert.NotNull(node);
            Assert.AreEqual(value, node.Value);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 2)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 9)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 1)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 10)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 5)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 6)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 2)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 8)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 1)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 9)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 4)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 5)]
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 6)]
        public void FindExistingFinsihTimeInGraph(int[] times, int time)
        {
            DirectedGraph graph = InitGraphFinishTimes(times);
            Node node = graph.Find(finishTime: time);

            Assert.NotNull(node);
            Assert.AreEqual(time, node.FinishTime);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 42)]
        public void FindNonExistingValueInGraphReturnsNull(int[] values, int value) 
        {
            DirectedGraph graph = InitGraphValues(values);
            Node node = graph.Find(value: value);

            Assert.IsNull(node);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 42)]
        public void FindNonExistingFinishTimeInGraphReturnsNull(int[] times, int time) 
        {
            DirectedGraph graph = InitGraphFinishTimes(times);
            Node node = graph.Find(finishTime: time);

            Assert.IsNull(node);
        }

        #endregion Find

        private DirectedGraph InitGraphValues(int[] values) 
        {
            int finishTimeCounter = 1;
            DirectedGraph graph = new DirectedGraph();
            foreach (int value in values)
            {
                graph.Nodes.Add(new Node { Value = value, FinishTime = finishTimeCounter++ });
            }

            return graph;
        }
        
        private DirectedGraph InitGraphFinishTimes(int[] times) 
        {
            int valueCounter = 1;

            DirectedGraph graph = new DirectedGraph();
            foreach (int time in times)
            {
                graph.Nodes.Add(new Node { Value = valueCounter++, FinishTime = time }); ;
            }

            return graph;
        }

        
    }
}
