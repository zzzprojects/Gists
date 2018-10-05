// Description: C# Eval - linq-dynamic-all-examples - Example 2
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/1M0scW

// @nuget: Z.Expressions.Eval

using System;
using System.Linq;
using Z.Expressions;

public class Program
{
	public static void Main()
	{
	
		int[] numbers = {1, 11, 3, 19, 41, 65, 19};
	
		var onlyOdd = numbers.All(n => "n % 2 == 1");
	
		Console.WriteLine("The list contains only odd numbers: {0}", onlyOdd);
	}
}