using Microsoft.AspNetCore.Mvc;
using ColonIA.Models;
using ColonIA.Data;

namespace ColonIA.Controllers
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

        [HttpPost]
        public ActionResult Login(Usuario data)
        {
            var context = new DbColonIaContext();

            if (context.Usuarios.Any(u => (u.Correo == data.Correo) && (u.Contrasena == data.Contrasena)))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
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