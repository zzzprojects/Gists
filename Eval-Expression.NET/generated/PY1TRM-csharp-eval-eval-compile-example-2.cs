// Description: C# Eval - eval-compile - Example 2
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/PY1TRM

using System;
using System.Collections.Generic;
using System.Linq;
using Z.Expressions;
					
public class Program
{
	public static void Main()
	{
		// Overload: Up to 9 parameters can be used
		var compiled_1 = Eval.Compile("{0} + {1}", typeof(int), typeof(int));
		object result = compiled_1(1, 2);
		
		Console.WriteLine(result);
		
		// Overload: params Type[]
		var values_1 = new List<int>() {1, 2};
		var types_1 = values_1.Select(x => x.GetType()).ToArray();
		
		var compiled_2 = Eval.Compile("{0} + {1}", types_1);
		result = compiled_2(values_1);
		
		Console.WriteLine(result);
		
		// Overload: IDictionary<string, Type>
		var values_2 = new Dictionary<string, object> { {"X", 1}, {"Y", 2} };
		var types_2 = values_2.ToDictionary(x => x.Key, x => x.Value.GetType());
		
		var compiled = Eval.Compile("X + Y", types_2);
		result = compiled(values_2);
					
		Console.WriteLine(result);
	}
}