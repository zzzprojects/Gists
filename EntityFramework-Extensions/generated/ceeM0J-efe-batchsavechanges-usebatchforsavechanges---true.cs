// Description: EFE - BatchSaveChanges - UseBatchForSaveChanges = true
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/ceeM0J

// @nuget: Microsoft.SqlServer.Compact
// @nuget: Z.EntityFramework.Classic
// @nuget: Z.EntityFramework.Classic.SqlServerCompact

using System;
using System.Linq;
using System.Data.Entity;

public class Program
{
	public static void Main()
	{
		// Custom configuration specific for .NET Fiddle
		Z.EntityFramework.Classic.EntityFrameworkManager.UseFiddleSqlCompact(System.Data.Entity.SqlServerCompact.SqlCeProviderServices.Instance, System.Data.SqlServerCe.SqlCeProviderFactory.Instance);

		using (var context = new EntityContext())
		{
			context.Customers.Add(new Customer() { Name ="Customer_A", Description = "Description", IsActive = true, LastLogin = new DateTime(2010,1,1) });
			context.Customers.Add(new Customer() { Name ="Customer_B", Description = "Description", IsActive = true, LastLogin = new DateTime(2010,1,1) });			
			context.Customers.Add(new Customer() { Name ="Customer_C", Description = "Description", IsActive = true, LastLogin = DateTime.Now });
			
			// The SaveChanges will automatically use BeachSaveChanges because the features have been forced in the constructor.
			context.SaveChanges();	
		}

		using (var context = new EntityContext())
		{
			var list = context.Customers.ToList();
			
			FiddleHelper.WriteTable("Customers", list);			
		}
	}

	public class EntityContext : DbContext
	{
		public EntityContext() : base(@"Data Source=ZZZ_Projects.sdf")
		{
			// Force to BatchSaveChanges instead of SaveChanges
			this.Configuration.BatchSaveChanges.UseBatchForSaveChanges = true;
		}
		
		public DbSet<Customer> Customers { get; set; }
	}
	
	
	public class Customer
	{
		public int CustomerID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime LastLogin { get; set; }
		public Boolean IsActive { get; set; }
	}
}