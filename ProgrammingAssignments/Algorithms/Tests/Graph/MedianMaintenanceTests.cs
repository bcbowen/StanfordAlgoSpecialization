using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
