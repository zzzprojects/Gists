// Description: C# Eval - index - Example 5
// Website: https://eval-expression.net/
// Run: https://dotnetfiddle.net/yW8S35

// @nuget: Z.Expressions.Eval

using System;
using Z.Expressions;
					
public class Program
{
	public static void Main()
	{
		var customer = new Customer() { Name = "ZZZ" };
		
		var nameGetter = Eval.Compile<Func<Customer, string>>("x.Name", "x");
		var name = nameGetter(customer);
				
		Console.WriteLine(name);
				
		
	}
	
	public class Customer
	{
		public String Name {get;set;}
	}
}