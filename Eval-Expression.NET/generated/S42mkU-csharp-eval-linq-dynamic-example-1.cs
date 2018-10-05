// Description: C# Eval - linq-dynamic - Example 1
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/S42mkU

// @nuget: Z.Expressions.Eval

using System;
using System.Collections.Generic;
using Z.Expressions;

public class Program
{
	public static void Main()
	{
		var list = new List<int>() { 1, 2, 3, 4, 5 };
		
		var list2 = list.Where(x => "x > 2");
		var list3 = list.Where(x => "x > X", new { X = 2 }); // with parameter
		
		foreach (var value in list) 
		{
			Console.WriteLine(value);
		}	
		
		Console.WriteLine("");
		
		foreach (var value in list2) 
		{
			Console.WriteLine(value);
		}
		
		Console.WriteLine("");
		
		foreach (var value in list3) 
		{
			Console.WriteLine(value);
		}
	}
}