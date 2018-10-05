// Description: C# Eval - linq-dynamic-count-examples - Example 3
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/3LGjG3

using System;
using System.Linq;
					
public class Program
{
	public static void Main()
	{
		int[] numbers = {5, 4, 1, 3, 9, 8, 6, 7, 2, 0};

		var oddNumbers = numbers.Count(n => n % 2 == 1);

		Console.WriteLine("There are {0} odd numbers in the list.", oddNumbers);


	}
}