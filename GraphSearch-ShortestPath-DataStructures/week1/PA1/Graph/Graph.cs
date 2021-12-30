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

        public List<Component> Components { get; private set; } = new List<Component>();

        public List<Node> Nodes { get; private set; } = new List<Node>();

        #region Find
        public Node Find(int value = -1, int finishTime = -1)
        {
            if (value < 0 && finishTime < 0) throw new ArgumentException("Either value or finishing time has to be passed");

            return Find(0, Nodes.Count() - 1, value, finishTime);

        }

        private Node Find(int startIndex, int endIndex, int value = -1, int finishingTime = -1) 
        {
            int searchVal = Math.Max(value, finishingTime); 

            Node result = null;
            
            Func<Node, int, int> Compare = (Node node, int i) => 
                {
                    return value > -1 ? node.Value.CompareTo(i) : node.FinishTime.CompareTo(i);
                }; 
            
            if (startIndex == endIndex) 
            {
                if (Compare(Nodes[startIndex], searchVal) == 0)
                {
                    result = Nodes[startIndex];
                }
                return result;
            }

            int diff = endIndex - startIndex;
            if (diff % 2 > 0) diff++;
            int midIndex = startIndex + (diff / 2);

            int comparison = Compare(Nodes[midIndex], searchVal);
            switch (comparison)
            {
                case 0:
                    return Nodes[midIndex];
                case 1:
                    return Find(startIndex, midIndex - 1, value, finishingTime);
                default:
                    return Find(midIndex + 1, endIndex, value, finishingTime);
            }

        }
        #endregion Find

        public static DirectedGraph Load(string path) 
        {
            DirectedGraph graph = new DirectedGraph();

            using (StreamReader reader = new StreamReader(path)) 
            {
                string line;
                int lastValue = -1;
                Node node = null;
                int value, referencedValue;
                while ((line = reader.ReadLine()) != null) 
                {
                    string[] fields = line.Split(' ');
                    if (fields.Length == 2)
                    {
                        value = int.Parse(fields[0]);
                        referencedValue = int.Parse(fields[1]);
                        if (value != lastValue)
                        {
                            node = new Node { Value = value };
                            graph.Nodes.Add(node);
                        }
                        lastValue = value;
                        node.NextNodeIds.Add(referencedValue);
                    }
                }

                reader.Close();
            }

            // set up links between nodes in graph
            foreach (Node node in graph.Nodes) 
            {
                foreach (int id in node.NextNodeIds) 
                {
                    Node referencedNode = graph.Find(value: id);
                    Debug.Assert(referencedNode != null, $"Node {id} not found in graph! This should not happen, man!");
                    referencedNode.PreviousNodes.Add(node);
                    node.NextNodes.Add(referencedNode); 
                }
            }

            return graph;
        }


        public void DoTheKosaraju() 
        {
            
        }

        /// <summary>
        /// Traverse the nodes from back to front and do DFS, mark the finishing time value for each node
        /// </summary>
        internal void FirstPass() 
        {
            for (int i = Nodes.Count - 1; i >= 0; i--) 
            {
                if (Nodes[i].Status != LastStep.FirstPass) 
                {
                    SetFinishTimes(Nodes[i]);
                }
            }
        }

        internal void SetFinishTimes(Node node) 
        {
            node.Status = LastStep.FirstPass;
            foreach (Node nextNode in node.NextNodes) 
            {
                if (nextNode.Status != LastStep.FirstPass) SetFinishTimes(nextNode);
            }
            node.FinishTime = _nextFinishTime++;
        }
    }
}
