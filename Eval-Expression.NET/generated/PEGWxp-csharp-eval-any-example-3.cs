// Description: C# Eval - any - Example 3
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/PEGWxp

// @nuget: Z.Expressions.Eval

using System;
using Z.Expressions;
					
public class Program
{
	public static void Main()
	{
		string[] words = {"believe", "relief", "receipt", "field"};
	
		var iAfterE = words.Execute<bool>("Any(w => w.Contains('ei'))");		
		
		Console.WriteLine("There is a word that contains in the list that contains 'ei': {0}", iAfterE);
	}
}