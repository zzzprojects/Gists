// Description: EFE - Index - Batch Operations Examples
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/D2qwGj

// @nuget: EntityFramework
// @nuget: EntityFramework.SqlServerCompact
// @nuget: Microsoft.SqlServer.Compact
// @nuget: Z.EntityFramework.Extensions

using System;
using System.Data.Entity;
using System.Linq;
					
public class Program
{
	public static void Main()
	{
		// Custom configuration specific for .NET Fiddle
		Z.EntityFramework.Extensions.EntityFrameworkManager.UseFiddleSqlCompact(System.Data.Entity.SqlServerCompact.SqlCeProviderServices.Instance, System.Data.SqlServerCe.SqlCeProviderFactory.Instance);

		GenerateData();
		
		// DeleteFromQuery: DELETE all rows from the database without loading entities in the context.
		using (var context = new EntityContext())
		{
			context.Customers.Where(x => !x.IsActive)
							 .DeleteFromQuery();
			
			FiddleHelper.WriteTable(context.Customers.ToList());	
		}
			
		// UpdateFromQuery: UPDATE all rows from the database without loading entities in the context.
		using (var context = new EntityContext())
		{
			context.Customers.Where(x => x.IsActive && !x.Name.Contains("Customer_C"))
							 .UpdateFromQuery(x => new Customer { IsActive = false, Name = "Updated_B" });
			
			FiddleHelper.WriteTable(context.Customers.ToList());	
		} 
	}
	
	public static void GenerateData()
	{
		using (var context = new EntityContext())
		{
			context.Customers.Add(new Customer() { Name ="Customer_A" , IsActive = false });
			context.Customers.Add(new Customer() { Name ="Customer_B" , IsActive = true });
			context.Customers.Add(new Customer() { Name ="Customer_C" , IsActive = true });

			context.BulkSaveChanges();

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