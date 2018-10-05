// Description: C# Eval - linq-dynamic-aggregate-examples - Example 4
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/p67L8v

// @nuget: Z.Expressions.Eval

using System;
using Z.Expressions;

public class Program
{
	public static void Main()
	{
		var startBalance = 100.0;
	
		int[] attemptedWithdrawals = {20, 10, 40, 50, 10, 70, 30};
	
		var endBalance = attemptedWithdrawals.Execute<double>("Aggregate(startBalance, (balance, nextWithdrawal) => ((nextWithdrawal <= balance) ? (balance - nextWithdrawal) : balance)", new {startBalance});
		
		Console.WriteLine("Ending balance: {0}", endBalance);
	}
}