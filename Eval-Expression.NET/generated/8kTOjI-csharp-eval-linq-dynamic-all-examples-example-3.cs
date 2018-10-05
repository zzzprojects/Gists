// Description: C# Eval - linq-dynamic-all-examples - Example 3
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/8kTOjI

// @nuget: Z.Expressions.Eval

using System;
using Z.Expressions;
					

public class Program
{
	public static void Main()
	{
		int[] numbers = {1, 11, 3, 19, 41, 65, 19};

		var onlyOdd = numbers.Execute<bool>("All(n => n % 2 == 1)");
		
		Console.WriteLine("The list contains only odd numbers: {0}", onlyOdd);
	}
}