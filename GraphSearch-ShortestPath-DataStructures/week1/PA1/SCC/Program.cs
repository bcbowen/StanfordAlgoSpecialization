using System;
using System.IO;

using Graph; 

namespace SCC
{
    class Program
    {
        static void Main(string[] args)
        {
            const string fileName = "PAData.txt";
            string path = Path.Combine(GetWorkingDirectory().FullName, fileName);
            
            DirectedGraph graph = DirectedGraph.Load(path);
            int[] result = graph.DoTheKosaraju(5);
            Console.WriteLine($"{result[0]},{result[1]},{result[2]},{result[3]},{result[4]}");
        }

        private static DirectoryInfo GetWorkingDirectory()
        {
            DirectoryInfo workingDirectory = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent;
            return workingDirectory;
        }
    }
}
