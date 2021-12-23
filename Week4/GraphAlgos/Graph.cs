﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphAlgos
{
	public class Graph : ICloneable
	{
		private Random _random = new Random();
		private List<Edge> _edges = new List<Edge>();
		//public List<Edge> Edges { get; private set; }
		//Dictionary <string, List<string>> _nodes = new Dictionary<string, System.Collections.Generic.List<string>>();
		List<Node> _nodes = new List<Node>();

		public int EdgeCount
		{
			get
			{
				return _edges.Count;
			}
		}

		public int NodeCount
		{
			get
			{
				return _nodes.Count;
			}
		}

		public void AddEdge(int node1, int node2)
		{
			_edges.Add(new Edge(node1, node2));
		}

		public void AddEdge(Edge edge)
		{
			AddEdge(edge.Node1, edge.Node2);
		}

		public void AddEdges(List<Edge> edges)
		{

			foreach (Edge edge in edges)
			{
				if (!_edges.Any(e => e.Node1 == edge.Node1 && e.Node2 == edge.Node2) && 
					!_edges.Any(e => e.Node1 == edge.Node2 && e.Node2 == edge.Node1))
					AddEdge(edge);
			}
		}

		public void AddNodes(string line)
		{
			char delimiter = line.Contains('\t') ? '\t' : ' ';
			string[] fields = line.Split(delimiter);
			string key = fields[0];
			int value;
			Node node;
			if (int.TryParse(key, out value))
			{
				//_nodes.Add(key, new List<string>());
				node = new Node { Value = value };
				for (int i = 1; i < fields.Length; i++)
				{
					if (int.TryParse(fields[i], out value))
						//_nodes[key].Add(fields[i]);
						node.LinkedNodes.Add(value);
				}
				_nodes.Add(node);
			}

		}

		public void TrimRandomEdge()
		{
			int index = _random.Next(0, _edges.Count - 1);
			Edge edge = _edges[index];
			//Console.WriteLine($"Trimming edge {edge} Edge count: {NodeCount} Node Count: {EdgeCount}");
			// edges terminating at node1 of this edge will now terminate at node2
			// node1 will be removed

			//string merged = $"{edge.Node1}_{edge.Node2}";

			foreach (Edge e in _edges.Where(e => e != edge))
			{
				if (e.Node1 == edge.Node1)
				{
					e.Node1 = edge.Node2;
				}
				if (e.Node2 == edge.Node1) 
				{
					e.Node2 = edge.Node2;
				}
			}

			_edges.Remove(edge); 

			// remove self references
			_edges.RemoveAll(e => e.Node1 == e.Node2);

			RerouteNodes(edge.Node1, edge.Node2);
		}

		// remove node 1, update references to point to node2
		private void RerouteNodes(int node1, int node2)
		{
			foreach (Node node in _nodes.Where(n => n.Value != node1))
			{
				int i1 = node.LinkedNodes.IndexOf(node1);
				int i2 = node.LinkedNodes.IndexOf(node2);

				if (i1 > -1) 
				{
					if (i2 > -1)
					{
						// i1 and i2 in the list, leave i2 and remove i1
						node.LinkedNodes.RemoveAt(i1);
					}
					else
					{
						// node1 is in the list but not node2, replace node1 with node2
						node.LinkedNodes[i1] = node2;
					}
				} 
			}

			Node n = _nodes.FirstOrDefault(n => n.Value == node1);

			if(n != null) _nodes.Remove(n);

		}

		
		public object Clone() 
		{
			Graph clone = new Graph();
			foreach (Edge edge in _edges) 
			{
				clone.AddEdge(edge.Clone() as Edge);
			}
			foreach (Node node in _nodes)
			{
				clone._nodes.Add(node.Clone() as Node);
			}

			return clone;
		}
		
		public static Graph LoadGraph(string path)
		{
			Graph graph = new Graph();
			using (StreamReader reader = new StreamReader(path))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					graph.AddEdges(Edge.ParseEdges(line));
					graph.AddNodes(line);
				}

				reader.Close();
			}

			return graph;
		}

		//List<Vector> Vectors { get; set; }
	}
}
