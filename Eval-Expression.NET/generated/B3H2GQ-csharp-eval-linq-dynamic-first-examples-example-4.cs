// Description: C# Eval - linq-dynamic-first-examples - Example 4
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/B3H2GQ

// @nuget: Z.Expressions.Eval

using System;
using System.Linq;
using Z.Expressions;
					
public class Program
{
	public static void Main()
	{
		string[] strings = {"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};

		var startsWithO = strings.First(s => "s[0] == 'o'");

		Console.WriteLine("A string starting with 'o': {0}", startsWithO);
	}
}