// Description: C# Eval - linq-dynamic-aggregate-examples - Example 1
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/BZLbt5

using System;
using System.Linq;

public class Program
{
	public static void Main()
	{
		double[] doubles = {1.7, 2.3, 1.9, 4.1, 2.9};

		var product = doubles.Aggregate((runningProduct, nextFactor) => runningProduct * nextFactor);
	
		Console.WriteLine("Total product of all numbers: {0}", product);
	}
}