using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ColonIA.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult CapturarError()
        {
            var referer = Request.Headers["Referer"].ToString();
            var error = HttpContext.Features.Get<IExceptionHandlerFeature>();

            TempData["MensajeError"] = error!.Error.Message;

            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }
            return View();
        }
    }
}
