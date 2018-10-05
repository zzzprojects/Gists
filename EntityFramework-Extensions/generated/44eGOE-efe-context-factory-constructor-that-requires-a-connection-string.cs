// Description: EFE - Context-factory - Constructor that requires a connection string
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/44eGOE

// @nuget: EntityFramework
// @nuget: EntityFramework.SqlServerCompact
// @nuget: Microsoft.SqlServer.Compact
// @nuget: Z.EntityFramework.Extensions

using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Extensions;
					
public class Program
{
	public static void Main()
	{
		// Custom configuration specific for .NET Fiddle
		Z.EntityFramework.Extensions.EntityFrameworkManager.UseFiddleSqlCompact(System.Data.Entity.SqlServerCompact.SqlCeProviderServices.Instance, System.Data.SqlServerCe.SqlCeProviderFactory.Instance);

		List<Invoice> list =  new List<Invoice>() {
		new Invoice() { Description = "Invoice_A", InvoiceItems = new List<InvoiceItem>() { new InvoiceItem() { Description = "Invoice_A_InvoiceItem_A" } , new InvoiceItem() { Description = "Invoice_A_InvoiceItem_B" }}}, 
		new Invoice() { Description = "Invoice_B", InvoiceItems = new List<InvoiceItem>() { new InvoiceItem() { Description = "Invoice_B_InvoiceItem_A" } , new InvoiceItem() { Description = "Invoice_B_InvoiceItem_B" }}}};
		
		// ContextFactory: The context factory is a function `Func<DbContext, DbContext>` that provide the current DbContext as a parameter and requires to return a new DbContext.
		EntityFrameworkManager.ContextFactory = context => new EntityContext(@"Data Source=ZZZ_Projects.sdf");
		
		using (var context = new EntityContext(@"Data Source=ZZZ_Projects.sdf"))
		{
			context.BulkInsert(list, options => options.IncludeGraph = true);
		}
		
		using (var context = new EntityContext(@"Data Source=ZZZ_Projects.sdf"))
		{
			FiddleHelper.WriteTable(context.Invoices.ToList().Select(x => new { x.InvoiceID, x.Description}));
			FiddleHelper.WriteTable(context.InvoiceItems.ToList());
		}
	}

	public class EntityContext : DbContext
	{
		public EntityContext(string connection) : base(connection)
		{

		}
		
		public DbSet<Invoice> Invoices { get; set; }
		public DbSet<InvoiceItem> InvoiceItems { get; set; }
	}
	
	
	public class Invoice
	{
		public int InvoiceID { get; set; }
		public string Description { get; set; }
		public List<InvoiceItem> InvoiceItems { get; set; }
	}
	
	public class InvoiceItem
	{
		public int InvoiceItemID { get; set; }
		public string Description { get; set; }
	}
}