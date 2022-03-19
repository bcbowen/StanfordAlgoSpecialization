using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Plane.TSPHeuristic
{
    public class Point
    {
        public Point(int index, double x, double y)
        {
            Index = index;
            X = x;
            Y = y;
        }
        
        public int Index { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public Point NearestNeighbor { get; private set; }

        public double NearestNeighborDistance { get; private set; }
        public void SetNearestNeighbor(Point neighbor)
        {
            NearestNeighbor = neighbor;

            double distance = System.Math.Sqrt(System.Math.Pow(neighbor.X - X, 2) + System.Math.Pow(neighbor.Y - Y, 2));
            NearestNeighborDistance = distance;
        }
    }
}
