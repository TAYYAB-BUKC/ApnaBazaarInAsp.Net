namespace ApnaBazaar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WishlistAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Wishlists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        ProductId = c.Int(nullable: false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wishlists", "ProductId", "dbo.Products");
            DropIndex("dbo.Wishlists", new[] { "ProductId" });
            DropTable("dbo.Wishlists");
        }
    }
}
