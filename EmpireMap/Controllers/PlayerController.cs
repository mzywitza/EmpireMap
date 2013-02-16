using EmpireMap.Filters;
using EmpireMap.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace EmpireMap.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class PlayerController : Controller
    {
        private ApplicationContext ctx = new ApplicationContext();

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
                    ViewBag.Message = "Deine Daten wurde gespeichert.";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Es ist ein Fehler aufgetreten: " + ex.Message;
                }
            }
            return PartialView("_PlayerData", player);
        }


    }
}
