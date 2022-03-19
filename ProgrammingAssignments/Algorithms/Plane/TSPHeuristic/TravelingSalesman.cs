using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Plane.TSPHeuristic
{
    public static class TravelingSalesman
    {



        public static List<Point> LoadData(string path) 
        { 
            List<Point> points = new List<Point>();


            using (StreamReader reader = new StreamReader(path))
            {
                // first line has number of points, can be ignored
                string line = reader.ReadLine();
                int pointId = 1;

                while ((line = reader.ReadLine()) != null)
                {
                    Point point = Point.ParsePoint(line); 
                    if (point.Index == 0) point.Index = pointId++;
                    points.Add(point);

                }
                reader.Close();
            }

            return points;
        }

        /// <summary>
        /// Checks sample of data to determine if it is already sorted in x, y ASC order
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        internal static bool NeedsSorting(List<Point> points) 
        {
            const int sampleSize = 20;

            double lastX = points[0].X; 
            double lastY = points[0].Y;
            int index = 1;
            while (index < sampleSize && index < points.Count) 
            {
                if (points[index].X < lastX) 
                {
                    return true;
                }

                if (points[index].X == lastX && points[index].Y < lastY)
                {
                    return true;
                }

                lastX = points[index].X;
                lastY = points[index].Y; 
                index++;
            }
            return false;
        }
    }
}
