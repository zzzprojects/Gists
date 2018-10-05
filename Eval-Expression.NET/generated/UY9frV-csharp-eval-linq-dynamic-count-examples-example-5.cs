// Description: C# Eval - linq-dynamic-count-examples - Example 5
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/UY9frV

// @nuget: Z.Expressions.Eval

using System;
using Z.Expressions;

					
public class Program
{
	public static void Main()
	{
		
		int[] numbers = {5, 4, 1, 3, 9, 8, 6, 7, 2, 0};
	
		var oddNumbers = numbers.Execute<int>("Count(n => n % 2 == 1)");
	
		Console.WriteLine("There are {0} odd numbers in the list.", oddNumbers);
		
	}
}