namespace EmpireMap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        ConfirmedBy = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        AllianceStatus = c.String(),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.PlayerId)
                .ForeignKey("dbo.UserProfile", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Castles",
                c => new
                    {
                        CastleId = c.Int(nullable: false, identity: true),
                        X = c.Long(nullable: false),
                        Y = c.Long(nullable: false),
                        Name = c.String(nullable: false),
                        IsAp = c.Boolean(nullable: false),
                        MapId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CastleId)
                .ForeignKey("dbo.Maps", t => t.MapId, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.MapId)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.Maps",
                c => new
                    {
                        MapId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MapId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Castles", new[] { "PlayerId" });
            DropIndex("dbo.Castles", new[] { "MapId" });
            DropIndex("dbo.Players", new[] { "UserId" });
            DropForeignKey("dbo.Castles", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Castles", "MapId", "dbo.Maps");
            DropForeignKey("dbo.Players", "UserId", "dbo.UserProfile");
            DropTable("dbo.Maps");
            DropTable("dbo.Castles");
            DropTable("dbo.Players");
            DropTable("dbo.UserProfile");
        }
    }
}
