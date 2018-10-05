// Description: EFE - Bulk-Merge - Ignore-on-merge
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/J4Utxh

// @nuget: EntityFramework
// @nuget: EntityFramework.SqlServerCompact
// @nuget: Microsoft.SqlServer.Compact
// @nuget: Z.EntityFramework.Extensions

using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

public class Program
{
	public static void Main()
	{
		// Custom configuration specific for .NET Fiddle
		Z.EntityFramework.Extensions.EntityFrameworkManager.UseFiddleSqlCompact(System.Data.Entity.SqlServerCompact.SqlCeProviderServices.Instance, System.Data.SqlServerCe.SqlCeProviderFactory.Instance);
				
		GenerateData();
		
		// BulkMerge: specify custom columns to exclude on insert or update
		using (var context = new EntityContext())
		{
			var list = context.Customers.ToList();
			list.ForEach( x => { x.Name = "*****"; x.Description = "Updated"; x.Login = "****"; x.Password = "****"; });
			list.Add(new Customer() {Name = "Customer_C", Description = "Updated", Login = "Login3", Password = "Password3"});
			
			context.BulkMerge(list, options =>
			{
				options.IgnoreOnMergeInsertExpression = customer =>  customer.Description;
				options.IgnoreOnMergeUpdateExpression = customer =>  new { customer.Name, customer.Login, customer.Password };
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
			context.Customers.Add(new Customer() { Name ="Customer_A", Description = "Description", Login = "Login1", Password = "Password1" });
			context.Customers.Add(new Customer() { Name ="Customer_B", Description = "Description" , Login = "Login2", Password = "Password2" });
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
		public string Login  { get; set; }
		public string Password { get; set; }
	}
}