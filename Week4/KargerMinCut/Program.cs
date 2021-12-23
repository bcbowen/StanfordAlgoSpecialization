using System;
using System.IO;

using GraphAlgos;

namespace KargerMinCut
{
    class Program
    {
        private static string GetDataDirectory()
        {
            DirectoryInfo currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            return Path.Combine(currentDirectory.Parent.Parent.Parent.FullName);
        }
        static void Main(string[] args)
        {
            string path = Path.Combine(GetDataDirectory(), "PA4Data.txt");
            if (File.Exists(path))
            {
                Graph graph = Graph.LoadGraph(path);
                int result = Karger.Analyze(graph);
                Console.WriteLine($"Result (min cuts): {result}");
            }
            else 
            {
                Console.WriteLine("Data file not found, punk"); 
            }
            
        }
    }
}
