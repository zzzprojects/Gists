// Description: EFE - Transaction - BulkSaveChanges
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/Igr6zU

// @nuget: EntityFramework
// @nuget: EntityFramework.SqlServerCompact
// @nuget: Microsoft.SqlServer.Compact
// @nuget: Z.EntityFramework.Extensions

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
public class Program
{
	public static void Main()
	{
		// Custom configuration specific for .NET Fiddle
		Z.EntityFramework.Extensions.EntityFrameworkManager.UseFiddleSqlCompact(System.Data.Entity.SqlServerCompact.SqlCeProviderServices.Instance, System.Data.SqlServerCe.SqlCeProviderFactory.Instance);	

		// Transaction : As SaveChanges, BulkSaveChanges already save all entities within an internal transaction. So by default, there is nothing to do.
 		// However, if you start a transaction within Entity Framework, BulkSaveChanges will honor it and will use this transaction instead of creating an internal transaction.
		using (var context = new EntityContext())
		{
			var list = new List<Customer>() { new Customer() { Name = "Customer_C", Description = "Description", IsActive = false}};
			
			
			context.Customers.AddRange(list); 
			
			var transaction = context.Database.BeginTransaction();
			try
			{
				context.BulkSaveChanges();
				transaction.Commit();
			}
			catch
			{
				transaction.Rollback();
			}
			
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
		public string Description { get; set; }
		public Boolean IsActive { get; set; }
	}
}