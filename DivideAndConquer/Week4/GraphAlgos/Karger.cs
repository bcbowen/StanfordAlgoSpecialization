using System;

namespace GraphAlgos
{
    public static class Karger
    {
		public static int Analyze(Graph graph)
		{
			int iterations = CalculateRepetitions(graph.NodeCount);
			int minCuts = int.MaxValue;
			for (int i = 0; i < iterations; i++)
			{
				int result = CalcMinCuts(graph);

				if (result < minCuts)
				{
					Console.WriteLine($"New min: {result} after {i} iterations ");
					minCuts = result;
				}
			}

			Console.WriteLine($"Final min: {minCuts} ");
			return minCuts;
		}

		private static int CalcMinCuts(Graph graph)
		{
			Graph testGraph = graph.Clone() as Graph;

			while (testGraph.NodeCount > 2)
			{
				testGraph.TrimRandomEdge();
			}

			return testGraph.EdgeCount;
		}

		private static int CalculateRepetitions(int nodeCount)
		{
			/*if (nodeCount <= 25) return nodeCount;

			if (nodeCount <= 100) return nodeCount / 2;

			//if (nodeCount <= 100) return nodeCount / 4;

			return nodeCount / 4;

			
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
}
