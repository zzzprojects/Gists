// Description: C# Eval - any - Example 1
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/36Tkzn

using System;
using System.Linq;
					
public class Program
{
	public static void Main()
	{
		string[] words = {"believe", "relief", "receipt", "field"};
	
		var iAfterE = words.Any(w => w.Contains("ei"));
	
		Console.WriteLine("There is a word that contains in the list that contains 'ei': {0}", iAfterE);

	}
}