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
using System.Web.Security;
using Web.Utils;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult SignIn(string tipo)
        {
            Usuario usuario = new Usuario();
            usuario.nombre = "Invitado";
            usuario.apellidos = "Invitaod";
            usuario.TipoUsuario.Add(
                new TipoUsuario
                {
                    id = 4,
                    descripcion = tipo

                }
                );
            Session["Usuario"] = usuario;
            return RedirectToAction("Index", "Home");

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
                    Log.Warn($"Intento de inicio de secion{username}");

                    ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("Login",
                        "Usuario no válido", Util.SweetAlertMessageType.warning);

                    return View("Index");
                }

                if (!oUsuario.estado)
                {
                    Log.Warn($"Intento de inicio de secion{username}");

                    ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("Login",
                        "Su usuario esta inactivo, no puede ingresar a la plataforma, contacte con un administrador.", Util.SweetAlertMessageType.warning);

                    return View("Index");
                }


                Response.RemoveOutputCacheItem(Url.Action("Login", "Auth"));

                Session["Usuario"] = oUsuario;

                Log.Info($"Accede{oUsuario.nombre} {oUsuario.apellidos} " +
                            $"con el rol {oUsuario.TipoUsuario.FirstOrDefault().descripcion}");
                TempData["mensaje"] = Util.SweetAlertHelper.Mensaje("Login",
                    "Usuario autenticado", Util.SweetAlertMessageType.success);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");

            }
        }
        private SelectList TipoUsuarioList(int idTipoUsuario = 0)
        {
            IServiceTipoUsuario _ServiceTipoUsuario = new ServiceTipoUsuario();
            IEnumerable<TipoUsuario> lista = _ServiceTipoUsuario.GetTipoUsuarios();

            // Filtrar la lista para eliminar el tipo de usuario con id igual a 1
            lista = lista.Where(tipoUsuario => tipoUsuario.id != 1);

            return new SelectList(lista, "id", "descripcion", idTipoUsuario);
        }

        public ActionResult SignUp(Usuario oUsuario)
        {
            ViewBag.idTipoUsuario = TipoUsuarioList();
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
