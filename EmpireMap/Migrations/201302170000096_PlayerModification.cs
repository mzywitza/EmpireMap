namespace EmpireMap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlayerModification : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Players", "AllianceStatus", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Players", "AllianceStatus", c => c.String());
        }
    }
}
