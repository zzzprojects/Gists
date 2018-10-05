// Description: EFE - BulkUpdate
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/2PIVE8

// @nuget: EntityFramework
// @nuget: EntityFramework.SqlServerCompact
// @nuget: Microsoft.SqlServer.Compact
// @nuget: Z.EntityFramework.Extensions
using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
					
public class Program
{
	public static void Main()
	{
		// Custom configuration specific for .NET Fiddle
		Z.EntityFramework.Extensions.EntityFrameworkManager.UseFiddleSqlCompact(System.Data.Entity.SqlServerCompact.SqlCeProviderServices.Instance, System.Data.SqlServerCe.SqlCeProviderFactory.Instance);
		
		
		// Insert data
		using (var context = new EntityContext())
		{
			context.BulkInsert(Data(), options => options.BatchSize = 100);
			
			FiddleHelper.WriteTable(context.Customers.Take(5).ToList());
		}
		
		// BulkUpdate: update data
		using (var context = new EntityContext())
		{
			var list = context.Customers.ToList();
			Console.WriteLine(list.Count);
			
			foreach(var customer in list)
			{
				customer.Name += " U";
			}
			
			context.BulkUpdate(list);
			
			FiddleHelper.WriteTable(context.Customers.Take(5).ToList());
		}

	}
	
	public static List<Customer> Data() 
	{
		List<Customer>  list = new List<Customer>();
		
		for(int i = 0; i < 1000; i++)
		{
			list.Add(new Customer() { Name ="Customer_" + i, Code ="Code_" + i});
		}
		
		return list;
	}

	public class EntityContext : DbContext
	{
		public EntityContext() : base(@"Data Source=ZZZ_Projects.sdf")
		{

		}
		
		public DbSet<Customer> Customers { get; set; }
	}
	
	
	public class Customer
	{
		public int CustomerID { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
	}
}