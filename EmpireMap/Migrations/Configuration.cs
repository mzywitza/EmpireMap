namespace EmpireMap.Migrations
{
    using EmpireMap.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<EmpireMap.Models.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EmpireMap.Models.ApplicationContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            WebSecurity.InitializeDatabaseConnection(
                "DefaultConnection",
                "UserProfile",
                "UserId",
                "UserName",
                autoCreateTables: true);

            var roles = new string[] { "Administrator", "Führung", "Benutzer" };

            foreach (var role in roles.Where(r => !Roles.RoleExists(r)))
                Roles.CreateRole(role);

            if (!WebSecurity.UserExists("Administrator"))
                WebSecurity.CreateUserAndAccount("Administrator", "withheld",
                    new { ConfirmedBy = "Administrator" });

            if (!Roles.GetRolesForUser("Administrator").Contains("Administrator"))
                Roles.AddUserToRole("Administrator", "Administrator");

            if (!WebSecurity.UserExists("InfamousMonk"))
                WebSecurity.CreateUserAndAccount("InfamousMonk", "imnotdumb",
                    new { ConfirmedBy = "Administrator" });

            if (!Roles.GetRolesForUser("InfamousMonk").Contains("Benutzer"))
                Roles.AddUserToRole("InfamousMonk", "Benutzer");

            context.Maps.AddOrUpdate(m => m.Name,
                new Map { Name = "Grün" },
                new Map { Name = "Eis" },
                new Map { Name = "Feuer" },
                new Map { Name = "Wüste" });

            context.SaveChanges();

            var monkUserProfile = context.UserProfiles.Where(u => u.UserName == "InfamousMonk").Single();
            
            context.Players.AddOrUpdate(p => p.Name,
                new Player { Name = "InfamousMonk", AllianceStatus = "Member", User = monkUserProfile });

            context.SaveChanges();

            var green = context.Maps.Where(m => m.Name == "Grün").Single();
            var ice = context.Maps.Where(m => m.Name == "Eis").Single();
            var monk = context.Players.Where(p => p.Name == "InfamousMonk").Single();
            context.Castles.AddOrUpdate(c => c.Name,
                new Castle { Name = "Keilerbau", X = 733, Y = 732, Player = monk, Map = green },
                new Castle { Name = "Frischlingswald", X = 729, Y = 729, IsAp = true, Player = monk, Map = green },
                new Castle { Name = "Specks Versteck", X = 730, Y = 736, IsAp = true, Player = monk, Map = green },
                new Castle { Name = "Eisbeinhöhle", X = 413, Y = 1028, IsAp = true, Player = monk, Map = ice });
        }
    }
}
