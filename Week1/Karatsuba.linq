<Query Kind="Program">
  <Namespace>System.Numerics</Namespace>
</Query>

void Main()
{
	BigInteger b = new BigInteger();
	KaratsubaTester.RunTests();
}

static class KaratsubaTester 
{
	public static void RunTests() 
	{
		TestMultiply();
		TestSubtract();
		TestAdd();
		TestSetSizes();
		TestSetSize();
		TestGetIdealSize();
	}

	public static void TestMultiply()
	{
		TestMultiply("2", "4", "8");
		TestMultiply("20", "4", "80");
		TestMultiply("20", "40", "800");
		TestMultiply("134", "46", "6164");
		TestMultiply("5678", "1234", "7006652");
	}

	public static void TestMultiply(string x, string y, string expected)
	{
		string result = Karatsuba.Multiply(x, y);
		if (result == expected)
		{
			Console.WriteLine($"TestMultiply for {x}, {y} PASSED");
		}
		else
		{
			Console.WriteLine($"TestMultiply for {x}, {y} FAILED; Expected {expected}, got {result}");
		}
	}

	public static void TestGetIdealSize() 
	{
		TestGetIdealSize("1", 1);		
		TestGetIdealSize("10", 2);
		TestGetIdealSize("103", 4);
		TestGetIdealSize("1234", 4);
		TestGetIdealSize("123456", 8);
		TestGetIdealSize("12345678", 8);
		TestGetIdealSize("1234567890", 16);
	}

	private static void TestGetIdealSize(string input, int expectedSize) 
	{
		int result = Karatsuba.GetIdealSize(input);
		if (result == expectedSize)
		{
			Console.WriteLine($"TestGetIdealSize for {input} PASSED");
		}
		else
		{
			Console.WriteLine($"TestGetIdealSize for {input} FAILED; Expected {expectedSize} got {result}");
		}
	}

	public static void TestSetSize()
	{
		TestSetSize("1", "1");
		TestSetSize("10", "10");
		TestSetSize("103", "0103");
		TestSetSize("1234", "1234");
		TestSetSize("123456", "00123456");
		TestSetSize("12345678", "12345678");
		TestSetSize("1234567890", "0000001234567890");
	}

	private static void TestSetSize(string input, string expected)
	{
		string result = Karatsuba.SetSize(input);
		if (result == expected)
		{
			Console.WriteLine($"TestSetSize for {input} PASSED");
		}
		else
		{
			Console.WriteLine($"TestSetSize for {input} FAILED; Expected {expected} got {result}");
		}
	}

	public static void TestSetSizes()
	{
		TestSetSizes("1", "2", "1", "2");
		TestSetSizes("13", "2", "13", "02");
		TestSetSizes("1", "10", "01", "10");
		TestSetSizes("103", "0103", "0103", "0103");
		TestSetSizes("1", "1234", "0001", "1234");
	}

	private static void TestSetSizes(string x, string y, string expectedX, string expectedY)
	{
		(string resultX, string resultY) = Karatsuba.SetSizes(x, y);
		if (resultX == expectedX && resultY == expectedY)
		{
			Console.WriteLine($"TestSetSizes for ({x}, {y}) PASSED");
		}
		else
		{
			Console.WriteLine($"TestSetSizes for ({x}, {y}) FAILED; Expected ({expectedX}, {expectedY}) got ({resultX}, {resultY})");
		}
	}

	public static void TestAdd() 
	{
		TestAdd("1", "2", "3");
		TestAdd("100", "234", "334");
		TestAdd("56", "78", "134");
		TestAdd("1000", "80", "1080");
		
	}
	
	public static void TestAdd(string x, string y, string expected)
	{
		string result = Karatsuba.Add(x, y);
		if (result == expected)
		{
			Console.WriteLine($"TestAdd for {x}, {y} PASSED");
		}
		else
		{
			Console.WriteLine($"TestAdd for {x}, {y} FAILED; Expected {expected}, got {result}");
		}
	}

	public static void TestSubtract()
	{
		TestSubtract("3", "2", "1");
		TestSubtract("334", "234", "100");
		TestSubtract("134", "78", "56");
	}

	public static void TestSubtract(string x, string y, string expected)
	{
		string result = Karatsuba.Subtract(x, y);
		if (result == expected)
		{
			Console.WriteLine($"TestSubtract for {x}, {y} PASSED");
		}
		else
		{
			Console.WriteLine($"TestSubtract for {x}, {y} FAILED; Expected {expected}, got {result}");
		}
	}

	

}

// You can define other methods, fields, classes and namespaces here

static class Karatsuba 
{

	public static string Multiply(string x, string y) 
	{
		if (int.Parse(x) == 0 || int.Parse(y) == 0) return "0";
		if (x.Length == 1 || y.Length == 1) return (int.Parse(x) * int.Parse(y)).ToString();

		int xLen = x.Length;
		int yLen = y.Length;
		int n = Math.Max(xLen, yLen);
		x = x.PadLeft(n, '0');
		y = y.PadLeft(n, '0');
		int halfN = (int)(Math.Ceiling((double)n/2));
		string a, b, c, d;
		(a, b) = SplitValue(x, halfN);
		//string a = x.Substring(0, halfN);
		//string b = xLen > halfN ? x.Substring(halfN, xLen - halfN) : "0";

		//string c = y.Substring(0, halfN);
		//string d = yLen > halfN ? y.Substring(halfN, yLen - halfN) : "0";
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
		string x = value.Substring(0, n); 
		string y = value.Substring(n);
		
		x = x.TrimStart('0');
		
		return (x, y);
	}

	/// <summary>Multiple with parameters that have been fixed (same length, length is a power of 2)</summary>
	internal static string MultiplyFixed(string x, string y)
	{
		if (x.Length == 1)
		{
			int i = int.Parse(x);
			int j = int.Parse(y);

			int result = i * j;
			return result.ToString();
		}
		else 
		{
			string a = x.Substring(0, x.Length / 2);
			string b = x.Substring(x.Length / 2, x.Length / 2);
			
			string c = y.Substring(0, y.Length / 2);
			string d = y.Substring(y.Length / 2, y.Length / 2);
			
			string step1 = MultiplyFixed(a, c); 
			string step2 = MultiplyFixed(b, d);
			string step3 = MultiplyFixed(Add(a, b), Add(c, d));
			
			// subtract step 2 from step 3
			string value = Subtract(step3.PadRight(step3.Length + x.Length, '0'), step2);
			
			value = Subtract(value, step1.PadRight(step1.Length + x.Length/2, '0'));
			
			return value;
		}
	}

	// subtract y from x
	internal static string Subtract(string x, string y) 
	{
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

	internal static string Add (string x, string y)
	{
		int carry = 0; 
		int top;
		int bottom;
		int sum;
		int n = Math.Max(x.Length, y.Length);
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
		
		return result.ToString();
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
		if (Math.Log2(value.Length) % 1 == 0) return value.Length;
		
		int testLen = 2;
		while(testLen < value.Length) 
		{
			testLen *= 2;
		}
		
		return testLen;
	}
	
}