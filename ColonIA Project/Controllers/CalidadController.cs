using Microsoft.AspNetCore.Mvc;

namespace ColonAI.Controllers
{
    public class CalidadController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult RegistrarLotesInspeccionados()
        {
            return View("RegistrarLotesInspeccionados");
        }

        public ActionResult ValidacionParametrosCalidad()
        {
            return View("ValidacionParametrosCalidad");
        }

        public ActionResult RegistrarAnalisisLaboratorio()
        {
            return View();
        }

        public ActionResult GenerarCertificadosCalidad()
        {
            return View("GenerarCertificadosCalidad");
        }

        public ActionResult GestionResultadosLaboratorio()
        {
            return View("GestionResultadosLaboratorio");
        }

        public ActionResult SeguimientoHistorialCalidad()
        {
            return View("SeguimientoHistorialCalidad");
        }

        public ActionResult VerificacionPreviaEnvio()
        {
            return View("VerificacionPreviaEnvio");
        }

        public ActionResult GenerarReportesCalidad()
        {
            return View("GenerarReportesCalidad");
        }

        public ActionResult ComparacionCalidadPorLote()
        {
            return View("ComparacionCalidadPorLote");
        }
    }
}
