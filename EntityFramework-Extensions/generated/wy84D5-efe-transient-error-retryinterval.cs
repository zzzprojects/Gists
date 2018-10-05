// Description: EFE - Transient-error - RetryInterval
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/wy84D5

// @nuget: EntityFramework
// @nuget: EntityFramework.SqlServerCompact
// @nuget: Microsoft.SqlServer.Compact
// @nuget: Z.EntityFramework.Extensions

using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;

public class Program
{
	public static void Main()
	{
		// Custom configuration specific for .NET Fiddle
		Z.EntityFramework.Extensions.EntityFrameworkManager.UseFiddleSqlCompact(System.Data.Entity.SqlServerCompact.SqlCeProviderServices.Instance, System.Data.SqlServerCe.SqlCeProviderFactory.Instance);	
		
		// RetryInterval : Gets or sets the interval to wait before retrying an operation when a transient error occurs.
		using (var context = new EntityContext())
		{
			var list = new List<Customer>() { new Customer() { Name = "Customer_A"}};
			
			
			context.Customers.AddRange(list); 
			
			
			context.BulkSaveChanges(options => {
				options.RetryCount = 3;
				options.RetryInterval = new TimeSpan(100);
			});
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
	}
}