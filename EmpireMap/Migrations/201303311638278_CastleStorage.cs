namespace EmpireMap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CastleStorage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Castles", "Storage", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Castles", "Storage");
        }
    }
}
