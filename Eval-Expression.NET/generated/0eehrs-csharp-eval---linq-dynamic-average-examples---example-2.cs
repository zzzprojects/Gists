// @nuget: Z.Expressions.Eval

using System;
using Z.Expressions;
					
public class Program
{
	public static void Main()
	{
		int[] numbers = {5, 4, 1, 3, 9, 8, 6, 7, 2, 0};
		
		var averageNum = numbers.Execute<double>("Average()");
		
		Console.WriteLine("The average number is {0}.", averageNum);
	}
}