// Description: C# Eval - eval-compile - Example 2
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/870F71

// @nuget: Z.Expressions.Eval

using System;
using Z.Expressions;
using System.Collections.Generic;
using System.Linq;
					
public class Program
{
	public static void Main()
	{		
		var compiled = Eval.Compile("{0} + {1}", typeof(int), typeof(int));
		object resul = compiled(1, 2);
		
		Console.WriteLine(resul);
		
		// Overload: params Type[]
		var values_1 = new List<int>() {1, 2};
		var types = values_1.Select(x => x.GetType()).ToArray();
		
		var compiled_2 = Eval.Compile("{0} + {1}", types);
		var result = compiled_2(values_1);
		
		Console.WriteLine(result);
		
		// Overload: IDictionary<string, Type>
		var values_2 = new Dictionary<string, object> { {"X", 1}, {"Y", 2} };
		var types_2 = values_2.ToDictionary(x => x.Key, x => x.Value.GetType());
		
		var compiled_3 = Eval.Compile("X + Y", types_2);
		result = compiled_3(values_2);
		
		Console.WriteLine(result);
	}
}