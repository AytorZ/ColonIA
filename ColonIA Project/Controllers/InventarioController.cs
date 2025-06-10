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
            var model = new CreacionRecursoViewModel();
            try
            {

                model.ListaCategorias = context.CategoriaInventarios.ToList();
                model.NuevoInventario = new Inventario();

            }
            catch (Exception e)
            {

                ViewBag.ErrorMessage = e.Message;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult CreacionRecurso(CreacionRecursoViewModel data)
        {
            try
            {
                data.ListaCategorias = context.CategoriaInventarios.ToList();

                if (ModelState.IsValid)
                {
                    context.Inventarios.Add(data.NuevoInventario);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return View(data);
        }


    }
}
