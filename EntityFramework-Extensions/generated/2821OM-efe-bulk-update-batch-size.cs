// Description: EFE - Bulk-Update - Batch Size
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/2821OM

// @nuget: EntityFramework
// @nuget: EntityFramework.SqlServerCompact
// @nuget: Microsoft.SqlServer.Compact
// @nuget: Z.EntityFramework.Extensions

using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

public class Program
{
	public static void Main()
	{
		// Custom configuration specific for .NET Fiddle
		Z.EntityFramework.Extensions.EntityFrameworkManager.UseFiddleSqlCompact(System.Data.Entity.SqlServerCompact.SqlCeProviderServices.Instance, System.Data.SqlServerCe.SqlCeProviderFactory.Instance);

		GenerateData();
 
		// BulkUpdate: Batch Size
		using (var context = new EntityContext())
		{
			var list = context.Customers.ToList();
			
			list.ForEach(x => x.IsActive = false);	
			
			context.BulkUpdate(list, options => options.BatchSize = 100);
		}
		
		using (var context = new EntityContext())
		{
			FiddleHelper.WriteTable(context.Customers.Take(5).ToList());	
		}
	}
		 
	public static void GenerateData()
	{
		
		using (var context = new EntityContext())
		{
			var list = new List<Customer>();
			for (int i = 0; i < 1000; i++)
			{
				list.Add(new Customer() { Name ="Customer_" + i, IsActive = true });
			}

			context.Customers.AddRange(list);
			context.BulkSaveChanges();	
		}
		
		using (var context = new EntityContext())
		{
			FiddleHelper.WriteTable(context.Customers.Take(5).ToList());	
		}
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
		public Boolean IsActive { get; set; }
	}
}