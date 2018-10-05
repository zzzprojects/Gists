// Description: EFE - Bulk-SaveChanges - BulkSaveChanges
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/z73RVE

// @nuget: EntityFramework
// @nuget: EntityFramework.SqlServerCompact
// @nuget: Microsoft.SqlServer.Compact
// @nuget: Z.EntityFramework.Extensions

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
public class Program
{
	public static void Main()
	{
		// Custom configuration specific for .NET Fiddle
		Z.EntityFramework.Extensions.EntityFrameworkManager.UseFiddleSqlCompact(System.Data.Entity.SqlServerCompact.SqlCeProviderServices.Instance, System.Data.SqlServerCe.SqlCeProviderFactory.Instance);

		GenerateData();		

		// BulkSaveChanges: BulkSaveChanges works exactly like `SaveChanges` but faster.
		using (var context = new EntityContext())
		{
			var listToRemove = context.Customers.Where(x => x.IsActive == false ).ToList();
			var listToModify = context.Customers.Where(x => x.IsActive == true ).ToList();
			var listToAdd = new List<Customer>() { new Customer() { Name = "Customer_C", Description = "Description", IsActive = false}};
			
			
			context.Customers.AddRange(listToAdd); // add
			context.Customers.RemoveRange(listToRemove); // remove
			listToModify.First().Description = "Updated_A"; // modify
			
			context.BulkSaveChanges();
			
			FiddleHelper.WriteTable(context.Customers.ToList());
		}
		
		// BulkSaveChanges: Easy to customize
		using (var context = new EntityContext())
		{
			context.Customers.AddRange(GenerateListCustomer());
			
			context.BulkSaveChanges(bulk => bulk.BatchSize = 100);
			
			FiddleHelper.WriteTable(context.Customers.Take(5).ToList());
		}
	}
		 
	public static void GenerateData()
	{
		
		using (var context = new EntityContext())
		{
			context.Customers.Add(new Customer() { Name ="Customer_A", Description = "Description", IsActive = true });
			context.Customers.Add(new Customer() { Name ="Customer_B", Description = "Description", IsActive = false });
			context.BulkSaveChanges();	
		}
		
		using (var context = new EntityContext())
		{
			FiddleHelper.WriteTable(context.Customers.ToList());	
		}
	}
	
	public static List<Customer> GenerateListCustomer() 
	{
		var customers = new List<Customer>();
		
		for (int i = 0; i < 1000; i++)
		{
			customers.Add(new Customer() { Name ="Customer_" + i, Description = "Description", IsActive = true });
		}
		
		return customers;
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