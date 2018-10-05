// Description: C# Eval - linq-dynamic-aggregate-examples - Example 3
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/LqobT0

using System;
using System.Linq;

public class Program
{
	public static void Main()
	{
		var startBalance = 100.0;

		int[] attemptedWithdrawals = {20, 10, 40, 50, 10, 70, 30};
	
		var endBalance = attemptedWithdrawals.Aggregate(startBalance, (balance, nextWithdrawal) => nextWithdrawal <= balance ? balance - nextWithdrawal : balance);
		
		Console.WriteLine("Ending balance: {0}", endBalance);
	}
}