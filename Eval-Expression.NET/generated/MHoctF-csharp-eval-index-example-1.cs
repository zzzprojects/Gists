// Description: C# Eval - index - Example 1
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/MHoctF

// @nuget: Z.Expressions.Eval

using System;
using Z.Expressions;
					
public class Program
{
	public static void Main()
	{
		// From simple expression...
		int result = Eval.Execute<int>("X + Y", new { X = 1, Y = 2});
		
		Console.WriteLine(result);
		
		// To complex code.
		result = Eval.Execute<int>(@"
			var list = new List<int>() { 1, 2, 3, 4, 5 };
			var filter = list.Where(x => x < 4);
			return filter.Sum(x => x);");
		
		Console.WriteLine(result);
				
		
	}
}