using Microsoft.AspNetCore.Mvc;

namespace ColonAI.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult UsuariosOperador()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UsuariosAdmin()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UsuariosSupervisor()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CambiarPassword()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PerfilUsuario()
        {
            return View();
        }

    }



}