// Description: EFE - Bulk-delete - More than one option
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/chMKYc

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
		

		// BulkDelete: Specify more than one option.
		using (var context = new EntityContext())
		{
			context.BulkDelete(context.Customers.ToList(), options => {
				options.BatchSize = 100;
				options.RetryCount = 3;
			});
			
			Console.WriteLine("Rows after Delete : " +  context.Customers.ToList().Count());	
		}
	}
		 
	public static void GenerateData()
	{
		
		using (var context = new EntityContext())
		{
			context.Customers.Add(new Customer() { Name ="Customer_A", IsActive = false  });
			context.Customers.Add(new Customer() { Name ="Customer_B", IsActive = true });
			context.Customers.Add(new Customer() { Name ="Customer_C", IsActive = false });

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
		public Boolean IsActive { get; set; }
	}
}