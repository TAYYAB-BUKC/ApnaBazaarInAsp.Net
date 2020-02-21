namespace ApnaBazaar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductIDAddedToReview : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "Product_ID", "dbo.Products");
            DropIndex("dbo.Reviews", new[] { "Product_ID" });
            RenameColumn(table: "dbo.Reviews", name: "Product_ID", newName: "ProductId");
            AlterColumn("dbo.Reviews", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reviews", "ProductId");
            AddForeignKey("dbo.Reviews", "ProductId", "dbo.Products", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "ProductId", "dbo.Products");
            DropIndex("dbo.Reviews", new[] { "ProductId" });
            AlterColumn("dbo.Reviews", "ProductId", c => c.Int());
            RenameColumn(table: "dbo.Reviews", name: "ProductId", newName: "Product_ID");
            CreateIndex("dbo.Reviews", "Product_ID");
            AddForeignKey("dbo.Reviews", "Product_ID", "dbo.Products", "ID");
        }
    }
}
