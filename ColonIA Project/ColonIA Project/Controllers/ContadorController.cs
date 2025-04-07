using System.Web.Mvc;

namespace ColonIA_Project.Controllers
{
    public class ContadorController : Controller
    {
        public ActionResult Index()
        {
            return View("Index"); 
        }

        public ActionResult RegistrarCostos()
        {
            return View("RegistrarCostos");
        }

        public ActionResult HistorialCostos()
        {
            return View("HistorialCostos");
        }

        public ActionResult ActualizarCostos()
        {
            return View("ActualizarCostos");
        }

        public ActionResult RegistrarVenta()
        {
            return View("RegistrarVenta");
        }

        public ActionResult RegistrarEgreso()
        {
            return View("RegistrarEgreso");
        }
    }
}
