// Description: C# Eval - linq-dynamic-equalall-examples - Example 1
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/J7H8eE

using System;
using System.Linq;
					
public class Program
{
	public static void Main()
	{
		var wordsA = new[] {"cherry", "apple", "blueberry"};
		var wordsB = new[] {"cherry", "apple", "blueberry"};
		
		var match = wordsA.SequenceEqual(wordsB);
		
		Console.WriteLine("The sequences match: {0}", match);
	}
}