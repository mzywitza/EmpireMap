using EmpireMap.Filters;
using EmpireMap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace EmpireMap.Controllers
{
    [Authorize(Roles="Administrator,Führung")]
    public class UserAdminController : Controller
    {
        ApplicationContext ctx = new ApplicationContext();
        //
        // GET: /UserAdmin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Link()
        {
            var uncomfirmed = ctx.UserProfiles.Count(u => string.IsNullOrEmpty(u.ConfirmedBy)); 
            return PartialView(uncomfirmed);
        }

        public ActionResult UnconfirmedUsers()
        {
            var users = (from user in ctx.UserProfiles
                         from player in ctx.Players
                         where player.UserId == user.UserId && user.ConfirmedBy == null
                         select new UnconfirmedUserModel
                         {
                             UserId = user.UserId,
                             UserName = user.UserName,
                             PlayerId = player.PlayerId,
                             PlayerName = player.Name,
                             Castles = ctx.Castles.Count(castle => castle.PlayerId == player.PlayerId)
                         }).ToList();

            return PartialView(users);
        }

        [HttpPost]
        public ActionResult Confirm(UnconfirmedUserModel[] users)
        {
            foreach (var user in users)
            {
                var profile = ctx.UserProfiles.Find(user.UserId);

                if (user.Confirm)
                {
                    Roles.AddUserToRole(profile.UserName, "Benutzer");
                    if (user.Leadership)
                        Roles.AddUserToRole(profile.UserName, "Führung");
                    profile.ConfirmedBy = WebSecurity.CurrentUserName;
                }
            }
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
