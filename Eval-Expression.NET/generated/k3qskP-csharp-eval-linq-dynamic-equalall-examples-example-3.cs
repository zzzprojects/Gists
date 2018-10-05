// Description: C# Eval - linq-dynamic-equalall-examples - Example 3
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/k3qskP

using System;
using System.Linq;
					
public class Program
{
	public static void Main()
	{
		var wordsA = new[] {"cherry", "apple", "blueberry"};
		var wordsB = new[] {"apple", "blueberry", "cherry"};
	
		var match = wordsA.SequenceEqual(wordsB);
	
		Console.WriteLine("The sequences match: {0}", match);
	}
}