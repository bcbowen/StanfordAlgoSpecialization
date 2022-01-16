using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using NUnit.Framework;

using Algorithms.Graph.MedianMaintenance;

namespace Algorithms.Tests.Graph
{
    [TestFixture]
    public class MedianMaintenanceTests
    {
        [TestCase(new[] { 5, 1}, new[] { 5 }, new[] { 1 })]
        [TestCase(new[] { 1, 5 }, new[] { 5 }, new[] { 1 })]
        [TestCase(new[] { 1, 5, 3, 6 }, new[] { 5, 6 }, new[] { 3, 1 })]
        [TestCase(new[] { 1, 5, 3, 6 }, new[] { 5, 6 }, new[] { 3, 1 })]
        [TestCase(new[] { 1, 5, 3, 6 , 2}, new[] { 5, 6 }, new[] { 3, 1, 2 })]
        [TestCase(new[] { 1, 5, 3, 6 , 8}, new[] { 5, 6, 8 }, new[] { 3, 1 })]
        public void ValuesDistributedCorrectlyToHeaps(int[] values, int[] hiValues, int[] loValues) 
        {
            MedianOMatic maintenanceMan = new MedianOMatic();

            foreach (int value in values) 
            {
                maintenanceMan.Insert(value);
            }

            Assert.AreEqual(hiValues, maintenanceMan.HiHeap.GetValues());
            Assert.AreEqual(loValues, maintenanceMan.LoHeap.GetValues());

        }
    
        /// <summary>
        /// When there are an even number of values, the counts in each heap should be equal. When there is odd, the last value will be added to the 
        /// appropriate heap and that heap will have one more 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="hiCount"></param>
        /// <param name="loCount"></param>
        [TestCase(new[] { 1, 5, 3, 6, 8, 2, 7, 4, 9, 10 }, 5, 5)] // 10 - both should be equal
        [TestCase(new[] { 1, 5, 3, 6, 8, 2, 7, 4, 9 }, 5, 4)] // 9 - last item was hi, hi heap should have one more
        [TestCase(new[] { 1, 5, 4, 6, 8, 2, 7, 9, 3 }, 4, 5)] // 9 - last item was lo, lo heap should have one more
        public void HeapsBalancedCorrectly(int[] values, int hiCount, int loCount)
        {
            MedianOMatic maintenanceMan = new MedianOMatic();

            foreach (int value in values)
            {
                maintenanceMan.Insert(value);
            }

            Assert.AreEqual(hiCount, maintenanceMan.HiHeap.Count);
            Assert.AreEqual(loCount, maintenanceMan.LoHeap.Count);

        }


        /// <summary>
        /// When there are an even number of values, the median is k/2 th item
        /// When there are an odd number of values, the median is (k+1)/2 th item
        /// </summary>
        /// <param name="values"></param>
        /// <param name="hiCount"></param>
        /// <param name="loCount"></param>
        [TestCase(new[] { 1, 5, 3, 6, 8, 2, 7, 4, 9, 10 }, 5)] // 10 - median is k/2 th item (5th)
        [TestCase(new[] { 1, 5, 3, 6, 8, 2, 7, 4, 9 }, 5)] // 9 - median is (k+1)/2 th item (5th)
        public void MedianCalculatedCorrectly(int[] values, int expectedMedian)
        {
            MedianOMatic maintenanceMan = new MedianOMatic();

            foreach (int value in values)
            {
                maintenanceMan.Insert(value);
            }

            Assert.AreEqual(expectedMedian, maintenanceMan.GetMedian());

        }

        /*
         * 
          	    val median  running total   vals                    e/o m 
                7	7	    7	            7	                    o	1
                9	7	    14	            7,9	                    e	1
                1	7	    21	            1,7,9	                o	2
                3	3	    24	            1,3,7,9	                e	2
                6	6	    30	            1,3,6,7,9	            o	3
                2	3	    33	            1,2,3,6,7,9	            e	3
                5	5	    38	            1,2,3,5,6,7,9	        o	4
                10	5	    43	            1,2,3,5,6,7,9,10	    e	4
                4	5	    48	            1,2,3,4,5,6,7,9,10	    o	5
                8	5	    53	            1,2,3,4,5,6,7,8,9,10    e	5

         * 
         * 
         * 
         7,9,1,3,6,2,5,10,4,8

         */
        /// <summary>
        /// See above comment for explanation of how this total should be calculated for the given test values (from input_random_1_10)
        /// </summary>
        [TestCase(new[] { 7 }, 7, 7)]
        [TestCase(new[] { 7, 9 }, 7, 14)]
        [TestCase(new[] { 7, 9, 1 }, 7, 21)]
        [TestCase(new[] { 7, 9, 1, 3 }, 3, 24)]
        [TestCase(new[] { 7, 9, 1, 3, 6 }, 6, 30)]
        [TestCase(new[] { 7, 9, 1, 3, 6, 2 }, 3, 33)]
        [TestCase(new[] { 7, 9, 1, 3, 6, 2, 5 }, 5, 38)]
        [TestCase(new[] { 7, 9, 1, 3, 6, 2, 5, 10 }, 5, 43)]
        [TestCase(new[] { 7, 9, 1, 3, 6, 2, 5, 10, 4 }, 5, 48)]
        [TestCase(new[] { 7, 9, 1, 3, 6, 2, 5, 10, 4, 8 }, 5, 53)]
        public void RunningMedianTotalIsCorrect(int[] values, int expectedMedian, int expectedRunningTotal) 
        {
            
            MedianOMatic _maintenanceMan = new MedianOMatic();
            foreach (int value in values) 
            {
                _maintenanceMan.Insert(value);
            }

            Assert.AreEqual(expectedMedian, _maintenanceMan.GetMedian());
            Assert.AreEqual(expectedRunningTotal, _maintenanceMan.RunningMedianTotalRaw);
            Assert.AreEqual(expectedRunningTotal, _maintenanceMan.RunningMedianTotalMod);
        }

