using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? logUser)
        {
            if (Session["Usuario"] == null)
            {
                // Redireccionar a la página de inicio si el usuario no está autenticado
                return RedirectToAction("Index", "Auth");
            }

            Usuario oUsuario = Session["Usuario"] as Usuario;

            if (oUsuario.TipoUsuario.FirstOrDefault().id == 1)
            {
                ViewBag.currentPage = "Dashboard";
                return View();
            }
            else
            {
                ViewBag.currentPage = "Dashboard";
                return RedirectToAction("CustomerIndex", "Home");
            }
        }

        public ActionResult CustomerIndex()
        {

            IEnumerable<Producto> lista;

            try
            {
                IServiceProducto _ServiceProducto = new ServiceProducto();

                lista = _ServiceProducto.GetProductos();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            ViewBag.currentPage = "Productos";
            return View(lista);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.currentPage = "Inicio";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}