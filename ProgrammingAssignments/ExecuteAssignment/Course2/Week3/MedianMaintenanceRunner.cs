using System;
using System.IO;
using Algorithms.Graph.MedianMaintenance;
namespace ExecuteAssignment.Course2.Week3
{
    public static class MedianMaintenanceRunner
    {

        public static void Run() 
        {
            Console.WriteLine("Running Median Maintenance Algorithm");
            MedianOMatic maintenanceMan = new MedianOMatic();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Course2", "Week3", "PAData.txt");

            FileInfo file = new FileInfo(path);

            string line;
            int value;

            using (StreamReader reader = new StreamReader(file.FullName))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (int.TryParse(line, out value))
                    {
                        maintenanceMan.Insert(value);
                    }
                }
                reader.Close();
            }

            Console.WriteLine(maintenanceMan.RunningMedianTotalMod);
        }

    }
}
