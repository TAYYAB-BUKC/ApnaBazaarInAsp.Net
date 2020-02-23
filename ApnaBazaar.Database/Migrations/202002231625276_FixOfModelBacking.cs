namespace ApnaBazaar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixOfModelBacking : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Wishlists", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Wishlists", "Quantity", c => c.Int(nullable: false));
        }
    }
}
