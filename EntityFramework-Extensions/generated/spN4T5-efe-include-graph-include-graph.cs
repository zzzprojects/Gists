// Description: EFE - Include-graph - Include Graph
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/spN4T5

// @nuget: EntityFramework
// @nuget: EntityFramework.SqlServerCompact
// @nuget: Microsoft.SqlServer.Compact
// @nuget: Z.EntityFramework.Extensions

using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
					
public class Program
{
	public static void Main()
	{
		// Custom configuration specific for .NET Fiddle
		Z.EntityFramework.Extensions.EntityFrameworkManager.UseFiddleSqlCompact(System.Data.Entity.SqlServerCompact.SqlCeProviderServices.Instance, System.Data.SqlServerCe.SqlCeProviderFactory.Instance);

		List<Invoice> list =  new List<Invoice>() {
		new Invoice() { Description = "Invoice_A", InvoiceItems = new List<InvoiceItem>() { new InvoiceItem() { Description = "Invoice_A_InvoiceItem_A" } , new InvoiceItem() { Description = "Invoice_A_InvoiceItem_B" }}}, 
		new Invoice() { Description = "Invoice_B", InvoiceItems = new List<InvoiceItem>() { new InvoiceItem() { Description = "Invoice_B_InvoiceItem_A" } , new InvoiceItem() { Description = "Invoice_B_InvoiceItem_B" }}}};
		
		
		// IncludeGraph: The IncludeGraph option allows to INSERT/UPDATE/MERGE entities by including the child entities graph.
		using (var context = new EntityContext())
		{
			context.BulkInsert(list, options => options.IncludeGraph = true);
		}
		
		using (var context = new EntityContext())
		{
			FiddleHelper.WriteTable(context.Invoices.ToList().Select(x => new { x.InvoiceID, x.Description}));
			FiddleHelper.WriteTable(context.InvoiceItems.ToList());
		}
	}

	public class EntityContext : DbContext
	{
		public EntityContext() : base(@"Data Source=ZZZ_Projects.sdf")
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