using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColonIA_Project.Controllers
{
    public class ProduccionController : Controller
    {
        #region Incubacion
        [HttpGet]
        public ActionResult Incubacion()
        {
            return View();
        }

        public ActionResult IncubacionDetalles()
        {
            return View();
        }
        #endregion

        #region Crianza
        [HttpGet]
        public ActionResult Crianza()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CrianzaDetalles()
        {
            return View();
        }

        #endregion

        #region Cosecha
        [HttpGet]
        public ActionResult Cosecha()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CosechaDetalles()
        {
            return View();
        }

        #endregion
    }
}