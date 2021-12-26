<Query Kind="Program">
  <Namespace>System.Numerics</Namespace>
</Query>

void Main()
{
	//KargerMinCutTester.RunTests();
	
	string path = Path.Combine(GetQueryDirectory(), "PA4Data.txt");
	
	Graph graph = Graph.LoadGraph(path);
	int result = KargerMinCut.Analyze(graph);
	Console.WriteLine($"Result (min cuts): {result}");
	
}


public static string GetQueryDirectory()
{
	FileInfo file = new FileInfo(Util.CurrentQueryPath);
	return file.DirectoryName;
}

static class KargerMinCutTester 
{
	public static void RunTests() 
	{
		List<TestData> testData = LoadTestData();
		int runLimit = 50;
		int runCount = 0;
		DateTime startTime; 
		foreach(TestData testCase in testData)
		{
			startTime = DateTime.Now;
			Console.WriteLine($"Analyzing {testCase.CaseName}");
			int result = KargerMinCut.Analyze(testCase.TestGraph); 
			runCount++; 
			
			if (result == testCase.ExpectedResult)
				Console.WriteLine($"Test {testCase.CaseName} passed, dude!");
			else
				Console.WriteLine($"Test {testCase.CaseName} failed, expected {testCase.ExpectedResult} got {result}");

			if (runCount == runLimit)
			{
				Console.WriteLine($"Halting after {runCount} iterations.");
				break;
			}
			
			Console.WriteLine($"Case ran in {DateTime.Now.Subtract(startTime).TotalSeconds} seconds");
		}
	}

	private static List<TestData> LoadTestData() 
	{
		List<TestData> testDataList = new List<TestData>();
		
		TestData testData;

		DirectoryInfo testCaseDirectory = new DirectoryInfo(Path.Combine(GetQueryDirectory(), "TestCases"));
		//foreach(FileInfo inputFile in testCaseDirectory.GetFiles().Where(f => f.Name.Contains("_0_4"))) 
		foreach(FileInfo inputFile in testCaseDirectory.GetFiles().Where(f => f.Name.StartsWith("input_"))) 
		{
			testData = new TestData(inputFile.Name.Replace("input_", ""));
			testData.TestGraph = Graph.LoadGraph(inputFile.FullName);
					
			FileInfo outputFile = new FileInfo(inputFile.FullName.Replace("input_", "output_"));
			using (StreamReader reader = new StreamReader(outputFile.FullName))
			{
				string line = reader.ReadLine();
				testData.ExpectedResult = int.Parse(line);
				reader.Close();
			}
			
			testDataList.Add(testData);
		}

		return testDataList;
	}

}

class TestData
{
	public TestData(string caseName) 
	{
		CaseName = caseName; 
	}
	
	public string CaseName { get; set; }
	public Graph TestGraph { get; set; }
	public long ExpectedResult {get; set; }

}


static class KargerMinCut 
{
	public static int Analyze(Graph graph)
	{
		int iterations = CalculateRepetitions(graph.EdgeCount * 2);
		int minCuts = int.MaxValue;
		for (int i = 0; i < iterations; i++)
		{
			int result = CalcMinCuts(graph);
			
			if (result < minCuts)
			{
				Console.WriteLine($"New min: {result} ");	
				minCuts = result;
			}
		}
		
		Console.WriteLine($"Final min: {minCuts} ");	
		return minCuts;
	}

	private static int CalcMinCuts(Graph graph) 
	{
		Graph testGraph = graph.Clone() as Graph;

		while(testGraph.NodeCount > 2) 
		{
			testGraph.TrimRandomEdge();
		}
		
		return testGraph.EdgeCount;
	}

	private static int CalculateRepetitions(int nodeCount)
	{
		/*
		// upper bound for min cuts = n choose 2
		double upperBound = (nodeCount * (nodeCount - 1)) / 2;
		
		// probability for min cut in one execution = 
		// upper bound * 1/ n choose 2
		double probability = upperBound * (1 / (nodeCount * (nodeCount - 1) / 2));
		
		// iterations = 1 / probability (number of times to execute the algorithm 
		return (int) (1 / probability);
		*/
		
		/*Fix this calculation*/ 
		return nodeCount;
	}
}

public class Graph : ICloneable
{
	private Random _random = new Random();
	private List<Edge> _edges = new List<Edge>();
	//public List<Edge> Edges { get; private set; }
	Dictionary <string, List<string>> _nodes = new Dictionary<string, System.Collections.Generic.List<string>>();

	public int EdgeCount 
	{ 
		get 
		{
			return _edges.Count();		
		} 
	}

	public int NodeCount
	{
		get
		{
			return _nodes.Count();
		}
	}

	public void AddEdge(string node1, string node2) 
	{
		_edges.Add(new Edge(node1, node2));
	}

	public void AddEdge(Edge edge) 
	{
		AddEdge(edge.Node1, edge.Node2);
	}
	
