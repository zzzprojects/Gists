// Description: C# Eval - linq-dynamic-average-examples - Example 3
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/CjVP6t

// @nuget: Z.Expressions.Eval

using System;
using System.Linq;
					
public class Program
{
	public static void Main()
	{
		string[] words = {"cherry", "apple", "blueberry"};

		var averageLength = words.Average(w => w.Length);
		
		Console.WriteLine("The average word length is {0} characters.", averageLength);

	}
}