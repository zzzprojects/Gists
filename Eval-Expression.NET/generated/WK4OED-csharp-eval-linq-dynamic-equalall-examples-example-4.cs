// Description: C# Eval - linq-dynamic-equalall-examples - Example 4
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/WK4OED

// @nuget: Z.Expressions.Eval

using System;
using Z.Expressions;
					
public class Program
{
	public static void Main()
	{
		var wordsA = new[] {"cherry", "apple", "blueberry"};
		var wordsB = new[] {"apple", "blueberry", "cherry"};
	
		var match = wordsA.Execute<bool>("SequenceEqual(wordsB)", new {wordsB});
	
		Console.WriteLine("The sequences match: {0}", match);
	}
}