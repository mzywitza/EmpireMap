using EmpireMap.Models;
using EmpireMap.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace EmpireMap.Controllers
{
    [InitializeSimpleMembership]
    public class LimitedController : Controller
    {
        ApplicationContext ctx = new ApplicationContext();

        public ActionResult Index()
        {
            var model = new LimitedPageModel();
            model.Player = ctx.Players.Where(p => p.UserId == WebSecurity.CurrentUserId).SingleOrDefault();
            if (model.Player != null)
                model.Castles = ctx.Castles.Where(c => c.PlayerId == model.Player.PlayerId).Include(c => c.Map).ToList();
            else
            {
                model.Player = new Player { Name = User.Identity.Name, AllianceStatus = AllianceStatus.Member, UserId = WebSecurity.CurrentUserId };
                model.Castles = new List<Castle>();
            }
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult Limited()
        {
            var confirmers = new List<string>();
            confirmers.AddRange(Roles.FindUsersInRole("Administrator", "%"));
            confirmers.AddRange(Roles.FindUsersInRole("Führung", "%"));
            var model = new LimitedHeaderModel { Confirmers = confirmers };
            return View(model);
        }

    }
}
