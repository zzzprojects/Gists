// Description: C# Eval - linq-dynamic-except-examples - Example 1
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/oynmkt

using System;
using System.Linq;
					
public class Program
{
	public static void Main()
	{
		int[] numbersA = {0, 2, 4, 5, 6, 8, 9};
		int[] numbersB = {1, 3, 5, 7, 8};
		
		var aOnlyNumbers = numbersA.Except(numbersB);
		
		Console.WriteLine("Numbers in first array but not second array:");
		foreach (var n in aOnlyNumbers)
		{
			Console.WriteLine(n.ToString());
		}
	}
}