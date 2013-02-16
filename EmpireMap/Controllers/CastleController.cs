using EmpireMap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmpireMap.Controllers
{
    [Authorize]
    public class CastleController : Controller
    {
        private ApplicationContext ctx = new ApplicationContext(); 

        public ActionResult Edit(int castleId)
        {
            var model = ctx.Castles.Find(castleId);
            return PartialView(model);
        }

    }
}
