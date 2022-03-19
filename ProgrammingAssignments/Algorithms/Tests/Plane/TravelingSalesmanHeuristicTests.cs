using System;
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

        [Test]
        public void NeedsSortingReturnsFalseForBigSortedData() 
        {
            List<Point> points = new List<Point>
            {
                Point.ParsePoint("1 9983.3333 98550.0000"),
                Point.ParsePoint("2 10000.0000 98533.3333"),
                Point.ParsePoint("3 10000.0000 98550.0000"),
                Point.ParsePoint("4 10000.0000 98566.6667"),
                Point.ParsePoint("5 10016.6667 98516.6667"),
                Point.ParsePoint("6 10033.3333 98533.3333"),
                Point.ParsePoint("7 10033.3333 98550.0000"),
                Point.ParsePoint("8 10033.3333 98583.3333"),
                Point.ParsePoint("9 10050.0000 98550.0000"),
                Point.ParsePoint("10 10066.6667 98516.6667"),
                Point.ParsePoint("11 10066.6667 98533.3333"),
                Point.ParsePoint("12 10083.3333 98550.0000"),
                Point.ParsePoint("13 10083.3333 98583.3333"),
                Point.ParsePoint("14 10150.0000 98533.3333"),
                Point.ParsePoint("15 10150.0000 98600.0000"),
                Point.ParsePoint("16 10183.3333 98483.3333"),
                Point.ParsePoint("17 10200.0000 98500.0000"),
                Point.ParsePoint("18 10216.6667 98516.6667"),
                Point.ParsePoint("19 10216.6667 98533.3333"),
                Point.ParsePoint("20 10216.6667 98616.6667"),
                Point.ParsePoint("21 10233.3333 98500.0000"),
                Point.ParsePoint("22 10233.3333 98616.6667"),
                Point.ParsePoint("23 10250.0000 98516.6667"),
                Point.ParsePoint("24 10250.0000 98600.0000"),
            };

            Assert.False(TravelingSalesman.NeedsSorting(points));
        }

        [Test]
        public void NeedsSortingReturnsFalseForSmallSortedData() 
        {
            List<Point> points = new List<Point>
            { 
                Point.ParsePoint("1 9983.3333 98550.0000"), 
                Point.ParsePoint("2 10000.0000 98533.3333"),
                Point.ParsePoint("3 10000.0000 98550.0000"),
                Point.ParsePoint("4 10000.0000 98566.6667"),
                Point.ParsePoint("5 10016.6667 98516.6667"),
                Point.ParsePoint("6 10033.3333 98533.3333"),
                Point.ParsePoint("7 10033.3333 98550.0000"),
                Point.ParsePoint("8 10033.3333 98583.3333"),
                Point.ParsePoint("9 10050.0000 98550.0000")
            };

            Assert.False(TravelingSalesman.NeedsSorting(points));

        }

        [TestCase(15, 10)]
        [TestCase(29, 100)]
        public void NeedsSortingReturnsTrueForUnsortedDataFile(int testNumber, int count)
        {
            string fileName = $"input_simple_{testNumber}_{count}.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories(DataDirectory).First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();
            Assert.NotNull(file, "Test file not found");

            List<Point> points = TravelingSalesman.LoadData(file.FullName);
            Assert.True(TravelingSalesman.NeedsSorting(points));
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


        [TestCase(15, 10)]
        [TestCase(29, 100)]
        public void DateLoadedFromTestFileSortedCorrectly(int testNumber, int count)
        {
            string fileName = $"input_simple_{testNumber}_{count}.txt";
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories(DataDirectory).First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();
            Assert.NotNull(file, "Test file not found");

            List<Point> points = TravelingSalesman.LoadData(file.FullName);
            Assert.True(TravelingSalesman.NeedsSorting(points));

            TravelingSalesman.SortPoints(points);

            Assert.False(TravelingSalesman.NeedsSorting(points));
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

            int result = TravelingSalesman.CalculateShortestTour(file.FullName);

            int expected = GetExpectedOutput(file.FullName);

            Assert.AreEqual(expected, result);
        }

        private int GetExpectedOutput(string fileName)
        {
            int result;
            string outputFileName = fileName.Replace("input_", "output_");
            using (StreamReader reader = new StreamReader(outputFileName))
            {
                string line = reader.ReadLine();
                reader.Close();               
                result = int.Parse(line);
            }

            return result;
        }
    }
}
