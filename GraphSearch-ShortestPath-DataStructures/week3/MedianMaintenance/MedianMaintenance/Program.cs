using System;
using System.IO;
using System.Linq;
using Graph;

namespace ShortestPath
{
    class Program
    {
        static void Main(string[] args)
        {
            const string fileName = "TestData.txt";
            string path = Path.Combine(GetWorkingDirectory().FullName, fileName);

            UndirectedGraph graph = UndirectedGraph.LoadGraph(path);
            graph.CalculateShortestPaths();

            int[] testIds = { 7, 37, 59, 82, 99, 115, 133, 165, 188, 197 };

            
            foreach (int id in testIds)
            {
                Console.Write(graph.ExploredNodes.First(n => n.NodeId == id).MinDistance + ",");
            }

            Console.WriteLine();
            Console.ReadKey();
        }

        private static DirectoryInfo GetWorkingDirectory()
        {
            DirectoryInfo workingDirectory = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent;
            return workingDirectory;
        }
    }
}
