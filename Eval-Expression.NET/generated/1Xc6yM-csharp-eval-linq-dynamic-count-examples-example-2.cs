// Description: C# Eval - linq-dynamic-count-examples - Example 2
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/1Xc6yM

// @nuget: Z.Expressions.Eval

using System;
using System.Linq;
using Z.Expressions;
					
public class Program
{
	public static void Main()
	{
		int[] factorsOf300 = {2, 2, 3, 5, 5};
	
		var uniqueFactors = factorsOf300.Distinct().Execute<int>("Count()");
	
		Console.WriteLine("There are {0} unique factors of 300.", uniqueFactors);
	}
}