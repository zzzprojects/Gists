// Description: C# Eval - linq-dynamic-average-examples - Example 1
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/ytINV0

using System;
using System.Linq;
					
public class Program
{
	public static void Main()
	{
		int[] numbers = {5, 4, 1, 3, 9, 8, 6, 7, 2, 0};
	
		var averageNum = numbers.Average();
	
		Console.WriteLine("The average number is {0}.", averageNum);
	}
}