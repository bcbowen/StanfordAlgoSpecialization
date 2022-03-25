using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.SAT
{
    public class Condition
    {
        public int Value1 { get; set; }
        public int Value2 { get; set; }

        public bool Is1 { get; set; }

        public bool Is2 { get; set; }

        public static Condition Parse(string line)
        {
            Condition condition = new Condition();
            string[] parts = line.Split(' ');
            if (parts.Length > 1)
            {
                int val = int.Parse(parts[0]);
                condition.Is1 = val > 0;
                condition.Value1 = System.Math.Abs(val);

                val = int.Parse(parts[1]);
                condition.Is2 = val > 0;
                condition.Value2 = System.Math.Abs(val);
            }
            else
            {
                throw new ArgumentException("Line does not have the expected format.. expected space delimited pair of ints, man", nameof(line));
            }

            return condition;
        }
    }
}
