using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace Algorithms.Math
{
    public static class Karatsuba
    {
		public static string Multiply(string x, string y)
		{
			if (x.Length < 4 && y.Length < 4)
			{
				if (int.Parse(x) == 0 || int.Parse(y) == 0) return "0";
				if (x.Length == 1 || y.Length == 1) return (int.Parse(x) * int.Parse(y)).ToString();
			}


			(x, y) = SetSizes(x, y);
			int xLen = x.Length;
			int yLen = y.Length;
			int n = System.Math.Min(xLen, yLen);
			x = x.PadLeft(n, '0');
			y = y.PadLeft(n, '0');
			int halfN = (int)(System.Math.Ceiling((double)n / 2));
			string a, b, c, d;
			(a, b) = SplitValue(x, halfN);

			(c, d) = SplitValue(y, halfN);

			string step1 = Multiply(a, c);
			string step2 = Multiply(b, d);
			string step3a = Multiply(Add(a, b), Add(c, d));
			string step3b = Subtract(step3a, step1);
			string step3 = Subtract(step3b, step2);
			// subtract step 2 from step 3

			string resulta = step1.PadRight(step1.Length + n, '0');
			string resultb = step3.PadRight(step3.Length + halfN, '0');
			string result = Add(Add(resulta, resultb), step2);

			//value = Subtract(value, step1.PadRight(step1.Length + x.Length / 2, '0'));

			return result;
		}

		private static (string, string) SplitValue(string value, int n)
		{
			string y = value.Substring(value.Length - n);
			string x = value.Substring(0, value.Length - n);

			string pattern = "^0+$";
			x = Regex.IsMatch(x, pattern) ? "0" : x.TrimStart('0');

			return (x, y);
		}

		/*
		// subtract y from x
		internal static string Subtract(string x, string y) 
		{
			//Console.WriteLine($"Subtracting {y} from {x}"); 
			if (x == y) return "0"; 
			bool borrow = false;
			StringBuilder result = new StringBuilder();

			if (y.Length < x.Length) y = y.PadLeft(x.Length, '0');

			int top;
			int bottom;
			int difference;
			for (int i = x.Length -1; i >= 0; i--)
			{
				top = int.Parse(x.Substring(i, 1));
				bottom = int.Parse(y.Substring(i, 1));
				if (borrow) top = top > 0 ? top - 1 : 9;
				if (top >= bottom)
				{
					borrow = false;
				}
				else
				{
					borrow = true; 
					top += 10;
				}
				difference = top - bottom;
				result.Insert(0, difference);

			}

			return result.ToString().TrimStart('0');

		}
		*/

		// subtract y from x
		internal static string Subtract(string x, string y)
		{
			BigInteger i = BigInteger.Parse(x);
			BigInteger j = BigInteger.Parse(y);
			return BigInteger.Subtract(i, j).ToString();
		}

        /*
		internal static string Add(string x, string y)
		{
			BigInteger i = BigInteger.Parse(x);
			BigInteger j = BigInteger.Parse(y);
			return BigInteger.Add(i, j).ToString();
		}
		*/

        internal static string Add (string x, string y)
		{
			int carry = 0; 
			int top;
			int bottom;
			int sum;
			int n = System.Math.Max(x.Length, y.Length);
			x = x.PadLeft(n, '0');
			y = y.PadLeft(n, '0');
			if (x.Length == 1 || y.Length == 1) 
			{
				return (int.Parse(x) + int.Parse(y)).ToString();
			}
			StringBuilder result = new StringBuilder(); 
			for (int i = x.Length - 1; i >= 0; i--) 
			{
				top = int.Parse(x.Substring(i, 1)); 
				bottom = int.Parse(y.Substring(i, 1));
				sum = top + bottom + carry;
				if (sum > 9)
				{
					carry = 1;
					result.Insert(0, sum - 10);
				}
				else
				{
					carry = 0; 
					result.Insert(0, sum);
				}
			}
			if (carry == 1)
			{
				result.Insert(0, carry);
			}

            return result.ToString().TrimStart('0');
        }
		

		internal static (string, string) SetSizes(string x, string y)
		{
			x = SetSize(x);
			y = SetSize(y);

			if (x.Length > y.Length)
			{
				y = y.PadLeft(x.Length, '0');
			}
			else if (y.Length > x.Length)
			{
				x = x.PadLeft(y.Length, '0');
			}

			return (x, y);
		}

		internal static string SetSize(string value)
		{
			int len = GetIdealSize(value);
			if (value.Length == len)
			{
				return value;
			}

			return value.PadLeft(len, '0');
		}

		internal static int GetIdealSize(string value)
		{
			if (string.IsNullOrEmpty(value)) return 0;

			// if length is 1 or 2, leave it as it is
			if (value.Length < 3) return value.Length;

			// if the length is already a power of 2 it is good
			if (System.Math.Log2(value.Length) % 1 == 0) return value.Length;

			int testLen = 2;
			while (testLen < value.Length)
			{
				testLen *= 2;
			}

			return testLen;
		}

	}
}
