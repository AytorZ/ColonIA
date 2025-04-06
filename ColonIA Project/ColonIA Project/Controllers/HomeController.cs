using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Practica2.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}