	public void AddEdges(List<Edge> edges)
	{
		
		foreach(Edge edge in edges) 
		{
			if (!_edges.Any(e => e.Node1 == edge.Node1 && e.Node2 == edge.Node2))
			AddEdge(edge);
		}
	}

	public void AddNodes(string line) 
	{
		char delimiter = line.Contains('\t') ? '\t' : ' ';
		string[] fields = line.Split(delimiter);
		string key = fields[0];
		_nodes.Add(key, new List<string>());
		for (int i = 1; i < fields.Length; i++) 
		{
			_nodes[key].Add(fields[i]);
		}
	}

	public void TrimRandomEdge() 
	{
		int index = _random.Next(0, _edges.Count - 1);
		Edge edge = _edges[index];
		//Console.WriteLine($"Trimming edge {edge} Edge count: {NodeCount} Node Count: {EdgeCount}");
		// edges terminating at node1 of this edge will now terminate at node2
		// node1 will be removed

		string merged = $"{edge.Node1}_{edge.Node2}";

		foreach(Edge e in _edges.Where(e => e != edge))
		{
			if (e.Node1 == edge.Node1 || e.Node1 == edge.Node2)
			{
				e.Node1 = merged;
			}
			
			if (e.Node2 == edge.Node1 || e.Node2 == edge.Node2)
			{
				e.Node2 = merged;
			}
		}
		
		_edges.Remove(edge); 

		// remove self references
		_edges.RemoveAll(e => e.Node1 == e.Node2);
		
		RerouteNodes(edge.Node1, edge.Node2, merged);
	}

	// remove node 2, update references to point to merged node
	private void RerouteNodes(string node1, string node2, string merged)
	{
		foreach(string key in _nodes.Keys.Where(k => k != node2))
		{
			int index = _nodes[key].IndexOf(node1);
			
			if (index > -1) 
			{
				_nodes[key][index] = merged;
			}

			index = _nodes[key].IndexOf(node2);
			if (index > -1)
			{
				// the list contains node2... if it also contains the merged node already 
				// remove it to avoid dupes; 
				// otherwise update it with the new label
				if (_nodes[key].IndexOf(merged) > -1)
				{ 
					_nodes[key].RemoveAt(index);
				}
				else
				{
					_nodes[key][index] = merged;	
				}
			}
		}

		_nodes.Add(merged, new List<string>());
		foreach(string node in _nodes[node1])
		{
			_nodes[merged].Add(node);
		}

		_nodes.Remove(node1);
		/*
		Console.WriteLine($"Removing node {node2}");
		if (node2 == "4")
		{
			Console.WriteLine($"Before: {GetKeys()}");
		}
		*/
		_nodes.Remove(node2);
		
		/*
		if (node2 == "4")
		{
			Console.WriteLine($"After: {GetKeys()}");
		}
		*/
	}

	private string GetKeys() 
	{
		StringBuilder result = new StringBuilder();
		foreach (string key in _nodes.Keys)
		{
			result.Append(key + " ");
		}
		
		return result.ToString();
		
	}

	public object Clone() 
	{
		Graph clone = new Graph();
		foreach (Edge edge in _edges) 
		{
			clone.AddEdge(edge.Clone() as Edge);
		}
		foreach (string key in _nodes.Keys) 
		{
			clone._nodes.Add(key, new List<string>());
			foreach(string node in _nodes[key]) 
			{
				clone._nodes[key].Add(node);
			}
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

/*
public class Vector 
{
	public int Label {get; set; }
	//public List<int> Vectors = new List<int>();
	
	public static Vector Parse(string line)
	{
		Vector vector = new Vector { Label = 0 };
		string[] fields = line.Split(' ');
		if (fields.Length > 0)
		{
			vector.Label = int.Parse(fields[0]);
			for(int i = 1; i < fields.Length; i++) 
			{
				vector.Vectors.Add(int.Parse(fields[i]));
			}
		}
		
		return vector; 
	}
	
}
*/

public class Edge : ICloneable
{
	public Edge(string node1, string node2) 
	{
		Node1 = node1; 
		Node2 = node2; 
	}

	public string Node1 {get; set;}
	public string Node2 {get; set;}

	public static List<Edge> ParseEdges(string line)
	{
		char delimiter = line.Contains('\t') ? '\t' : ' ';
		List<Edge> edges = new List<Edge>();
		
		string[] fields = line.Split(delimiter);
		if (fields.Length > 0)
		{
			string node1, node2; 
			
			//string node1 = fields[0];
			for (int i = 1; i < fields.Length; i++)
			{
				if (fields[0].CompareTo(fields[i]) < 0)
				{
					node1 = fields[0]; 
					node2 = fields[i]; 
				}
				else
				{
					node1 = fields[i]; 
					node2 = fields[0];
				}
				
				if (!edges.Any(e => e.Node1 == node1 && e.Node2 == node2)) 
				{
					edges.Add(new Edge(node1, node2));	
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