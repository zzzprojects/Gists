// Description: EFE - BulkSaveChanges
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/jNqaMF

// @nuget: EntityFramework
// @nuget: EntityFramework.SqlServerCompact
// @nuget: Microsoft.SqlServer.Compact
// @nuget: Z.EntityFramework.Extensions

using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Z.EntityFramework.Extensions;
					
public class Program
{
	public static void Main()
	{
		// Custom configuration specific for .NET Fiddle
		Z.EntityFramework.Extensions.EntityFrameworkManager.UseFiddleSqlCompact(System.Data.Entity.SqlServerCompact.SqlCeProviderServices.Instance, System.Data.SqlServerCe.SqlCeProviderFactory.Instance);
		
		List<Customer> list = Data();
		
		// BulkSaveChanges
		using (var context = new CustomerContext())
		{	
			// list contains 2000 records		
			context.Customers.AddRange(list);
			
			context.BulkSaveChanges();
			
			FiddleHelper.WriteTable(context.Customers.Take(5));
		}
		
	}
	
	public static List<Customer> Data() 
	{
		List<Customer>  list = new List<Customer>();
		
		for(int i = 0; i < 2000; i++)
		{
			list.Add(new Customer() { Name ="Customer_" + i});
		}
		
		return list;
	}

	public class CustomerContext : DbContext
	{
		public CustomerContext() : base(@"Data Source=ZZZ_Projects.sdf")
		{

		}
		
		public DbSet<Customer> Customers { get; set; }
	}
	
	
	public class Customer
	{
		public int CustomerID { get; set; }
		public string Name { get; set; }
	}
}