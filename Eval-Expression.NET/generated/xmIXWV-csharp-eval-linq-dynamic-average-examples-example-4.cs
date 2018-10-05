// Description: C# Eval - linq-dynamic-average-examples - Example 4
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/xmIXWV

// @nuget: Z.Expressions.Eval

using System;
using Z.Expressions;
					
public class Program
{
	public static void Main()
	{
		string[] words = {"cherry", "apple", "blueberry"};

		var averageLength = words.Execute<double>("Average(w => w.Length)");

		Console.WriteLine("The average word length is {0} characters.", averageLength);
	}
}