// Description: EFE - Bulk-Merge - Batch Size
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/RJtqZq

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
		

		// BulkMerge: Batch Size
		using (var context = new EntityContext())
		{
			var list = context.Customers.ToList();
			
			UpdateAndAddToList(list);			
			
			context.BulkMerge(list, options => options.BatchSize = 100);
			
			FiddleHelper.WriteTable(context.Customers.Take(5).ToList());	
		}
	}
	
	public static void UpdateAndAddToList(List<Customer> list)
	{
		for (int i = 0; i < 1000; i++)
		{
			list.Add(new Customer() { Name ="Customer_" + i});
		}
		
		list.ForEach(x => x.IsActive = false);
	}
		 
	public static void GenerateData()
	{
		
		using (var context = new EntityContext())
		{
			context.Customers.Add(new Customer() { Name ="Customer_A", IsActive = true });
			context.Customers.Add(new Customer() { Name ="Customer_B", IsActive = true });

			context.BulkSaveChanges();	
		}
		
		using (var context = new EntityContext())
		{
			FiddleHelper.WriteTable(context.Customers.ToList());	
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