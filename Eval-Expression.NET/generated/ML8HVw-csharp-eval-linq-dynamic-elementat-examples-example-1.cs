// Description: C# Eval - linq-dynamic-elementat-examples - Example 1
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/ML8HVw

using System;
using System.Linq;
					
public class Program
{
	public static void Main()
	{
		int[] numbers = {5, 4, 1, 3, 9, 8, 6, 7, 2, 0};

		var fourthLowNum = numbers.Where(n => n > 5).ElementAt(1); // second number is index 1 because sequences use 0-based indexing 

		Console.WriteLine("Second number > 5: {0}", fourthLowNum);
	}
}