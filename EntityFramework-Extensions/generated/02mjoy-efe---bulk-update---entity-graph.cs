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

		GenerateData();
		
		List<Invoice> invoices;
		
		using (var context = new EntityContext())
		{
			invoices = context.Invoices.Include(x => x.InvoiceItems).ToList();
		}
		
		invoices.ForEach(x => {x.Description = x.Description.Replace("Invoice", "Updated"); x.InvoiceItems.ForEach(y => y.Description = y.Description.Replace("Invoice", "Updated"));});
		
		// BulkUpdate: INCLUDE child entities
		using (var context = new EntityContext())
		{
			context.BulkUpdate(invoices, options => options.IncludeGraph = true);
		}
		
		
		using (var context = new EntityContext())
		{
			FiddleHelper.WriteTable(context.Invoices.ToList().Select(x => new { x.InvoiceID, x.Description}));
			FiddleHelper.WriteTable(context.InvoiceItems.ToList());
		}
	}

	public static void GenerateData()
	{
		List<Invoice> list =  new List<Invoice>() {
		new Invoice() { Description = "Invoice_A", InvoiceItems = new List<InvoiceItem>() { new InvoiceItem() { Description = "Invoice_A_InvoiceItem_A" } , new InvoiceItem() { Description = "Invoice_A_InvoiceItem_B" }}}, 
		new Invoice() { Description = "Invoice_B", InvoiceItems = new List<InvoiceItem>() { new InvoiceItem() { Description = "Invoice_B_InvoiceItem_A" } , new InvoiceItem() { Description = "Invoice_B_InvoiceItem_B" }}}};
		
		
		// BulkInsert: INCLUDE child entities
		using (var context = new EntityContext())
		{
			context.BulkInsert(list, options => options.IncludeGraph = true);
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