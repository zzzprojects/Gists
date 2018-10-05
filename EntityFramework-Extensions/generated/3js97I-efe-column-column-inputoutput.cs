// Description: EFE - Column - Column InputOutput
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/3js97I

// @nuget: EntityFramework
// @nuget: EntityFramework.SqlServerCompact
// @nuget: Microsoft.SqlServer.Compact
// @nuget: Z.EntityFramework.Extensions

using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
					
public class Program
{
	public static void Main()
	{
		// Custom configuration specific for .NET Fiddle
		Z.EntityFramework.Extensions.EntityFrameworkManager.UseFiddleSqlCompact(System.Data.Entity.SqlServerCompact.SqlCeProviderServices.Instance, System.Data.SqlServerCe.SqlCeProviderFactory.Instance);

		List<Customer> list =  new List<Customer>() { new Customer() { Name ="Customer_A", Description = "Description" , IsActive = false }, 
		new Customer()  { Name ="Customer_B", Description = "Description", IsActive = true },
		new Customer() { Name ="Customer_C", Description = "Description" , IsActive = true }};
		
		
		// Column InputOutput: Specify custom columns to 
		using (var context = new EntityContext())
		{
			context.BulkInsert(list, options => options.ColumnInputOutputExpression = c => new { c.CustomerID ,c.Name, c.IsActive });
			
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