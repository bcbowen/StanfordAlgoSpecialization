﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using System.Threading.Tasks;

namespace DataStructures.Kosaraju
{
    public class DirectedGraph
    {
        private int _nextFinishTime = 1;

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
    }
}
