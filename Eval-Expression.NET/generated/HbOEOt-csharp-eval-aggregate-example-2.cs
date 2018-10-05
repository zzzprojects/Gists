// Description: C# Eval - aggregate - Example 2
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/HbOEOt

using System;
using Z.Expressions;

public class Program
{
	public static void Main()
	{
		double[] doubles = {1.7, 2.3, 1.9, 4.1, 2.9};

		var product = doubles.Execute<double>("Aggregate((runningProduct, nextFactor) => runningProduct * nextFactor)");
		
		Console.WriteLine("Total product of all numbers: {0}", product);
	}
}