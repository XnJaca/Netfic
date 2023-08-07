using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            IEnumerable<Usuario> lista;
            try
            {
                IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                lista = _ServiceUsuario.GetUsuarios();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }

            ViewBag.currentPage = "Usuarios";
            return View(lista);
        }

        public ActionResult GetUsuariosDeshabilitados()
        {
            IEnumerable<Usuario> lista;
            try
            {
                IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                lista = _ServiceUsuario.GetUsuarioDeshabilitados();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }

            ViewBag.currentPage = "Usuarios";
            return View(lista);

        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario oUsuario;
            try
            {

                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                oUsuario = _ServiceUsuario.GetUsuarioById(Convert.ToInt32(id));

                if (oUsuario == null)
                {
                    TempData["Message"] = "No existe el usuario solicitado";
                    TempData["Redirect"] = "Usuario";
                    TempData["Redirect-Action"] = "Index";
                    //Redireccion a la vista del error
                    return RedirectToAction("Default", "Error");
                }
                return View(oUsuario);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }

        private SelectList TelefonosUsuarioList(int userID = 0)
        {
            IServiceTelefono _ServiceTelefono = new ServiceTelefono();
            IEnumerable<Telefono> lista = _ServiceTelefono.GetTelefonoByIdUser(userID);

            var list = lista.Select(t => new SelectListItem
            {
                Value = t.id.ToString(),
                Text = t.numero + " (" + t.tipoTelefono + ")"
            });

            return new SelectList(list, "Value", "Text", userID);
        }

        private SelectList TipoUsuarioList(int idTipoUsuario = 0)
        {
            IServiceTipoUsuario _ServiceTipoUsuario = new ServiceTipoUsuario();
            IEnumerable<TipoUsuario> lista = _ServiceTipoUsuario.GetTipoUsuarios();

            return new SelectList(lista, "id", "descripcion", idTipoUsuario);

        }


        //[Authorize]
        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.idTelefono = TelefonosUsuarioList();
            ViewBag.idTipoUsuario = TipoUsuarioList();
            return View();
        }

        // POST: User/Create-Update
        [HttpPost]
        public ActionResult Save(Usuario pUsuario, string[] telefonos, string[] tipoUsuarios)
        {

            IServiceUsuario _ServiceUsuario = new ServiceUsuario();

            try
            {
                Usuario oUsuario = _ServiceUsuario.Save(pUsuario, tipoUsuarios, telefonos);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int? id)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario oUsuario;

            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                oUsuario = _ServiceUsuario.GetUsuarioById(Convert.ToInt32(id));
                if (oUsuario == null)
                {
                    TempData["Message"] = "No existe el usuario solicitado o no se encuentra disponible";
                    TempData["Redirect"] = "Usuario";
                    TempData["Redirect-Action"] = "Index";
                    //Redireccion a la vista del error
                    return RedirectToAction("Default", "Error");
                }

                //ViewBag.idTipoUsuario = TipoUsuarioList(oUsuario.TipoUsuario.)
                ViewBag.idTipoUsuario = TipoUsuarioList(oUsuario.TipoUsuario.FirstOrDefault().id);
                ViewBag.telefonos = TelefonosUsuarioList(oUsuario.id);
                return View(oUsuario);

            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");

            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
