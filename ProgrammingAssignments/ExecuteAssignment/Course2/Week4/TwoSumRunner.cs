using System;
using System.IO;

using Algorithms.HashTable;

namespace ExecuteAssignment.Course2.Week4
{
    internal static class TwoSumRunner
    {
        public static int Run() 
        {
            Console.WriteLine("Running Two Sum Algorithm");
            
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Course2", "Week4", "PAData.txt");

            int value;

            return TwoSum.GetSums(path);
        }
    }
}
