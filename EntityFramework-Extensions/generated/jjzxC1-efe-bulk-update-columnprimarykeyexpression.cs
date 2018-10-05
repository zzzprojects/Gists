// Description: EFE - Bulk-Update - ColumnPrimaryKeyExpression
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/jjzxC1

// @nuget: EntityFramework
// @nuget: EntityFramework.SqlServerCompact
// @nuget: Microsoft.SqlServer.Compact
// @nuget: Z.EntityFramework.Extensions

using System.Linq;
using System.Data.Entity;
using System.Data.SqlServerCe;
using System.Collections.Generic;

public class Program
{
	public static void Main()
	{
		// Custom configuration specific for .NET Fiddle
		Z.EntityFramework.Extensions.EntityFrameworkManager.UseFiddleSqlCompact(System.Data.Entity.SqlServerCompact.SqlCeProviderServices.Instance, System.Data.SqlServerCe.SqlCeProviderFactory.Instance);
		
		// An index is required for the key only for SQL Compact.
		CreateBDWithIndex();
		
		GenerateData();
		
		var list = new List<Customer>() { new Customer () { Name = "Customer_B", Description = "Updated_B", Login = "Login5", Password = "Password2" }};
	
		
		// BulkUpdate: Specify custom keys - Single Key
		using (var context = new EntityContext())
		{
			context.BulkUpdate(list, options => options.ColumnPrimaryKeyExpression = customer => customer.Name);
			
			
			FiddleHelper.WriteTable(context.Customers.ToList());
		}
		
		list = new List<Customer>();
		list.Add(new Customer() {Name = "2", Login = "Login5", Password = "Password2"}) ;
		list.Add(new Customer() {Name = "1", Description = "Updated_A", Login = "Login1", Password = "Password1" });
			
		// BulkUpdate: Specify custom keys - Surrogate Key
		using (var context = new EntityContext())
		{
			context.BulkUpdate(list, options => options.ColumnPrimaryKeyExpression = customer => new { customer.Login, customer.Password });
			
			
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
				command.CommandText = "CREATE UNIQUE INDEX UX_Name ON Customers(Name)";
				command.ExecuteNonQuery();
			}
			
			using (var command = connection.CreateCommand())
			{
				command.CommandText = "CREATE UNIQUE INDEX UX_Login ON Customers(Login, Password)";
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
		public string Description { get; set; }
		public string Login  { get; set; }
		public string Password { get; set; }
	}
}