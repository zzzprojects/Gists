// Description: EFE - Bulk-Merge - ColumnInputExpression
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/ei5SRo

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
		

		// BulkMerge: Specify custom columns to Merge
		using (var context = new EntityContext())
		{
			var list = context.Customers.ToList();
			
			list.Add(new Customer() { Name ="Customer_C", Description = "Description_C"});
			list.ForEach(x => { x.IsActive = false; x.Name += "_NotMerge"; x.Description += "_Merge"; });
			
			context.BulkMerge(list, options => options.ColumnInputExpression = c => new {c.CustomerID, c.Description, c.IsActive});	
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
			context.Customers.Add(new Customer() { Name ="Customer_A", Description = "Description", IsActive = true });
			context.Customers.Add(new Customer() { Name ="Customer_B", Description = "Description", IsActive = true });

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