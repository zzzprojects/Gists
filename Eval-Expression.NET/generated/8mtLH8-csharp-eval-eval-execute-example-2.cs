// Description: C# Eval - eval-execute - Example 2
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/8mtLH8

// @nuget: Z.Expressions.Eval

using System;
using System.Collections.Generic;
using System.Dynamic;
using Z.Expressions;

public class Program
{
	public static void Main()
	{
		// Parameter: Anonymous Type
		object result = Eval.Execute("X + Y", new { X = 1, Y = 2} );
		
		Console.WriteLine(result);
		
		// Parameter: Argument Position
		result = Eval.Execute("{0} + {1}", 1, 2);
		
		Console.WriteLine(result);
		
		// Parameter: Class Member
		dynamic expandoObject = new ExpandoObject();
		expandoObject.X = 1;
		expandoObject.Y = 2;
		result = Eval.Execute("X + Y", expandoObject);

		Console.WriteLine(result);
		
		// Parameter: Dictionary Key
		var values = new Dictionary<string, object>() { {"X", 1}, {"Y", 2} };
		result = Eval.Execute("X + Y", values);
								
		Console.WriteLine(result);
	}
}