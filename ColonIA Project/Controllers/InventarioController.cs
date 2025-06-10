using ColonIA.Data;
using ColonIA.Models;
using Microsoft.AspNetCore.Mvc;

namespace ColonIA.Controllers
{
    public class InventarioController : Controller
    {
        ColonIaContext context = new();
        // GET: /Inventario/Recursos
        public ActionResult Recursos()
        {
            return View();
        }

        // GET: /Inventario/HistorialRecursos
        public ActionResult HistorialRecursos()
        {
            return View();
        }

        // GET: /Inventario/AsignarRecurso
        public ActionResult AsignarRecurso()
        {
            return View();
        }

        // GET: /Inventario/HistorialInventario
        public ActionResult HistorialInventario()
        {
            return View();
        }

        public ActionResult Categorias()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Categorias(CategoriaInventario data)
        {
            var result = context.CategoriaInventarios.Add(data);
            context.SaveChanges();

            if (result != null)
            {
                return View();
            }
            else
            {
                ViewBag.ErrorMessage = "Hubo un problema al agregar la categoria";
                return View();
            }
        }

        [HttpGet]
        public ActionResult CreacionRecurso()
        {
            var model = new CreacionRecursoViewModel
            {
                ListaCategorias = context.CategoriaInventarios.ToList(),
                NuevoInventario = new Inventario()
            };

            return View(model);
        }


    }
}
