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
            var users = (from user in ctx.UserProfiles
                         join player in ctx.Players on user.UserId equals player.UserId into players
                         from player in players.DefaultIfEmpty()
                         orderby player.Name 
                         orderby user.UserName
                         select new UserAdminIndexModel
                         {
                             UserName = user.UserName,
                             UserId = user.UserId,
                             PlayerName = player.Name
                         }).ToList();
            foreach (var user in users)
            {
                user.Leadership = Roles.IsUserInRole(user.UserName, "Führung");
            }
            return View(users);
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

        public ActionResult EditUser(int id)
        {
            var user = ctx.UserProfiles.Find(id);
            if (user == null) return HttpNotFound("User nicht vorhanden");

            var model = new UserAdminIndexModel { UserId = user.UserId, UserName = user.UserName, ConfirmedBy=user.ConfirmedBy };
            var player = ctx.Players.SingleOrDefault(p => p.UserId == model.UserId);

            if (player != null) 
                model.PlayerName = player.Name;

            if (Roles.IsUserInRole(user.UserName, "Führung")) 
                model.Leadership = true;
              
            return View(model);
        }

        [HttpPost]
        public ActionResult EditUser(int id, UserAdminIndexModel model)
        {
            var user = ctx.UserProfiles.Find(id);
            if (user == null) return HttpNotFound("User nicht vorhanden");

            if (model.Leadership)
            {
                if (!Roles.IsUserInRole(user.UserName, "Führung"))
                    Roles.AddUserToRole(user.UserName, "Führung");
            }
            else
            {
                if (Roles.IsUserInRole(user.UserName, "Führung"))
                    Roles.RemoveUserFromRole(user.UserName, "Führung");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            var user = ctx.UserProfiles.Find(id);
            if (user == null) return HttpNotFound("User nicht vorhanden");

            var player = ctx.Players.SingleOrDefault(p => p.UserId == user.UserId);
            if (player != null)
            {
                foreach (var castle in player.Castles.ToList())
                    ctx.Castles.Remove(castle);
                ctx.Players.Remove(player);
            }
            
            ctx.SaveChanges();
            var roles = new[] { "Administrator", "Führung", "Benutzer" };
            foreach (var role in roles)
            {
                if (Roles.IsUserInRole(user.UserName, role)) Roles.RemoveUserFromRole(user.UserName, role);
            }

            Membership.DeleteUser(user.UserName,true);
            return RedirectToAction("Index");
        }
    }
}
