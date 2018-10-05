// Description: C# Eval - linq-dynamic-concat-examples - Example 2
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/kIv2qY

// @nuget: Z.Expressions.Eval

using System;
using System.Collections.Generic;
using Z.Expressions;
					
public class Program
{
	public static void Main()
	{
		int[] numbersA = {0, 2, 4, 5, 6, 8, 9};
		int[] numbersB = {1, 3, 5, 7, 8};
		
		var allNumbers = numbersA.Execute<IEnumerable<int>>("Concat(numbersB)", new {numbersB});
		
		
		Console.WriteLine("All numbers from both arrays:");
		foreach (var n in allNumbers)
		{
			Console.WriteLine(n.ToString());
		}
	}
}