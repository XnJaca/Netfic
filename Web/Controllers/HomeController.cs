using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

            if (oUsuario.TipoUsuario.FirstOrDefault().id == 1 && logUser == 1 || logUser == null)
            {
                return View();
            }
            else {
                return RedirectToAction("CustomerIndex", "Home");
            }
        }

        public ActionResult CustomerIndex() { return View(); }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}