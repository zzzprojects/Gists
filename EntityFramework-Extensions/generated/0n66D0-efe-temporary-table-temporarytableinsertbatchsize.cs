// Description: EFE - Temporary-table - TemporaryTableInsertBatchSize
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/0n66D0

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
		
		/* Note: Temporary-table are not supported in SQL CE. The same syntax is valid for SQL Server.*/

		// TemporaryTableInsertBatchSize: Gets or sets the number of records to use in a batch when inserting in a temporary table. This number is recommended to be high.
		using (var context = new EntityContext())
		{
			context.Customers.AddRange(AddToList());
			
			
			context.BulkSaveChanges(options =>
									{
										options.TemporaryTableInsertBatchSize = 100;
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
		public Boolean IsActive { get; set; }
	}
}