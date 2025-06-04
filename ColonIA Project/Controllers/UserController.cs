using Microsoft.AspNetCore.Mvc;
using ColonIA.Models;
using ColonIA.Data;
using System.Net.Mail;
using System.Net;

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



        [HttpPost]
        public async Task<IActionResult> EnviarCodigo(string correo)
        {
            using var context = new DbColonIaContext();
            var usuario = context.Usuarios.FirstOrDefault(u => u.Correo == correo);

            if (usuario == null)
                return Json(new { success = false, message = "Correo no registrado." });

            // Generar codigo
            string code = new Random().Next(100000, 999999).ToString();
            //usuario.ResetCode = code;
            //usuario.ResetCodeExpiration = DateTime.Now.AddMinutes(10);
            await context.SaveChangesAsync();

            // Enviar email
            using var smtp = new SmtpClient("smtp.gmail.com")
            {
                Credentials = new NetworkCredential("smontenegroperez@gmail.com", "wkmm nshk tfeo rpda"),
                EnableSsl = true
            };
            await smtp.SendMailAsync(new MailMessage("smontenegroperez", correo)
            {
                Subject = "Código de Recuperación",
                Body = $"Tu código de recuperación es: {code}"
            });

            return Json(new { success = true });
        }

        //[HttpPost]
        //public IActionResult VerificarCodigo(string correo, string codigo)
        //{
        //    using var context = new DbColonIaContext();
        //    //var usuario = context.Usuarios.FirstOrDefault(u => u.Correo == correo && u.ResetCode == codigo);

        //    if (usuario == null || usuario.ResetCodeExpiration < DateTime.Now)
        //        return Json(new { success = false, message = "Código inválido o expirado." });

        //    // Guardar correo como verificado
        //    TempData["CorreoRecuperacion"] = correo;

        //    return Json(new { success = true });
        //}

        [HttpPost]
        public async Task<IActionResult> CambiarPassword(string nuevaContrasena)
        {
            string? correo = TempData["CorreoRecuperacion"] as string;
            if (string.IsNullOrEmpty(correo))
                return RedirectToAction("Login");

            using var context = new DbColonIaContext();
            var usuario = context.Usuarios.FirstOrDefault(u => u.Correo == correo);
            if (usuario == null) return RedirectToAction("Login");

            usuario.Contrasena = nuevaContrasena;
            //usuario.ResetCode = null;
            //usuario.ResetCodeExpiration = null;
            await context.SaveChangesAsync();

            return RedirectToAction("Login");
        }



    }
}