using EmpireMap.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace EmpireMap.Controllers
{
    [Authorize]
    public class PlayerController : Controller
    {
        private ApplicationContext ctx = new ApplicationContext();

        public ActionResult Index()
        {
            var model = new PlayerIndexModel();
            model.Player = ctx.Players.Where(p => p.UserId == WebSecurity.CurrentUserId).SingleOrDefault();
            if (model.Player != null)
                model.Castles = ctx.Castles.Where(c => c.PlayerId == model.Player.PlayerId).Include(c => c.Map).ToList();
            else
            {
                model.Player = new Player { Name = User.Identity.Name, AllianceStatus = AllianceStatus.Member, UserId = WebSecurity.CurrentUserId };
                model.Castles = new List<Castle>();
            }
            if (TempData.ContainsKey("Message"))
                ViewBag.Message = TempData["Message"];
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult Limited()
        {
            var confirmers = new List<string>();
            confirmers.AddRange(Roles.FindUsersInRole("Administrator", "%"));
            confirmers.AddRange(Roles.FindUsersInRole("Führung", "%").Where(u=>!confirmers.Contains(u)));
            var model = new LimitedHeaderModel { Confirmers = confirmers };
            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(Player player)
        {
            if (ModelState.IsValid)
            {
                if (!User.IsInRole("Benutzer") && WebSecurity.CurrentUserId != player.UserId)
                {
                    return new HttpUnauthorizedResult("Du darfst nur Deinen eigenen Spieler bearbeiten!");
                }
                try
                {
                    if (player.PlayerId > 0)
                    {
                        ctx.Entry(player).State = EntityState.Modified;
                    }
                    else
                    {
                        ctx.Players.Add(player);
                    }
                    ctx.SaveChanges();
                    TempData["Message"] = "Deine Daten wurde gespeichert.";
                }
                catch (Exception ex)
                {
                    TempData["Message"] = "Es ist ein Fehler aufgetreten: " + ex.Message;
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditCastle(int id)
        {
            var model = ctx.Castles.Find(id);
            ViewBag.MapList = ctx.Maps.ToList().Select(m => new SelectListItem { Value = m.MapId.ToString(), Text = m.Name, Selected = m.MapId == model.MapId });
            ViewBag.Castles = ctx.Castles.Where(c => c.PlayerId == model.PlayerId).Include(c => c.Map).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult EditCastle(Castle model)
        {
            return SaveCastle(model);
        }

        public ActionResult CreateCastle()
        {
            var player = GetCurrentPlayer();
            if (player == null) return new HttpUnauthorizedResult("Du musst erst Deinen Spieler anlegen");
            var model = new Castle { PlayerId = player.PlayerId };
            ViewBag.MapList = ctx.Maps.ToList().Select(m => new SelectListItem { Value = m.MapId.ToString(), Text = m.Name });
            ViewBag.Castles = ctx.Castles.Where(c => c.PlayerId == player.PlayerId).Include(c => c.Map).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateCastle(Castle model)
        {
            return SaveCastle(model);
        }

        [HttpPost]
        public ActionResult DeleteCastle(int id)
        {
            var player = GetCurrentPlayer();
            var castle = ctx.Castles.Find(id);
            if (!User.IsInRole("Benutzer"))
            {
                if (player == null)
                    return new HttpUnauthorizedResult("Du musst erst Deinen Spieler anlegen");
                if (castle.PlayerId != player.PlayerId)
                    return new HttpUnauthorizedResult("Du darfst nur Deinen eigenen Spieler bearbeiten!");
            }

            ctx.Castles.Remove(castle);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        private ActionResult SaveCastle(Castle model)
        {
            var player = GetCurrentPlayer();
            if (!User.IsInRole("Benutzer"))
            {
                if (player == null)
                    return new HttpUnauthorizedResult("Du musst erst Deinen Spieler anlegen");
                if (model.PlayerId != player.PlayerId)
                    return new HttpUnauthorizedResult("Du darfst nur Deinen eigenen Spieler bearbeiten!");
            } 

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.CastleId > 0)
                    {
                        ctx.Entry(model).State = EntityState.Modified;
                    }
                    else
                    {
                        ctx.Castles.Add(model);
                    }
                    ctx.SaveChanges();
                    TempData["Message"] = "Deine Daten wurde gespeichert.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["Message"] = "Es ist ein Fehler aufgetreten: " + ex.Message;
                }
            }
            ViewBag.MapList = ctx.Maps.ToList().Select(m => new SelectListItem { Value = m.MapId.ToString(), Text = m.Name, Selected = m.MapId == model.MapId });
            ViewBag.Castles = ctx.Castles.Where(c => c.PlayerId == player.PlayerId).Include(c => c.Map).ToList();
            return View(model);
        }

        private Player GetCurrentPlayer()
        {
            return ctx.Players.SingleOrDefault(p => p.UserId == WebSecurity.CurrentUserId);
        }

    }
}
