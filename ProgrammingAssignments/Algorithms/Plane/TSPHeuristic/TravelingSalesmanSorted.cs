using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Plane.TSPHeuristic
{
    public static class TravelingSalesmanSorted
    {
        /// <summary>
        /// Depends on having the data sorted, works for tiny cases and 2 small cases, but not for the rest of the test cases
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static double CalculateShortestTour(string path) 
        {
            List<Point> points = LoadData(path);

            // Problem file has 33K rows already sorted, test cases are much smaller and not sorted. So we'll make sure we need to sort the data
            if (NeedsSorting(points)) 
            {
                SortPoints(points);
            }

            for (int i = 0; i < points.Count; i++) 
            {
                if (points[i].NearestNeighbor != null) continue;
                int nearestNeighbor = FindNearestNeighbor(points, i);
                points[i].SetNearestNeighbor(points[nearestNeighbor]);

            }

            // last node goes back to first point
            points[points.Count - 1].SetNearestNeighbor(points[0]);

            double distance = points.Sum(p => p.NearestNeighborDistance);

            return System.Math.Floor(distance); 
        }

        internal static int FindNearestNeighbor(List<Point> points, int index)
        {
            int minPointIndex = 0;
            double minDistance = double.MaxValue;

            // search left (points without nearest neighbor already set);
            // don't include first point which will be the neighbor of the final point
            for (int i = index - 1; i > 1; i--)
            {
                if (points[i].NearestNeighbor != null) continue; 

                double distance = Point.CalculateDistance(points[i], points[index]);
                if (distance < minDistance) 
                {
                    minPointIndex = i;
                    minDistance = distance;
                }
                if (System.Math.Abs(points[i].X - points[index].X) > minDistance)
                {
                    break;
                }
            }

            // search right (points without nearest neighbor already set)
            for (int i = index + 1; i < points.Count; i++)
            {
                if (points[i].NearestNeighbor != null) continue;

                double distance = Point.CalculateDistance(points[i], points[index]);
                if (distance < minDistance)
                {
                    minPointIndex = i;
                    minDistance = distance;
                }
                if (System.Math.Abs(points[i].X - points[index].X) > minDistance)
                {
                    break;
                }
            }

            return minPointIndex;
        }


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

        internal static void SortPoints(List<Point> points) 
        {
            points.Sort(PointComparer);
        }

        /// <summary>
        /// Comparer used to sort points in ASC order by x then y
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static int PointComparer(Point p1, Point p2) 
        {
            if (p1.X != p2.X) 
            {
                return p1.X.CompareTo(p2.X);
            }

            return p1.Y.CompareTo(p2.Y);
        }
    }
}
