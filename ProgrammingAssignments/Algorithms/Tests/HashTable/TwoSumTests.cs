using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithms.HashTable;
using NUnit.Framework;

namespace Algorithms.Tests.HashTable
{
    [TestFixture]
    public class TwoSumTests
    {
        [TestCase("input_random_1_10.txt")]
        [TestCase("input_random_2_10.txt")]
        [TestCase("input_random_3_10.txt")]
        [TestCase("input_random_4_10.txt")]
        [TestCase("input_random_5_20.txt")]
        [TestCase("input_random_6_20.txt")]
        [TestCase("input_random_7_20.txt")]
        [TestCase("input_random_8_20.txt")]
        public void TwoSumTestTiny(string fileName) 
        {
            RunTest(fileName);
        }

        [TestCase("input_random_9_40.txt")]
        [TestCase("input_random_10_40.txt")]
        [TestCase("input_random_11_40.txt")]
        [TestCase("input_random_12_40.txt")]
        [TestCase("input_random_13_80.txt")]
        [TestCase("input_random_14_80.txt")]
        [TestCase("input_random_15_80.txt")]
        [TestCase("input_random_16_80.txt")]
        public void TwoSumTestSmall(string fileName)
        {
            RunTest(fileName);
        }

        [TestCase("input_random_17_160.txt")]
        [TestCase("input_random_18_160.txt")]
        [TestCase("input_random_19_160.txt")]
        [TestCase("input_random_20_160.txt")]
        [TestCase("input_random_21_320.txt")]
        [TestCase("input_random_22_320.txt")]
        [TestCase("input_random_23_320.txt")]
        [TestCase("input_random_24_320.txt")]
        public void TwoSumTestMedium(string fileName)
        {
            RunTest(fileName);
        }

        [TestCase("input_random_25_640.txt")]
        [TestCase("input_random_26_640.txt")]
        [TestCase("input_random_27_640.txt")]
        [TestCase("input_random_28_640.txt")]
        [TestCase("input_random_29_1280.txt")]
        [TestCase("input_random_30_1280.txt")]
        [TestCase("input_random_31_1280.txt")]
        [TestCase("input_random_32_1280.txt")]
        public void TwoSumTestLarge(string fileName)
        {
            RunTest(fileName);
        }

        [TestCase("input_random_33_2560.txt")]
        [TestCase("input_random_34_2560.txt")]
        [TestCase("input_random_35_2560.txt")]
        [TestCase("input_random_36_2560.txt")]
        [TestCase("input_random_37_5120.txt")]
        [TestCase("input_random_38_5120.txt")]
        [TestCase("input_random_39_5120.txt")]
        [TestCase("input_random_40_5120.txt")]
        public void TwoSumTestVenti(string fileName)
        {
            RunTest(fileName);
        }

        private void RunTest(string fileName) 
        {
            DirectoryInfo testDirectory = TestUtils.GetTestCaseDirectory().GetDirectories("TwoSumData").First();
            FileInfo file = testDirectory.GetFiles(fileName).FirstOrDefault();
            Assert.NotNull(file, "Test file not found");

            int result = TwoSum.GetSums(file.FullName);

            string outputPath = file.FullName.Replace("input_", "output_");
            int expectedResult; 
            using (StreamReader reader = new StreamReader(outputPath)) 
            {
                string line = reader.ReadLine();
                expectedResult = int.Parse(line);
                reader.Close();
            }

            Assert.AreEqual(expectedResult, result);
        }
    }
}
