// Description: C# Eval - linq-dynamic-all-examples - Example 1
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/E82Y4a

using System;
using System.Linq;

public class Program
{
	public static void Main()
	{
		int[] numbers = {1, 11, 3, 19, 41, 65, 19};
		
		var onlyOdd = numbers.All(n => n % 2 == 1);
		
		Console.WriteLine("The list contains only odd numbers: {0}", onlyOdd);
	}
}