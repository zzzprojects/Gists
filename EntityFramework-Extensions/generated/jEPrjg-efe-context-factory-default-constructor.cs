// Description: EFE - Context-factory - Default constructor
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/jEPrjg

// @nuget: EntityFramework
// @nuget: EntityFramework.SqlServerCompact
// @nuget: Microsoft.SqlServer.Compact
// @nuget: Z.EntityFramework.Extensions

using System.Data.Entity;
using System.Collections.Generic;
					
public class Program
{
	public static void Main()
	{
		// Custom configuration specific for .NET Fiddle
		Z.EntityFramework.Extensions.EntityFrameworkManager.UseFiddleSqlCompact(System.Data.Entity.SqlServerCompact.SqlCeProviderServices.Instance, System.Data.SqlServerCe.SqlCeProviderFactory.Instance);

		List<Customer> list =  new List<Customer>() { new Customer() { Name ="Customer_A" }, new Customer() { Name ="Customer_B" }, new Customer() { Name ="Customer_C" }};
		
		using (var context = new EntityContext())
		{
			context.BulkInsert(list);
			
			FiddleHelper.WriteTable("Insert",list);	
		}
	}

	public class EntityContext : DbContext
	{
		public EntityContext() : base(@"Data Source=ZZZ_Projects.sdf")
		{
			// I'm a default constructor!
		}
		
		public DbSet<Customer> Customers { get; set; }
	}
	
	
	public class Customer
	{
		public int CustomerID { get; set; }
		public string Name { get; set; }
	}
}