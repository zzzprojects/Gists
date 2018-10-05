// Description: C# Eval - eval-compile - Example 1
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/MBHlX8

// @nuget: Z.Expressions.Eval

using System;
using Z.Expressions;
					
public class Program
{
	public static void Main()
	{
		// Delegate Func
		var compiled_1 = Eval.Compile<Func<int, int, int>>("{0} + {1}");
		int result = compiled_1(1, 2);
		
		Console.WriteLine(result);
		
		// Delegate Action
		var compiled_2 = Eval.Compile<Action<int, int>>("{0} + {1}");
		compiled_2(1, 2);
		
		Console.WriteLine(result);
		
		// Named Parameter
		var compiled_3 = Eval.Compile<Func<int, int, int>>("X + Y", "X", "Y");
		result = compiled_3(1, 2);
					
		Console.WriteLine(result);
	}
}