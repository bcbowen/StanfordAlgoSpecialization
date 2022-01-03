using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SCCTests")]
namespace Graph
{
    public class DirectedGraph
    {
        private int _nextFinishTime = 1;
        //private int _nextComponentGroup = 1;
        //private Component _currentComponent;

        public List<Component> Components { get; private set; } = new List<Component>();

        public SortedList<int, Node> Nodes { get; private set; } = new SortedList<int, Node>();

        /// <summary>
        /// Finish Time
        ///  key: Finish Time
        ///  value: Node
        /// </summary>
        public SortedList<int, Node> FinishTimes { get; private set; } = new SortedList<int, Node>();

        
        public static DirectedGraph Load(string path) 
        {
            DirectedGraph graph = new DirectedGraph();
            int maxValue = 0;

            using (StreamReader reader = new StreamReader(path)) 
            {
                string line;                
                int value, referencedValue;
                // Load Connected nodes from file (not guaranteed to be in order) 
                while ((line = reader.ReadLine()) != null) 
                {
                    string[] fields = line.Split(' ');
                    if (fields.Length >= 2)
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
            Stack<Node> nodeStack = new Stack<Node>();
            Node node; 
            foreach (int key in Nodes.Keys.OrderByDescending(k => k)) 
            {
                node = Nodes[key];
                if (node.Status != LastStep.FirstPass)
                {
                    node.Status = LastStep.FirstPass;
                    nodeStack.Push(node);
                    Node nextNode; 
                    do
                    {
                        while ((nextNode = node.NextNodes.FirstOrDefault(n => n.Status != LastStep.FirstPass)) != null) 
                        {
                            node = nextNode;
                            node.Status = LastStep.FirstPass;
                            nodeStack.Push(node);
                        }
                        FinishTimes.Add(_nextFinishTime++, node);
                        int lastValue = node.Value;
                        do
                        {
                            if (nodeStack.Peek().NextNodes.Any(n => n.Status != LastStep.FirstPass))
                            {
                                // don't remove a node from the stack if it has other edges to explore
                                node = nodeStack.Peek();
                            }
                            else
                            {
                                node = nodeStack.Pop();
                            }
                            
                        } while (node.Value == lastValue && nodeStack.Count > 0);
                        
                        //SetFinishTimes(node);
                    } while (nodeStack.Count > 0);
                    
                    // add the final node
                    FinishTimes.Add(_nextFinishTime++, node);

                }
                
            }
        }

        internal void SecondPass() 
        {
            Stack<Node> nodeStack = new Stack<Node>();
            Component currentComponent;
            Node previousNode; 
            int nextComponentGroupId = 1;
            for (int i = FinishTimes.Count - 1; i >= 0; i--) 
            {
                Node node = FinishTimes[FinishTimes.Keys[i]];

                if (node.Status != LastStep.SecondPass) 
                {
                    nodeStack.Push(node);
                    currentComponent = new Component(nextComponentGroupId++);
                    currentComponent.Leader = node;
                    currentComponent.Nodes.Add(node);
                    node.Status = LastStep.SecondPass;
                    do
                    {
                        while ((previousNode = node.PreviousNodes.FirstOrDefault(n => n.Status != LastStep.SecondPass)) != null) 
                        {
                            node = previousNode;
                            node.Status = LastStep.SecondPass;
                            currentComponent.Nodes.Add(node);
                            nodeStack.Push(node);
                        }
                       
                        int lastValue = node.Value;
                        do 
                        {
                            if (nodeStack.Peek().PreviousNodes.Any(n => n.Status != LastStep.SecondPass))
                            {
                                node = nodeStack.Peek();
                            }
                            else 
                            {
                                node = nodeStack.Pop();
                            }
                            
                        } while (node.Value == lastValue && nodeStack.Count > 0);

                    } while (nodeStack.Count > 0);

                    Components.Add(currentComponent);

                }
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
