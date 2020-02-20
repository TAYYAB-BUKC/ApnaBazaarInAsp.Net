namespace ApnaBazaar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfigurationEntityNameChanged : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Configurations", newName: "ApnaBazaarConfigurations");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ApnaBazaarConfigurations", newName: "Configurations");
        }
    }
}
