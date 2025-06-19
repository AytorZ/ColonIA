using Microsoft.AspNetCore.Mvc;
using ColonIA.Models;
using ColonIA.Data;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace ColonIA.Controllers
{
    public class UserController : Controller
    {
        private readonly ColonIaContext context = new();


        [HttpGet]
        public ActionResult UsuariosOperador()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult UsuariosAdmin(int page = 1)
        {
            int pageSize = 3;

            var query = context.Usuarios
                .Include(u => u.IdRoleNavigation)
                .OrderBy(u => u.NombreCompleto);

            int totalUsuarios = query.Count();

            var usuariosPaginados = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int totalPages = (int)Math.Ceiling((double)totalUsuarios / pageSize);

            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;

            return View(usuariosPaginados);
        }



        [HttpPost]
        public IActionResult EditarUsuario(Usuario data)
        {
            var u = context.Usuarios.Find(data.IdUsuario);
            if (u != null)
            {
                u.NombreCompleto = data.NombreCompleto;
                u.Correo = data.Correo;
                u.Telefono = data.Telefono;
                u.Direccion = data.Direccion;
                u.IdRole = data.IdRole;
                if (!string.IsNullOrWhiteSpace(data.Contrasena))
                    u.Contrasena = data.Contrasena;
                context.SaveChanges();
            }
            return RedirectToAction(nameof(UsuariosAdmin));
        }


        [HttpPost]
        public IActionResult EliminarUsuario(int id)
        {
            var u = context.Usuarios.Find(id);
            if (u != null)
            {
                context.Usuarios.Remove(u);
                context.SaveChanges();
            }
            return RedirectToAction(nameof(UsuariosAdmin));
        }






        [HttpGet]
        public ActionResult UsuariosSupervisor(int page = 1)
        {
            int pageSize = 8;

            var query = context.Usuarios
                .Include(u => u.IdRoleNavigation)
                .Where(u => u.IdRole == 3)
                .OrderBy(u => u.NombreCompleto);

            int totalUsuarios = query.Count();

            var usuariosPaginados = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int totalPages = (int)Math.Ceiling((double)totalUsuarios / pageSize);

            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;

            return View(usuariosPaginados);

        }




        [HttpPost]
        public IActionResult EditarUsuarioSupervisor(Usuario data)
        {
            var u = context.Usuarios.Find(data.IdUsuario);
            if (u != null && u.IdRole == 3)
            {
                u.NombreCompleto = data.NombreCompleto;
                u.Correo = data.Correo;
                u.Telefono = data.Telefono;
                u.Direccion = data.Direccion;
              
                if (!string.IsNullOrWhiteSpace(data.Contrasena))
                    u.Contrasena = data.Contrasena;
                context.SaveChanges();
            }
            return RedirectToAction(nameof(UsuariosSupervisor));
        }


        [HttpPost]
        public IActionResult EliminarUsuarioSupervisor(int id)
        {
            var u = context.Usuarios.Find(id);
            if (u != null && u.IdRole == 3)
            {
                context.Usuarios.Remove(u);
                context.SaveChanges();
            }
            return RedirectToAction(nameof(UsuariosSupervisor));
        }







        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario data)
        {
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

        [HttpPost]
        public ActionResult Register(Usuario data)
        {
            if (data.Contrasena.Equals(data.ConfirmacionContrasenna))
            {
                context.Usuarios.Add(data);
                context.SaveChanges();
                return RedirectToAction("UsuariosAdmin", "User");
            }
            else
            {
                ViewBag.ErrorMessage = "Las contraseñas no coinciden";
                return View();
            }

        }

        [HttpGet]
        public ActionResult CambiarPassword()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PerfilUsuario()
        {
            int? userID = HttpContext.Session.GetInt32("UserId");

            if (userID == null)
                return RedirectToAction("Index", "Home");

            var result = context.Usuarios.FirstOrDefault(u => u.IdUsuario == userID);
            return View(result);
        }

        // NUEVO: Actualizar perfil del usuario
        [HttpPost]
        public IActionResult ActualizarPerfil(Usuario data)
        {
            var usuarioBD = context.Usuarios.FirstOrDefault(u => u.IdUsuario == data.IdUsuario);
            if (usuarioBD == null)
                return NotFound();

            usuarioBD.NombreCompleto = data.NombreCompleto;
            usuarioBD.Correo = data.Correo;
            usuarioBD.Telefono = data.Telefono;
            usuarioBD.Direccion = data.Direccion;

            // Solo se actualiza la contraseña si se proporciona una nueva
            if (!string.IsNullOrWhiteSpace(data.Contrasena))
                usuarioBD.Contrasena = data.Contrasena;

            context.SaveChanges();

            return RedirectToAction("PerfilUsuario");
        }

        [HttpPost]
        public async Task<IActionResult> EnviarCodigo([FromForm] string correo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(correo))
                    return Json(new { success = false, message = "Correo no proporcionado." });

                using var context = new ColonIaContext();
                var correoLimpio = correo.Trim().ToLower();
                var usuario = context.Usuarios
                    .FirstOrDefault(u => u.Correo.Trim().ToLower().Equals(correoLimpio));
                if (usuario == null)
                    return Json(new { success = false, message = "Correo no registrado." });

                // Generar código
                string code = new Random().Next(100000, 999999).ToString();
                usuario.ResetCode = code;
                usuario.ResetCodeExpiration = DateTime.Now.AddMinutes(10);
                await context.SaveChangesAsync();

                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "PasswordResetTemplate.html");
                if (!System.IO.File.Exists(templatePath))
                    return Json(new { success = false, message = "No se encontró la plantilla de correo." });

                string body = await System.IO.File.ReadAllTextAsync(templatePath);
                body = body.Replace("{{code}}", code);

                // Enviar el correo
                using var smtp = new SmtpClient("smtp.gmail.com")
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("proyectocolonia.info@gmail.com", "tzdu ohls mspp taad"),
                    Timeout = 20000
                };

                var mail = new MailMessage("proyectocolonia.info@gmail.com", correo)
                {
                    Subject = "Restablecimiento de Contraseña - Código de Recuperación",
                    IsBodyHtml = true,
                    Body = body
                };

                await smtp.SendMailAsync(mail);

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
            using var context = new ColonIaContext();
            var correoLimpio = correo.Trim().ToLower();
            var usuario = context.Usuarios
                .FirstOrDefault(u => u.Correo.Trim().ToLower().Equals(correoLimpio) && u.ResetCode == codigo);

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

            using var context = new ColonIaContext();
            var correoLimpio = correo.Trim().ToLower();
            var usuario = context.Usuarios
                .FirstOrDefault(u => u.Correo.Trim().ToLower().Equals(correoLimpio));
            if (usuario == null) return RedirectToAction("Login");

            usuario.Contrasena = nuevaContrasena;
            usuario.ResetCode = null;
            usuario.ResetCodeExpiration = null;
            await context.SaveChangesAsync();

            return RedirectToAction("Login");
        }
    }
}
