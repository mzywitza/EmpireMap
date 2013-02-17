using EmpireMap.Models;
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
    [Authorize]
    public class ApplicationController : Controller
    {
        ApplicationContext ctx = new ApplicationContext(); 

        public ActionResult Index(int? id)
        {
            if (!User.IsInRole("Benutzer"))
                return RedirectToAction("Index","Player");

            var map = id.HasValue ?
                ctx.Maps.Single(m => m.MapId == id.Value) :
                ctx.Maps.Single(m => m.Name == "Grün");

            var model = new MapCastleModel
            {
                Id = map.MapId,
                Maps = ctx.Maps.ToList(),
                Castles = ctx.Castles.Where(c => c.MapId == map.MapId).Include(c => c.Player).ToList()
            };

            return View(model);
        }
    }
}
