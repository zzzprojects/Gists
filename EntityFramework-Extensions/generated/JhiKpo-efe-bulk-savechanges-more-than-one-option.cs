// Description: EFE - Bulk-SaveChanges - More than one option
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/JhiKpo

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
		

		// BulkMerge: Specify more than one option
		using (var context = new EntityContext())
		{
			var list = AddToList();
			context.Customers.AddRange(list);
			
			
			context.BulkSaveChanges( options => {
					options.BatchSize = 100;
					options.BulkOperationExecuting = bulkOperation => {  
						list.ForEach(x =>  { x.Description = "Before_Execution_Description"; x.IsActive = false;});
					};
				});
		}
		
		using (var context = new EntityContext())
		{
			FiddleHelper.WriteTable(context.Customers.Take(5).ToList());	
		}
	}
		 	
	public static List<Customer> AddToList()
	{
		var list = new List<Customer>();
		for (int i = 0; i < 1000; i++)
		{
			list.Add(new Customer() { Name ="Customer_" + i});
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
		public string Description { get; set; }
		public Boolean IsActive { get; set; }
	}
}