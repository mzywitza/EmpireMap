namespace EmpireMap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResourceDelivery : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Troups", "Wood", c => c.Int(nullable: false));
            AddColumn("dbo.Troups", "Stone", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Troups", "Stone");
            DropColumn("dbo.Troups", "Wood");
        }
    }
}
