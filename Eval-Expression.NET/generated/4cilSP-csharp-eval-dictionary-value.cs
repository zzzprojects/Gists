// Description: C# Eval - Dictionary Value
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/4cilSP

// @nuget: Z.Expressions.Eval

using System;
using System.Collections.Generic;
using System.Dynamic;
using Z.Expressions;

public class Program
{
	public static void Main()
	{
		EvalManager.DefaultContext.RegisterStaticMember(typeof(CustomStaticMember));

		var dictionary = new Dictionary<string, object>();

		dictionary.Add("x", false);
		dictionary.Add("y", 1);
		dictionary.Add("z", 2);

		var result = Eval.Execute("IIF(x, y, z);", dictionary);
		
		Console.WriteLine(result);
	}
	
	public static class CustomStaticMember
	{
		public static object IIF(bool predicate, object valueTrue, object valueFalse)
		{
			return predicate ? valueTrue : valueFalse;
		}
		
		public static bool AND(bool a, bool b)
		{
			return a && b;
		}
	}
}