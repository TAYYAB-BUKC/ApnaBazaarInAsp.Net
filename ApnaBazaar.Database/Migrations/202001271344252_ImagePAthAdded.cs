namespace ApnaBazaar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImagePAthAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Imagepath", c => c.String());
            AddColumn("dbo.Products", "Imagepath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Imagepath");
            DropColumn("dbo.Categories", "Imagepath");
        }
    }
}
