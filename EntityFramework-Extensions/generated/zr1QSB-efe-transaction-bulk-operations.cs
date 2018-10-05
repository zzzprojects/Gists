// Description: EFE - Transaction - Bulk Operations
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/zr1QSB

// @nuget: EntityFramework
// @nuget: EntityFramework.SqlServerCompact
// @nuget: Microsoft.SqlServer.Compact
// @nuget: Z.EntityFramework.Extensions

using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
					
public class Program
{
	public static void Main()
	{
		// Custom configuration specific for .NET Fiddle
		Z.EntityFramework.Extensions.EntityFrameworkManager.UseFiddleSqlCompact(System.Data.Entity.SqlServerCompact.SqlCeProviderServices.Instance, System.Data.SqlServerCe.SqlCeProviderFactory.Instance);

		List<Customer> list1 =  new List<Customer>() { new Customer() { Name ="Customer_A" }, new Customer() { Name ="Customer_B" }};
		List<Customer> list2 =  new List<Customer>() { new Customer() { Name ="Customer_C" }, new Customer() { Name ="Customer_D" }};
				
		// Transaction : Bulk Operations such as BulkInsert, BulkUpdate, BulkDelete doesn't use a transaction by default. This is your responsibility to handle it.
		// If you start a transaction within Entity Framework, Bulk Operations will honor it.
		using (var context = new EntityContext())
		{
			var transaction = context.Database.BeginTransaction();
			try
			{
				context.BulkInsert(list1);
				context.BulkInsert(list2);
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
	}
}