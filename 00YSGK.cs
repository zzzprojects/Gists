That's an update// @nuget: Z.Expressions.Eval

using System;
using System.Collections.Generic;
using Z.Expressions;
					
public class Program
{
	public static void Main()
	{
		
		var context = new EvalContext();
		// ... context options ...
		
		string code = "Price * Quantity";
		var compiled = context.Compile<Func<OrderItem, decimal>>(code);
		
		List<OrderItem> list = new List<OrderItem>();
		
		for (int i = 1; i <= 5; i++)
        {
			list.Add(new OrderItem() {Price = i*0.25, Quantity = i });
        }
		
		decimal totals = 0;
		foreach(var item in list)
		{
			var value = compiled(item);
			Console.WriteLine(value);
			totals += value;
		}
				
		Console.WriteLine("");
		Console.WriteLine("Total : " + totals);
	}
	
	public class OrderItem 
	{
		public double Price {get;set;}	
		public int Quantity {get;set;}	
	}
}
