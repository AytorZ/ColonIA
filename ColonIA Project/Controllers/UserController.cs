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
                var usuario = context.Usuarios.FirstOrDefault(u => u.Correo == data.Correo && u.Contrasena == data.Contrasena);

                if (usuario != null)
                {
                    HttpContext.Session.SetInt32("UserId", usuario.IdUsuario);
                    HttpContext.Session.SetInt32("UserRole", usuario.IdRole);
                    HttpContext.Session.SetString("UserEmail", usuario.Correo);

                    return RedirectToAction("Index", "Home");
                }
            }

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



        [HttpPost]
        public async Task<IActionResult> EnviarCodigo([FromForm] string correo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(correo))
                    return Json(new { success = false, message = "Correo no proporcionado." });

                using var context = new DbColonIaContext();
                var correoLimpio = correo.Trim().ToLower();
                var usuario = context.Usuarios
                    .FirstOrDefault(u => u.Correo.Trim().ToLower() == correoLimpio);
                if (usuario == null)
                    return Json(new { success = false, message = "Correo no registrado." });

                // Generar codigo
                string code = new Random().Next(100000, 999999).ToString();
                usuario.ResetCode = code;
                usuario.ResetCodeExpiration = DateTime.Now.AddMinutes(10);
                await context.SaveChangesAsync();

                // Enviar correo
                using var smtp = new SmtpClient("smtp.gmail.com")

                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("smontenegroperez@gmail.com", "clave"),//clave debe ser reemplazada por la clave
                    Timeout = 20000
                };
                await smtp.SendMailAsync(new MailMessage("smontenegroperez@gmail.com", correo)
                {
                    Subject = "Código de Recuperación",
                    Body = $"Tu código de recuperación es: {code}"
                });

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error interno: " + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult VerificarCodigo(string correo, string codigo)
        {
            using var context = new DbColonIaContext();
            var correoLimpio = correo.Trim().ToLower();
            var usuario = context.Usuarios
                .FirstOrDefault(u => u.Correo.Trim().ToLower() == correoLimpio && u.ResetCode == codigo);

            if (usuario == null)
                return Json(new { success = false, message = "Código inválido o expirado." });

            if (!usuario.ResetCodeExpiration.HasValue || usuario.ResetCodeExpiration < DateTime.Now)
                return Json(new { success = false, message = "Código inválido o expirado." });

            TempData["CorreoRecuperacion"] = correo;
            return Json(new { success = true, redirectUrl = Url.Action("CambiarPassword", "User", new { correo }) });
        }

        [HttpPost]
        public async Task<IActionResult> CambiarPassword(string nuevaContrasena)
        {
            string? correo = TempData["CorreoRecuperacion"] as string;
            if (string.IsNullOrEmpty(correo))
                return RedirectToAction("Login");

            using var context = new DbColonIaContext();
            var correoLimpio = correo.Trim().ToLower();
            var usuario = context.Usuarios
                .FirstOrDefault(u => u.Correo.Trim().ToLower() == correoLimpio);
            if (usuario == null) return RedirectToAction("Login");

            usuario.Contrasena = nuevaContrasena;
            usuario.ResetCode = null;
            usuario.ResetCodeExpiration = null;
            await context.SaveChangesAsync();

            return RedirectToAction("Login");
        }



    }
}