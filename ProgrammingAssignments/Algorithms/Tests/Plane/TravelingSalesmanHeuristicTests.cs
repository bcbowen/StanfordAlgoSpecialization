﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using Algorithms.Plane.TSPHeuristic;

namespace Algorithms.Tests.Plane
{
    [TestFixture]
    public class TravelingSalesmanHeuristicTests
    {
        const string DataDirectory = "TSPHeuristicData"; 

        [Test]
        public void DataFileLoaded()
        {
            string fileName = "input_simple_15_10.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories(DataDirectory).First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();
            Assert.NotNull(file, "Test file not found");

            List<Point> points = TravelingSalesman.LoadData(file.FullName);
            Assert.AreEqual(10, points.Count);
        }


        [TestCase("2 10000.0000 98533.3333", 2, 10000.0, 98533.3333)]
        [TestCase("2.8510790898786214 0.041701200928715654", 0, 2.8510790898786214, 0.041701200928715654)]
        public void PointParsedFromValidLine(string line, int expectedIndex, double expectedX, double expectedY) 
        {
            Point point = Point.ParsePoint(line);
            Assert.AreEqual(expectedIndex, point.Index);
            Assert.AreEqual(expectedX, point.X);
            Assert.AreEqual(expectedY, point.Y);
        }

        [TestCase(1, 2)]
        [TestCase(2, 2)]
        [TestCase(3, 2)]
        [TestCase(4, 2)]
        [TestCase(5, 4)]
        [TestCase(6, 4)]
        [TestCase(7, 4)]
        [TestCase(8, 4)]
        public void CalculateShortestPathTinyFiles(int testNumber, int count) 
        {
            RunTSPHeuristic(testNumber, count);
        }

        [TestCase(9, 8)]
        [TestCase(10, 8)]
        [TestCase(11, 8)]
        [TestCase(12, 8)]
        [TestCase(13, 10)]
        [TestCase(14, 10)]
        [TestCase(15, 10)]
        [TestCase(16, 10)]
        public void CalculateShortestPathSmallFiles(int testNumber, int count)
        {
            RunTSPHeuristic(testNumber, count);
        }

        [TestCase(17, 20)]
        [TestCase(18, 20)]
        [TestCase(19, 20)]
        [TestCase(20, 20)]
        [TestCase(21, 40)]
        [TestCase(22, 40)]
        [TestCase(23, 40)]
        [TestCase(24, 40)]
        public void CalculateShortestPathMediumFiles(int testNumber, int count)
        {
            RunTSPHeuristic(testNumber, count);
        }

        [TestCase(25, 80)]
        [TestCase(26, 80)]
        [TestCase(27, 80)]
        [TestCase(28, 80)]
        [TestCase(29, 100)]
        [TestCase(30, 100)]
        [TestCase(31, 100)]
        [TestCase(32, 100)]
        [TestCase(33, 200)]
        [TestCase(34, 200)]
        [TestCase(35, 200)]
        [TestCase(36, 200)]
        [TestCase(37, 400)]
        [TestCase(38, 400)]
        [TestCase(39, 400)]
        [TestCase(40, 400)]
        public void CalculateShortestPathLargeFiles(int testNumber, int count)
        {
            RunTSPHeuristic(testNumber, count);
        }

        [TestCase(41, 800)]
        [TestCase(42, 800)]
        [TestCase(43, 800)]
        [TestCase(44, 800)]
        [TestCase(45, 1000)]
        [TestCase(46, 1000)]
        [TestCase(47, 1000)]
        [TestCase(48, 1000)]
        [TestCase(49, 2000)]
        [TestCase(50, 2000)]
        [TestCase(51, 2000)]
        [TestCase(52, 2000)]
        [TestCase(53, 4000)]
        [TestCase(54, 4000)]
        [TestCase(55, 4000)]
        [TestCase(56, 4000)]
        public void CalculateShortestPathXLFiles(int testNumber, int count)
        {
            RunTSPHeuristic(testNumber, count);
        }

        [TestCase(57, 8000)]
        [TestCase(58, 8000)]
        [TestCase(59, 8000)]
        [TestCase(60, 8000)]
        [TestCase(61, 10000)]
        [TestCase(62, 10000)]
        [TestCase(63, 10000)]
        [TestCase(64, 10000)]
        [TestCase(65, 20000)]
        [TestCase(66, 20000)]
        [TestCase(67, 20000)]
        [TestCase(68, 20000)]
        public void CalculateShortestPathHugeFiles(int testNumber, int count)
        {
            RunTSPHeuristic(testNumber, count);
        }

        [TestCase(69, 40000)]
        [TestCase(70, 40000)]
        [TestCase(71, 40000)]
        [TestCase(72, 40000)]
        public void CalculateShortestPathGinormousFiles(int testNumber, int count)
        {
            RunTSPHeuristic(testNumber, count);
        }

        /// <summary>
        /// Run the Traveling Salesman Heuristic algorithm
        /// </summary>
        /// <param name="testNumber"></param>
        /// <param name="count"></param>
        private void RunTSPHeuristic(int testNumber, int count)
        {
            string fileName = $"input_simple_{testNumber}_{count}.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories(DataDirectory).First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();
            Assert.NotNull(file, "Test file not found");

            double result = TravelingSalesman.CalculateShortestTour(file.FullName);

            double expected = GetExpectedOutput(file.FullName);

            Assert.AreEqual(expected, result);
        }


        private double GetExpectedOutput(string fileName)
        {
            double result;
            string outputFileName = fileName.Replace("input_", "output_");
            using (StreamReader reader = new StreamReader(outputFileName))
            {
                string line = reader.ReadLine();
                reader.Close();               
                result = double.Parse(line);
            }

            return result;
        }
    }
}
