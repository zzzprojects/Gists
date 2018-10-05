// Description: EFE - DeleteFromQuery - DeleteFromQuery
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/0my0fe

// @nuget: EntityFramework
// @nuget: EntityFramework.SqlServerCompact
// @nuget: Microsoft.SqlServer.Compact
// @nuget: Z.EntityFramework.Extensions

using System;
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
		
		// DeleteFromQuery: DELETE all rows from the database using a LINQ Query without loading entities in the context
		using (var context = new EntityContext())
		{
			// DELETE all customers that are inactive
			context.Customers.Where(x => !x.IsActive).DeleteFromQuery();
			
			FiddleHelper.WriteTable(context.Customers.ToList());	
		}
		
		// DeleteFromQuery: DELETE all rows from the database using a LINQ Query without loading entities in the context
		using (var context = new EntityContext())
		{
			// DELETE customers by Name
			context.Customers.Where(x => x.Name =="Customer_B").DeleteFromQuery();
			
			
			FiddleHelper.WriteTable(context.Customers.ToList());
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
	
		
	public static void CreateBDWithIndex()
	{
		using (var context = new EntityContext())
		{
			context.Database.CreateIfNotExists();
			
		}
		using (var connection = new SqlCeConnection(@"Data Source=ZZZ_Projects.sdf"))
		{
			connection.Open();
			using (var command = connection.CreateCommand())
			{
				command.CommandText = "CREATE UNIQUE INDEX UX_Name ON Customers (Name)";
				command.ExecuteNonQuery();
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
		public Boolean IsActive { get; set; }
	}
}