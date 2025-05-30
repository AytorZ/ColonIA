using Microsoft.AspNetCore.Mvc;

namespace ColonIA.Controllers
{
    public class AnalisisController : Controller
    {
        // GET: Analisis
        public ActionResult AnalisisChat()
        {
            return View();
        }
        public ActionResult AnalisisAI()
        {
            return View();
        }
    }

}