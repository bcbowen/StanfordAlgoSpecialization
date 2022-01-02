using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SCCTests")]
namespace Graph
{
    public class DirectedGraph
    {
        private int _nextFinishTime = 1;
        private int _nextComponentGroup = 1;
        private Component _currentComponent;

        public List<Component> Components { get; private set; } = new List<Component>();

        public SortedList<int, Node> Nodes { get; private set; } = new SortedList<int, Node>();

        /// <summary>
        /// Finish Time
        ///  key: Finish Time
        ///  value: Node
        /// </summary>
        public SortedList<int, Node> FinishTimes { get; private set; } = new SortedList<int, Node>();

        /*
        #region Find
        public Node Find(int value)
        {
            return Find(0, Nodes.Count() - 1, value);

        }

        private Node Find(int startIndex, int endIndex, int value)
        {
            Node result = null;
                       
            if (startIndex == endIndex) 
            {
                if (Nodes[startIndex].Value == value)
                {
                    result = Nodes[startIndex];
                }
                return result;
            }

            int diff = endIndex - startIndex;
            if (diff % 2 > 0) diff++;
            int midIndex = startIndex + (diff / 2);

            if (Nodes[midIndex].Value == value)
            {
                return Nodes[midIndex];
            } else if (Nodes[midIndex].Value > value)
            {
                return Find(startIndex, midIndex - 1, value);
            }
            else
            {
                return Find(midIndex + 1, endIndex, value);
            }
        }
        #endregion Find
        */

        public static DirectedGraph Load(string path) 
        {
            DirectedGraph graph = new DirectedGraph();
            int maxValue = 0;

            using (StreamReader reader = new StreamReader(path)) 
            {
                string line;                
                //int lastValue = -1;
                //int nextValue = 1;
                //Node node = null;
                int value, referencedValue;
                // Load Connected nodes from file (not guaranteed to be in order) 
                while ((line = reader.ReadLine()) != null) 
                {
                    string[] fields = line.Split(' ');
                    if (fields.Length == 2)
                    {
                        value = int.Parse(fields[0]);
                        if (value > maxValue) maxValue = value;
                        referencedValue = int.Parse(fields[1]);

                        Node node;
                        if (!graph.Nodes.ContainsKey(value))
                        {
                            node = new Node { Value = value };
                            graph.Nodes.Add(value, node);
                        }
                        else
                        {
                            node = graph.Nodes[value];
                        }
                        node.NextNodeIds.Add(referencedValue);                        
                    }
                }
                
                reader.Close();
            }

            // Add disconnected nodes by finding ones missing from file
            var range = Enumerable.Range(1, maxValue);
            var missingValues = range.Except(graph.Nodes.Keys);
            foreach (int value in missingValues)
            {
                graph.Nodes.Add(value, new Node { Value = value });
            }

            // set up links between nodes in graph
            foreach (int key in graph.Nodes.Keys) 
            {
                Node node = graph.Nodes[key];
                foreach (int id in node.NextNodeIds) 
                {
                    Node referencedNode = graph.Nodes[id];
                    Debug.Assert(referencedNode != null, $"Node {id} not found in graph! This should not happen, man!");
                    referencedNode.PreviousNodes.Add(node);
                    node.NextNodes.Add(referencedNode); 
                }
            }

            return graph;
        }

        /// <summary>
        /// Return the n biggest components (node count) in desc order
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public int[] DoTheKosaraju(int count) 
        {
            FirstPass();
            SecondPass();
            int[] result = GetSCCCounts(count);
            return result;
        }

        /// <summary>
        /// Traverse the nodes from back to front and do DFS, mark the finishing time value for each node
        /// </summary>
        internal void FirstPass() 
        {
            foreach (int key in Nodes.Keys.OrderByDescending(k => k)) 
            {
                if (Nodes[key].Status != LastStep.FirstPass) 
                {
                    SetFinishTimes(Nodes[key]);
                }
            }
        }

        internal void SetFinishTimes(Node node) 
        {
            node.Status = LastStep.FirstPass;
            foreach (Node nextNode in node.NextNodes.Where(n => n.Status != LastStep.FirstPass)) 
            {
                SetFinishTimes(nextNode);
            }
            FinishTimes.Add(_nextFinishTime++, node);
        }

        internal void SecondPass() 
        {
            for (int i = FinishTimes.Count - 1; i >= 0; i--) 
            {
                Node node = FinishTimes[FinishTimes.Keys[i]];
                if (node.Status != LastStep.SecondPass) 
                {
                    _currentComponent = new Component(_nextComponentGroup++);
                    _currentComponent.Leader = node;
                    
                    CrawlComponents(node);
                    Components.Add(_currentComponent);
                }
            }


        }

        internal void CrawlComponents(Node node) 
        {
            _currentComponent.Nodes.Add(node);
            node.Status = LastStep.SecondPass;
            foreach (Node sibling in node.PreviousNodes.Where(n => n.Status != LastStep.SecondPass)) 
            {
                CrawlComponents(sibling);
            }
        }

        public int[] GetSCCCounts(int count) 
        {
            int[] result = new[] { 0, 0, 0, 0, 0};
            var componentCounts = (from component in Components select new { Count = component.Nodes.Count }).OrderByDescending(c => c.Count).Take(count).ToArray();
            for (int i = 0; i < componentCounts.Length; i++) 
            {
                result[i] = componentCounts[i].Count;
            }

            return result;
        }
    }
}
