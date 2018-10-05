// Description: EFE - Execute-event - BulkOperationExecuting
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/mIhWyT

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
		
		var list = new List<Customer>() { new Customer()	{ Name ="Customer_A"}};

		// BulkOperationExecuting: Gets or sets an action to execute `before` the bulk operation is executed.
		using (var context = new EntityContext())
		{
			context.Customers.AddRange(list);
			
			
			context.BulkSaveChanges(options => {
				options.BulkOperationExecuting = bulkOperation => {	list.ForEach(x =>  { x.Description = "Before_Execution_Description"; x.IsActive = false;});};
			});
		}
		
		using (var context = new EntityContext())
		{
			FiddleHelper.WriteTable("Table",context.Customers.ToList());	
			FiddleHelper.WriteTable("List",list);	
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