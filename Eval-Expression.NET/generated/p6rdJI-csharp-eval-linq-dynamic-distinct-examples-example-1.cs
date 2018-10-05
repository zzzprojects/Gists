// Description: C# Eval - linq-dynamic-distinct-examples - Example 1
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/p6rdJI

using System;
using System.Linq;
					
public class Program
{
	public static void Main()
	{
		int[] factorsOf300 = {2, 2, 3, 5, 5};

		var uniqueFactors = factorsOf300.Distinct();

		Console.WriteLine("Prime factors of 300:");
		foreach (var f in uniqueFactors)
		{
			Console.WriteLine(f.ToString());
		}
	}
}