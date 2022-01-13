using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
	public class Edge : ICloneable
	{
		public Edge(int node1, int node2)
		{
			Node1 = node1;
			Node2 = node2;
		}

		public int Node1 { get; set; }
		public int Node2 { get; set; }

		public static List<Edge> ParseEdges(string line)
		{
			char delimiter = line.Contains('\t') ? '\t' : ' ';
			List<Edge> edges = new List<Edge>();

			string[] fields = line.Split(delimiter);
			if (fields.Length > 0)
			{
				int node1, node2;

				for (int i = 1; i < fields.Length; i++)
				{
					int test;
					if (int.TryParse(fields[0], out test))
					{
						node1 = test;
						if (int.TryParse(fields[i], out test))
						{
							node2 = test;
							if (!edges.Any(e => e.Node1 == node1 && e.Node2 == node2) && !edges.Any(e => e.Node1 == node2 && e.Node2 == node1))
							{
								edges.Add(new Edge(node1, node2));
							}

						}
					}
				}
			}

			return edges;
		}

		public object Clone()
		{
			return new Edge(Node1, Node2);
		}

		public override string ToString()
		{
			return $"{Node1}--{Node2}";
		}
	}
}
