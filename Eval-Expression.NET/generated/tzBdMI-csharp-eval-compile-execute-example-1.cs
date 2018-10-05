// Description: C# Eval - compile-execute - Example 1
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/tzBdMI

// @nuget: Z.Expressions.Eval

using System;
using Z.Expressions;
					
public class Program
{
	public static void Main()
	{

		var context = new EvalContext();
		// ... context options ...
		
		var orderItem = new OrderItem();
		
		orderItem.Price = 5.25;
		orderItem.Quantity = 4;
		
		string code = "Price * Quantity";
		var price = context.Execute<decimal>(code, orderItem);
				
		Console.WriteLine(price);
	}
	
	public class OrderItem 
	{
		public double Price {get;set;}	
		public int Quantity {get;set;}	
	}
}