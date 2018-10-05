// Description: EFE - Include-graph - IncludeGraphOperationBuilder
// Website: https://entityframework-extensions.net/
// Run: https://dotnetfiddle.net/0uW3tw

// @nuget: EntityFramework
// @nuget: EntityFramework.SqlServerCompact
// @nuget: Microsoft.SqlServer.Compact
// @nuget: Z.EntityFramework.Extensions

using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using Z.BulkOperations;
using System.Data.SqlServerCe;
using System;
					
public class Program
{
	public static void Main()
	{
		// Custom configuration specific for .NET Fiddle
		Z.EntityFramework.Extensions.EntityFrameworkManager.UseFiddleSqlCompact(System.Data.Entity.SqlServerCompact.SqlCeProviderServices.Instance, System.Data.SqlServerCe.SqlCeProviderFactory.Instance);
		
		// An index is required for the key only for SQL Compact.
		CreateBDWithIndex();

		List<Guid> listKey = new List<Guid>() { Guid.NewGuid(),Guid.NewGuid() };
		
		GenerateData(listKey);
		
		List<Invoice> invoices =  new List<Invoice>() {
		new Invoice() { Description = "Updated_A", ColumGuid = listKey.ElementAt(0), InvoiceItems = new List<InvoiceItem>() { new InvoiceItem() { Description = "Updated_A_InvoiceItem_A", ColumGuid = listKey.ElementAt(0) }}}, 
		new Invoice() { Description = "Updated_B", ColumGuid = listKey.ElementAt(1), InvoiceItems = new List<InvoiceItem>() { new InvoiceItem() { Description = "Updated_B_InvoiceItem_A", ColumGuid = listKey.ElementAt(1) }}}};
		
		
		invoices.Add(new Invoice() { Description = "Invoice_C", ColumGuid = Guid.NewGuid(), InvoiceItems = new List<InvoiceItem>() { new InvoiceItem() { Description = "Invoice_C_InvoiceItem_A", ColumGuid = Guid.NewGuid() } , new InvoiceItem() { Description = "Invoice_C_InvoiceItem_B" , ColumGuid = Guid.NewGuid()}}});
		
		// IncludeGraphOperationBuilder: The IncludeGraphOperationBuilder let you customize the bulk operations by entity type. 
		using (var context = new EntityContext())
		{
			context.BulkMerge(invoices, options =>
						  {
							  options.IncludeGraph = true;
							  options.IncludeGraphOperationBuilder = operation =>
							  {
								  if (operation is BulkOperation<Invoice>)
								  {
									  var bulk = (BulkOperation<Invoice>) operation;
									  bulk.ColumnPrimaryKeyExpression = x => x.ColumGuid ;
								  }
								  else if (operation is BulkOperation<InvoiceItem>)
								  {
									  var bulk = (BulkOperation<InvoiceItem>) operation;
									  bulk.ColumnPrimaryKeyExpression = x => x.ColumGuid;
								  }
							  };
						  });
		}
		
		
		using (var context = new EntityContext())
		{
			FiddleHelper.WriteTable(context.Invoices.ToList());
			FiddleHelper.WriteTable(context.InvoiceItems.ToList());
		}
	}

	public static void GenerateData(List<Guid> listKey)
	{
		List<Invoice> list =  new List<Invoice>() {
		new Invoice() { Description = "Invoice_A", ColumGuid = listKey.ElementAt(0), InvoiceItems = new List<InvoiceItem>() { new InvoiceItem() { Description = "Invoice_A_InvoiceItem_A", ColumGuid = listKey.ElementAt(0) } , new InvoiceItem() { Description = "Invoice_A_InvoiceItem_B", ColumGuid = Guid.NewGuid()  }}}, 
		new Invoice() { Description = "Invoice_B", ColumGuid = listKey.ElementAt(1), InvoiceItems = new List<InvoiceItem>() { new InvoiceItem() { Description = "Invoice_B_InvoiceItem_A", ColumGuid = listKey.ElementAt(1) } , new InvoiceItem() { Description = "Invoice_B_InvoiceItem_B" , ColumGuid = Guid.NewGuid() }}}};
		
		
		// BulkInsert: INCLUDE child entities
		using (var context = new EntityContext())
		{
			context.BulkInsert(list, options => options.IncludeGraph = true);
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
				command.CommandText = "CREATE UNIQUE INDEX UX_Invoice_Key ON Invoices(ColumGuid)";
				command.ExecuteNonQuery();
			}
			
			using (var command = connection.CreateCommand())
			{
				command.CommandText = "CREATE UNIQUE INDEX UX_InvoiceItem_Key ON InvoiceItems(ColumGuid)";
				command.ExecuteNonQuery();
			}
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
		public Guid ColumGuid { get; set; }
		public string Description { get; set; }
		public List<InvoiceItem> InvoiceItems { get; set; }
	}
	
	public class InvoiceItem
	{
		public int InvoiceItemID { get; set; }
		public Guid ColumGuid { get; set; }
		public string Description { get; set; }
	}
}