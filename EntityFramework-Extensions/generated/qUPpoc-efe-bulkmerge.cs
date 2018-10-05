// Description: EFE - BulkMerge
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/qUPpoc

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
			context.BulkInsert(Data());
			
			FiddleHelper.WriteTable(context.Customers.ToList());
		}
		
		// BulkMerge: Merge data
		using (var context = new EntityContext())
		{
			var list = context.Customers.ToList();
			
			foreach(var customer in list)
			{
				customer.Name += " U";
			}
			
			list.Add(new Customer(){ Name = "New Customer 1", IsActive = true });
			list.Add(new Customer(){ Name = "New Customer 2", IsActive = true });
			
			context.BulkMerge(list);
			
			FiddleHelper.WriteTable(context.Customers.ToList());
		}


	}
	
	public static List<Customer> Data() 
	{
		List<Customer>  list = new List<Customer>();
		
		bool isActive = false;
		
		for(int i = 0; i < 3; i++)
		{
			isActive = !isActive;
			list.Add(new Customer() { Name ="Customer_" + i, IsActive = isActive });
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
		public bool IsActive { get; set; }
	}
}