        [TestCase("input_random_1_10.txt")]
        [TestCase("input_random_2_10.txt")]
        [TestCase("input_random_3_10.txt")]
        [TestCase("input_random_4_10.txt")]
        [TestCase("input_random_5_20.txt")]
        [TestCase("input_random_6_20.txt")]
        [TestCase("input_random_7_20.txt")]
        [TestCase("input_random_8_20.txt")]
        public void MedianTestsTiny(string fileName) 
        {
            RunMedianTest(fileName);
        }

        [TestCase("input_random_9_40.txt")]
        [TestCase("input_random_10_40.txt")]
        [TestCase("input_random_11_40.txt")]
        [TestCase("input_random_12_40.txt")]
        [TestCase("input_random_13_80.txt")]
        [TestCase("input_random_14_80.txt")]
        [TestCase("input_random_15_80.txt")]
        [TestCase("input_random_16_80.txt")]
        public void MedianTestsSmall(string fileName)
        {
            RunMedianTest(fileName);
        }

        [TestCase("input_random_17_160.txt")]
        [TestCase("input_random_18_160.txt")]
        [TestCase("input_random_19_160.txt")]
        [TestCase("input_random_20_160.txt")]
        [TestCase("input_random_21_320.txt")]
        [TestCase("input_random_22_320.txt")]
        [TestCase("input_random_23_320.txt")]
        [TestCase("input_random_24_320.txt")]
        public void MedianTestsMedium(string fileName)
        {
            RunMedianTest(fileName);
        }

        [TestCase("input_random_25_640.txt")]
        [TestCase("input_random_26_640.txt")]
        [TestCase("input_random_27_640.txt")]
        [TestCase("input_random_28_640.txt")]
        [TestCase("input_random_29_1280.txt")]
        [TestCase("input_random_30_1280.txt")]
        [TestCase("input_random_31_1280.txt")]
        [TestCase("input_random_32_1280.txt")]
        public void MedianTestsGrande(string fileName)
        {
            RunMedianTest(fileName);
        }

        [TestCase("input_random_33_2560.txt")]
        [TestCase("input_random_34_2560.txt")]
        [TestCase("input_random_35_2560.txt")]
        [TestCase("input_random_36_2560.txt")]
        [TestCase("input_random_37_5120.txt")]
        [TestCase("input_random_38_5120.txt")]
        [TestCase("input_random_39_5120.txt")]
        [TestCase("input_random_40_5120.txt")]
        public void MedianTestsXL(string fileName)
        {
            RunMedianTest(fileName);
        }

        [TestCase("input_random_41_10000.txt")]
        [TestCase("input_random_42_10000.txt")]
        [TestCase("input_random_43_10000.txt")]
        [TestCase("input_random_44_10000.txt")]
        public void MedianTestsMassive(string fileName)
        {
            RunMedianTest(fileName);
        }

        private void RunMedianTest(string fileName) 
        {
            int expectedMedian;

            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories("MedianMaintenanceData").First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();
            Assert.NotNull(file, "Test file not found");

            MedianOMatic _maintenanceMan = new MedianOMatic();

            string line;
            int value; 

            using (StreamReader reader = new StreamReader(file.FullName)) 
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (int.TryParse(line, out value))
                    {
                        _maintenanceMan.Insert(value);
                    }
                }
                reader.Close();
            }

            using (StreamReader reader = new StreamReader(file.FullName.Replace("input_", "output_"))) 
            {
                line = reader.ReadLine();
                expectedMedian = int.Parse(line);
                reader.Close();
            }

            Assert.AreEqual(expectedMedian, _maintenanceMan.RunningMedianTotalMod);
        }
    }
}
