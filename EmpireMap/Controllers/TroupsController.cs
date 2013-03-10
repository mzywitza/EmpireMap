using EmpireMap.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace EmpireMap.Controllers
{
    [Authorize]
    public class TroupsController : Controller
    {
        private ApplicationContext ctx = new ApplicationContext();

        public ActionResult Index()
        {
            var model = TroupIndexModel.Create(ctx, WebSecurity.CurrentUserId);
            if (model == null) return RedirectToAction("Index", "Player");
            return View(model);
        }

        [HttpPost]
        public ActionResult AddTroups(int mapId)
        {
            try
            {
                var player = GetCurrentPlayer();
                var data = new Troup
                {
                    Player = player,
                    MapId = mapId,
                    LastUpdated = DateTime.Now
                };

                ctx.Troups.Add(data);
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Es ist ein Fehler aufgetreten: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            var player = GetCurrentPlayer();
            var troups = ctx.Troups.Include(t=>t.Map).SingleOrDefault(t => t.PlayerId == player.PlayerId && t.TroupId == id);
            if (troups == null) return new HttpUnauthorizedResult("Du darfst nur Deine eigenen Truppen bearbeiten");

            ViewBag.IndexModel = TroupIndexModel.Create(ctx, WebSecurity.CurrentUserId);

            return View(troups);
        }

        private Player GetCurrentPlayer()
        {
            return ctx.Players.SingleOrDefault(p => p.UserId == WebSecurity.CurrentUserId);
        }
    }
}
