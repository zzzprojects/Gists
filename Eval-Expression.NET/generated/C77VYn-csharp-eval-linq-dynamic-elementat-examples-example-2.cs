// Description: C# Eval - linq-dynamic-elementat-examples - Example 2
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/C77VYn

// @nuget: Z.Expressions.Eval

using System;
using System.Linq;
using Z.Expressions;
					
public class Program
{
	public static void Main()
	{
		int[] numbers = {5, 4, 1, 3, 9, 8, 6, 7, 2, 0};

		var fourthLowNum = numbers.Where(n => n > 5).Execute<int>("ElementAt(1)"); // second number is index 1 because sequences use 0-based indexing 
		
		Console.WriteLine("Second number > 5: {0}", fourthLowNum);
	}
}