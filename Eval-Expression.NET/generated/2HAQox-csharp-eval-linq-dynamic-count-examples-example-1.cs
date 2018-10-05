// Description: C# Eval - linq-dynamic-count-examples - Example 1
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/2HAQox

using System;
using System.Linq;
					
public class Program
{
	public static void Main()
	{
		int[] factorsOf300 = {2, 2, 3, 5, 5};

		var uniqueFactors = factorsOf300.Distinct().Count();

 
		Console.WriteLine("There are {0} unique factors of 300.", uniqueFactors);	
	
	}
}