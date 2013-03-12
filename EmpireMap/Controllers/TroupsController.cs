using EmpireMap.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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

        [HttpPost]
        public ActionResult Edit(int id, Troup data)
        {

            var player = GetCurrentPlayer();
            if (data.PlayerId != player.PlayerId) return new HttpUnauthorizedResult("Du darfst nur Deine eigenen Truppen bearbeiten");

            if (ModelState.IsValid)
            {
                try
                {
                    data.LastUpdated = DateTime.Now;
                    ctx.Entry(data).State = EntityState.Modified;
                    ctx.SaveChanges();
                    TempData["Message"] = "Deine Daten wurde gespeichert.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["Message"] = "Es ist ein Fehler aufgetreten: " + ex.Message;
                }
            }

            data.Map = ctx.Maps.Find(data.MapId);
            data.Player = player;
            ViewBag.IndexModel = TroupIndexModel.Create(ctx, WebSecurity.CurrentUserId);

            return View(data);
        }

        [Authorize(Roles="Administrator,Führung")]
        public ActionResult TroupList()
        {
            return View(ctx.Troups.Include(t=>t.Player).Include(t=>t.Map).ToList());
        }

        [Authorize(Roles = "Administrator,Führung")]
        public ActionResult CsvList()
        {
            var data = ctx.Troups.Include(t => t.Player).Include(t => t.Map).OrderBy(t => t.Map.Name).ThenBy(t => t.Player.Name).ToList();
            
            //HttpContext.Response.AddHeader("content-disposition", "attachment; filename=Truppen.csv");

            var sw = new StreamWriter(new MemoryStream());

            sw.WriteLine("\"Welt\";\"Spieler\";\"Deff\";\"S. Deff\";\"Off\";\"S. Off\";\"Stand\"");
            foreach (var line in data)
            {
                sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\"",
                                           line.Map.Name,
                                           line.Player.Name,
                                           line.Deff,
                                           line.EnhancedDeff,
                                           line.Off,
                                           line.EnhancedOff,
                                           line.LastUpdated));
            }
            sw.Flush();
            sw.BaseStream.Seek(0, SeekOrigin.Begin);

            return File(sw.BaseStream, "text/csv", "Truppen.csv");
        }

        private Player GetCurrentPlayer()
        {
            return ctx.Players.SingleOrDefault(p => p.UserId == WebSecurity.CurrentUserId);
        }
    }
}
