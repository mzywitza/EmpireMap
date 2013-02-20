using EmpireMap.Migrations;
using EmpireMap.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace EmpireMap
{
    public class EntityInitializer
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationContext, Configuration>());
            
            // Starting Context to create tables for Membership Initializer
            // Will be no-op with MigrateDatabaseToLatestVersion, but will initialize Membership with other Initializers
            using (var ctx = new ApplicationContext())
            {
                ctx.Database.Initialize(false);
                if (!WebSecurity.Initialized)
                    WebSecurity.InitializeDatabaseConnection(
                        "DefaultConnection",
                        "UserProfile",
                        "UserId",
                        "UserName",
                        autoCreateTables: true);
            }
        }
    }
}