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

        /// <summary>
        /// Parse point from string representation. 2 formats supported: 
        /// 1 (point, x, y - space delimited): 1 9983.3333 98550.0000
        /// 2 (x, y - space delimited): 0.09273476967488037 0.41528476435904427
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static Point ParsePoint(string line) 
        {
            string[] fields = line.Split(' ');
            if (fields.Length < 2) 
            {
                throw new ArgumentException("Unsupported format for Point", nameof(line));
            }
            int index = 0;
            int xIndex; 
            int yIndex;
            double x; 
            double y;

            if (fields.Length > 2)
            {
                if (!int.TryParse(fields[0], out index))
                {
                    throw new ArgumentException("Unable to parse index from line", nameof(line));
                }
                xIndex = 1;
            }
            else 
            {
                xIndex = 0;
            }
            yIndex = xIndex + 1;

            if (!double.TryParse(fields[xIndex], out x))
            {
                throw new ArgumentException("Unable to parse X from line", nameof(line));
            }

            if (!double.TryParse(fields[yIndex], out y))
            {
                throw new ArgumentException("Unable to parse Y from line", nameof(line));
            }

            return new Point(index, x, y);
        }
    }
}
