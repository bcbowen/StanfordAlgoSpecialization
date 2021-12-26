<Query Kind="Program" />

void Main()
{
	double[] values = {.1, .5, 2, 10, 100};

	foreach (double n in values)
	{
		Console.WriteLine($"n = {n}; a: {a(n)}; b: {b(n)}; c: {c(n)}; d: {d(n)}; e: {e(n)}"); 	
		
	}
	
}

// You can define other methods, fields, classes and namespaces here

double a (double n) 
{
	return Math.Pow(2, Math.Log2(n));
}

double b (double n)
{
	return Math.Pow(2, Math.Pow(2, Math.Log2(n)));
}

double c(double n) 
{
	return Math.Pow(n, 5/2);
}

double d(double n) 
{
	return Math.Pow(2, Math.Pow(n, 2));	
}

double e(double n) 
{
	return Math.Pow(n, 2) * Math.Log2(n);
}