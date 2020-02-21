namespace ApnaBazaar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Comment = c.String(),
                        Rating = c.Single(nullable: false),
                        Product_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.Product_ID)
                .Index(t => t.Product_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "Product_ID", "dbo.Products");
            DropIndex("dbo.Reviews", new[] { "Product_ID" });
            DropTable("dbo.Reviews");
        }
    }
}
