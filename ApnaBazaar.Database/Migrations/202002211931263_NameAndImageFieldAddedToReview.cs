namespace ApnaBazaar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NameAndImageFieldAddedToReview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "Name", c => c.String());
            AddColumn("dbo.Reviews", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "Image");
            DropColumn("dbo.Reviews", "Name");
        }
    }
}
