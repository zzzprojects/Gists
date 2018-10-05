// Description: C# Eval - index - Example 3
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/V6cZ0K

// @nuget: Z.Expressions.Eval

using System;
using System.Dynamic;
using System.Collections.Generic;

using Z.Expressions;
					
public class Program
{
	public static void Main()
	{
		// Anonymous Type
		int result = Eval.Execute<int>("X + Y", new { X = 1, Y = 2} );
		
		Console.WriteLine(result);
		
		// Class Member
		dynamic expandoObject = new ExpandoObject();
		expandoObject.X = 1;
		expandoObject.Y = 2;
		result = Eval.Execute<int>("X + Y", expandoObject);
		
		Console.WriteLine(result);
		
		// Dictionary Key
		var values = new Dictionary<string, object>() { {"X", 1}, {"Y", 2} };
		result = Eval.Execute<int>("X + Y", values);
		
		Console.WriteLine(result);
		
		// Argument Position
		result = Eval.Execute<int>("{0} + {1}", 1, 2);
		
		Console.WriteLine(result);
	}
}