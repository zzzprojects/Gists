// Description: EFE - Bulk-Update - More than one option
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/qfv3Ee

// @nuget: EntityFramework
// @nuget: EntityFramework.SqlServerCompact
// @nuget: Microsoft.SqlServer.Compact
// @nuget: Z.EntityFramework.Extensions

using System;
using System.Linq;
using System.Data.Entity;

public class Program
{
	public static void Main()
	{
		// Custom configuration specific for .NET Fiddle
		Z.EntityFramework.Extensions.EntityFrameworkManager.UseFiddleSqlCompact(System.Data.Entity.SqlServerCompact.SqlCeProviderServices.Instance, System.Data.SqlServerCe.SqlCeProviderFactory.Instance);

		GenerateData();

		// BulkUpdate: Specify more than one option
		using (var context = new EntityContext())
		{
			var list = context.Customers.ToList();
			
			list.First().Description = "Updated_A";
			list.First().IsActive = false;
			list.First().Name = "NotUpdated";			
			
			context.BulkUpdate(list, options => {
					options.BatchSize = 100;
					options.ColumnInputExpression = c => new {c.CustomerID, c.Description, c.IsActive};
				});
		}
		
		using (var context = new EntityContext())
		{
			FiddleHelper.WriteTable(context.Customers.ToList());	
		}
	}
		 
	public static void GenerateData()
	{
		
		using (var context = new EntityContext())
		{
			context.Customers.Add(new Customer() { Name ="Customer_A", Description = "Description", IsActive = true });

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
		public string Description { get; set; }
		public Boolean IsActive { get; set; }
	}
}