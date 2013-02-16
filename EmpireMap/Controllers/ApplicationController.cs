using EmpireMap.Models;
using EmpireMap.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace EmpireMap.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class ApplicationController : Controller
    {
        ApplicationContext ctx = new ApplicationContext(); 

        public ActionResult Index()
        {
            if (!User.IsInRole("Benutzer"))
                return RedirectToAction("Index","Limited");
            return View();
        }
    }
}
