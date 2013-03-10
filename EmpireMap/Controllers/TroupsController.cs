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

    }
}
