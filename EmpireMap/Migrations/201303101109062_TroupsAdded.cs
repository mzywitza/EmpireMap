namespace EmpireMap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TroupsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Troups",
                c => new
                    {
                        TroupId = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        MapId = c.Int(nullable: false),
                        TroupCount = c.Int(nullable: false),
                        IsDeff = c.Boolean(nullable: false),
                        IsVeteran = c.Boolean(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TroupId)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Maps", t => t.MapId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.MapId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Troups", new[] { "MapId" });
            DropIndex("dbo.Troups", new[] { "PlayerId" });
            DropForeignKey("dbo.Troups", "MapId", "dbo.Maps");
            DropForeignKey("dbo.Troups", "PlayerId", "dbo.Players");
            DropTable("dbo.Troups");
        }
    }
}
