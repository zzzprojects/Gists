// Description: C# Eval - linq-dynamic-first-examples - Example 3
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/GjrQL8

using System;
using System.Linq;
					
public class Program
{
	public static void Main()
	{
		string[] strings = {"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};

		var startsWithO = strings.First(s => s[0] == 'o');

		Console.WriteLine("A string starting with 'o': {0}", startsWithO);

	}
}