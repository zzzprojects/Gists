// Description: C# Eval - index - Example 4
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/eZM4mh

// @nuget: Z.Expressions.Eval

using System;
using Z.Expressions;
					
public class Program
{
	public static void Main()
	{
		int result = Eval.Execute<int>(@"
		var list = new List<int>() { 1, 2, 3, 4, 5 };
		return list.Where(x => x > X).Take(Y).Count();", new { X = 1, Y = 2});
		
		Console.WriteLine(result);
	}
}