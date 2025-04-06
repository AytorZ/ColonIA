using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ColonIA_Project
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Contador",
                url: "contador",
                defaults: new { controller = "Contador", action = "Index" }
            );

            routes.MapRoute(
                name: "RegistrarCostos",
                url: "contador/registrar",
                defaults: new { controller = "Contador", action = "RegistrarCostos" }
            );

            routes.MapRoute(
                name: "HistorialCostos",
                url: "contador/historial",
                defaults: new { controller = "Contador", action = "HistorialCostos" }
            );

            routes.MapRoute(
                name: "ActualizarCostos",
                url: "contador/actualizar",
                defaults: new { controller = "Contador", action = "ActualizarCostos" }
            );

            routes.MapRoute(
                name: "RegistrarVenta",
                url: "contador/venta",
                defaults: new { controller = "Contador", action = "RegistrarVenta" }
            );

            routes.MapRoute(
                name: "RegistrarEgreso",
                url: "contador/egreso",
                defaults: new { controller = "Contador", action = "RegistrarEgreso" }
            );

            routes.MapRoute(
                name: "RegistrarLotesInspeccionados",
                url: "calidad/registrarlotesinspeccionados",
                defaults: new { controller = "Calidad", action = "RegistrarLotesInspeccionados" }
            );

            routes.MapRoute(
                name: "ValidacionParametrosCalidad",
                url: "calidad/validacionparametroscalidad",
                defaults: new { controller = "Calidad", action = "ValidacionParametrosCalidad" }
            );

            routes.MapRoute(
                name: "RegistrarAnalisisLaboratorio",
                url: "calidad/registraranalisislaboratorio",
                defaults: new { controller = "Calidad", action = "RegistrarAnalisisLaboratorio" }
            );

            routes.MapRoute(
                name: "GenerarCertificadosCalidad",
                url: "calidad/generarcertificadoscalidad",
                defaults: new { controller = "Calidad", action = "GenerarCertificadosCalidad" }
            );

            routes.MapRoute(
                name: "GestionResultadosLaboratorio",
                url: "calidad/gestionresultadoslaboratorio",
                defaults: new { controller = "Calidad", action = "GestionResultadosLaboratorio" }
            );

            routes.MapRoute(
                name: "SeguimientoHistorialCalidad",
                url: "calidad/seguimientohistorialcalidad",
                defaults: new { controller = "Calidad", action = "SeguimientoHistorialCalidad" }
            );

            routes.MapRoute(
                name: "VerificacionPreviaEnvio",
                url: "calidad/verificacionpreviaenvio",
                defaults: new { controller = "Calidad", action = "VerificacionPreviaEnvio" }
            );

            routes.MapRoute(
                name: "GenerarReportesCalidad",
                url: "calidad/generarreportescalidad",
                defaults: new { controller = "Calidad", action = "GenerarReportesCalidad" }
            );

            routes.MapRoute(
                name: "ComparacionCalidadPorLote",
                url: "calidad/comparacioncalidadporlote",
                defaults: new { controller = "Calidad", action = "ComparacionCalidadPorLote" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
