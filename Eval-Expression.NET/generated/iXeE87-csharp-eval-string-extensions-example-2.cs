// Description: C# Eval - string-extensions - Example 2
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/iXeE87

// @nuget: Z.Expressions.Eval

using System;
using Z.Expressions;

public class Program
{
	public static void Main()
	{
		
		Console.WriteLine( "1+2".Execute<int>()); // return 3
		Console.WriteLine( "X+Y".Execute(new { X = 1, Y = 2 })); // return 3
	}
}