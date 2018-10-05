// Description: EFE - Bulk-Merge - BulkMerge
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/FsJKnV

// @nuget: EntityFramework
// @nuget: EntityFramework.SqlServerCompact
// @nuget: Microsoft.SqlServer.Compact
// @nuget: Z.EntityFramework.Extensions

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.SqlServerCe;

public class Program
{
	public static void Main()
	{
		// Custom configuration specific for .NET Fiddle
		Z.EntityFramework.Extensions.EntityFrameworkManager.UseFiddleSqlCompact(System.Data.Entity.SqlServerCompact.SqlCeProviderServices.Instance, System.Data.SqlServerCe.SqlCeProviderFactory.Instance);

		// An index is required for the key only for SQL Compact.
		CreateBDWithIndex();
		
		GenerateData();
		

		// BulkMerge: MERGE all entities from the database.
		using (var context = new EntityContext())
		{
			var list = context.Customers.ToList();
			
			list.Add(new Customer() { Name ="Customer_C", Description = "Description"});
			
			list.First().Description = "Updated_A";
			
			context.BulkMerge(list);
			
			FiddleHelper.WriteTable(context.Customers.ToList());	
		}
		
		
		var customers = new List<Customer>();
		customers.Add(new Customer() { Name = "Customer_B", Description = "Updated_B", IsActive = false});
		customers.Add(new Customer() { Name = "Customer_D", Description = "Description", IsActive = false});
		
		
		// BulkMerge: Easy to customize
		using (var context = new EntityContext())
		{
			context.BulkMerge(customers, options => options.ColumnPrimaryKeyExpression = customer => customer.Name);
			
			FiddleHelper.WriteTable(context.Customers.ToList());
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
	
		
	public static void CreateBDWithIndex()
	{
		using (var context = new EntityContext())
		{
			context.Database.CreateIfNotExists();
			
		}
		using (var connection = new SqlCeConnection(@"Data Source=ZZZ_Projects.sdf"))
		{
			connection.Open();
			using (var commande = connection.CreateCommand())
			{
				commande.CommandText = "CREATE UNIQUE INDEX UX_Name ON Customers(Name)";
				commande.ExecuteNonQuery();
			}
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