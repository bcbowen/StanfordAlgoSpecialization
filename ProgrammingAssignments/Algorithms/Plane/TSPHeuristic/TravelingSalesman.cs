using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Plane.TSPHeuristic
{
    public static class TravelingSalesman
    {
        public static double CalculateShortestTour(string path) 
        {
            List<Point> points = LoadData(path);
            int currentPoint = 0; 
            int visitedCount = 0;
            while (visitedCount < points.Count - 1) 
            {
                int nearestNeighborIndex = -1;
                double minDistance = double.MaxValue;

                for (int j = 1; j < points.Count; j++)
                {
                    if (points[j].Visited || currentPoint == j) continue;
                    if (System.Math.Abs(points[currentPoint].X - points[j].X) < minDistance)
                    {
                        double distance = Point.CalculateDistance(points[currentPoint], points[j]);
                        if (distance < minDistance)
                        {
                            nearestNeighborIndex = j;
                            minDistance = distance;
                        }
                    }
                }
                Debug.Assert(nearestNeighborIndex >= 0);
                
                points[currentPoint].SetNearestNeighbor(points[nearestNeighborIndex]);
                points[nearestNeighborIndex].Visited = true;
                currentPoint = nearestNeighborIndex;
                visitedCount++;
                
            }

            Debug.Assert(points.Count(p => !p.Visited) == 1);

            Point final = points.First(p => !p.Visited);

            // last node goes back to first point
            points[currentPoint].SetNearestNeighbor(final);

            double totalDistance = points.Sum(p => p.NearestNeighborDistance);

            return System.Math.Floor(totalDistance); 
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

    }
}
