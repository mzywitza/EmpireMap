using EmpireMap.Migrations;
using EmpireMap.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace EmpireMap
{
    public class MemberShipInitializer
    {
        public static void Initialize()
        {
            Database.SetInitializer<ApplicationContext>(null);

            try
            {
                using (var context = new ApplicationContext())
                {
                    if (!context.Database.Exists())
                    {
                        // Create the SimpleMembership database without Entity Framework migration schema
                        ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                    }
                }

                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationContext, Configuration>());
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
            }
        }
    }
}