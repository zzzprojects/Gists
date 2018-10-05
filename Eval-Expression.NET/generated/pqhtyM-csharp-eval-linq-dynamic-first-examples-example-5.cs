// Description: C# Eval - linq-dynamic-first-examples - Example 5
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/pqhtyM

// @nuget: Z.Expressions.Eval

using System;
using Z.Expressions;
					
public class Program
{
	public static void Main()
	{
		string[] strings = {"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};

		var startsWithO =  strings.Execute<string>("First(s => s[0] == 'o')");

		Console.WriteLine("A string starting with 'o': {0}", startsWithO);
	}
}