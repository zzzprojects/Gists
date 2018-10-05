// Description: C# Eval - linq-dynamic-distinct-examples - Example 2
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/uMxKIt

// @nuget: Z.Expressions.Eval

using System;
using System.Collections.Generic;
using Z.Expressions;
					
public class Program
{
	public static void Main()
	{
		int[] factorsOf300 = {2, 2, 3, 5, 5};
	
		var uniqueFactors = factorsOf300.Execute<IEnumerable<int>>("Distinct()");
	
	
		Console.WriteLine("Prime factors of 300:");
		foreach (var f in uniqueFactors)
		{
			Console.WriteLine(f.ToString());
		}
	}
}