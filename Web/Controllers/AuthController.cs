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
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            if (Session["Usuario"] != null)
            {
                // Redireccionar a la página de inicio si el usuario ya está autenticado
                return RedirectToAction("Index", "Home");
            }


            return View();
        }

        public ActionResult Login(string username, string password)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario oUsuario;

            try
            {

                oUsuario = _ServiceUsuario.Login(username, password);

                if (oUsuario == null)
                {
                    TempData["Message"] = "Usuario o Contraseña Incorrectos.";
                    TempData["Redirect"] = "Auth";
                    TempData["Redirect-action"] = "Index";
                    // Redireccion a la vista del error
                    return RedirectToAction("Index", "Auth");
                }
                Response.RemoveOutputCacheItem(Url.Action("Login", "Auth"));
                Session["Usuario"] = oUsuario;
                return RedirectToActionPermanent("Index", "Home");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Index", "Auth");

            }
        }

        public ActionResult SignUp(Usuario oUsuario)
        {
            return View();
        }

        public ActionResult Logout()
        {
            // Eliminar la variable de sesión del usuario
            Session.Remove("Usuario");

            // Redireccionar a la página de inicio de sesión
            return RedirectToAction("Index", "Auth");
        }

    }
}
