// Description: EFE - Bulk-insert - Batch Size
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/JrFGIe

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
	
		// BulkInsert: specify the Batch Size
		using (var context = new EntityContext())
		{
			context.BulkInsert(Data(), options => options.BatchSize = 100);
			
			FiddleHelper.WriteTable(context.Customers.Take(5).ToList());
		}
	}
	
	public static List<Customer> Data() 
	{
		List<Customer>  list = new List<Customer>();
		
		for(int i = 0; i < 1000; i++)
		{
			list.Add(new Customer() { Name ="Customer_" + i});
		}
		
		return list;
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