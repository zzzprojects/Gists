// Description: EFE - Audit - AuditEntries
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/WwQ7oZ

// @nuget: EntityFramework
// @nuget: EntityFramework.SqlServerCompact
// @nuget: Microsoft.SqlServer.Compact
// @nuget: Z.EntityFramework.Extensions

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Z.BulkOperations;

public class Program
{
	public static void Main()
	{
		// Custom configuration specific for .NET Fiddle
		Z.EntityFramework.Extensions.EntityFrameworkManager.UseFiddleSqlCompact(System.Data.Entity.SqlServerCompact.SqlCeProviderServices.Instance, System.Data.SqlServerCe.SqlCeProviderFactory.Instance);

		GenerateData();		
		
		// AuditEntries: Gets INSERTED and DELETED data when UseAudit option is enabled.
		using (var context = new EntityContext())
		{
			var listToRemove = context.Customers.Where(x => x.IsActive == false ).ToList();
			var listToModify = context.Customers.Where(x => x.IsActive == true ).ToList();
			var listToAdd = new List<Customer>() { new Customer() { Name = "Customer_C", Description = "Description", IsActive = false}};
			
			
			context.Customers.AddRange(listToAdd); // add
			context.Customers.RemoveRange(listToRemove); // remove
			listToModify.First().Description = "Updated_A"; // modify
			
			List<AuditEntry> auditEntries = new List<AuditEntry>();
			
			context.BulkSaveChanges(options =>
			{
				options.UseAudit = true;
				options.BulkOperationExecuted = bulkOperation => auditEntries.AddRange(bulkOperation.AuditEntries);
			});
			
			 
			
			foreach (var entry in auditEntries)
			{
				foreach (var value in entry.Values)
				{
					var oldValue = value.OldValue;
					var newValue = value.NewValue;
				}
				
				FiddleHelper.WriteTable(entry.Values);
			}
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