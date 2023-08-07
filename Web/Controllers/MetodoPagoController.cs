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
using System.Web.Routing;

namespace Web.Controllers
{
    public class MetodoPagoController : Controller
    {
        // GET: MetodoPago
        public ActionResult Index(int idUsuario)
        {
            IEnumerable<MetodoPago> lista;
            try
            {
                IServiceMetodoPago _ServiceMetodoPago = new ServiceMetodoPago();
                lista = _ServiceMetodoPago.GetMetodoPagoByIdUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }

            ViewBag.currentPage = "MetodoPago";
            ViewBag.idUsuario = idUsuario;
            return View(lista);
        }

        // GET: MetodoPago/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MetodoPago/Create
        public ActionResult Create(int idUsuario)
        {
            try
            {
                ViewBag.idUsuario = idUsuario;
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }

        }

        // POST: MetodoPago/Create
        [HttpPost]
        public ActionResult Save(MetodoPago metodoPago)
        {
            try
            {
                IServiceMetodoPago _ServiceMetodoPago = new ServiceMetodoPago();
                try
                {
                    if (metodoPago.tipo == "Sinpe")
                    {
                        metodoPago.fechaExpericacion = null;
                    }
                    MetodoPago oMetodoPago = _ServiceMetodoPago.Save(metodoPago);
                    var routeValues = new RouteValueDictionary
                    {
                        { "idUsuario", oMetodoPago.usuarioId }
                    };

                    // Redirigir a la acción Index con el RouteValueDictionary como parámetro
                    return RedirectToAction("Index", "MetodoPago", routeValues);
                }
                catch (Exception)
                {

                    throw;
                }

            }
            catch
            {
                return View();
            }
        }

        // GET: MetodoPago/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MetodoPago/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MetodoPago/Delete/5
        public ActionResult Delete(int id)
        {
            IServiceMetodoPago _ServiceMetodoPago = new ServiceMetodoPago();
            try
            {
                // TODO: Add delete logic here
                MetodoPago oMetodoPago = _ServiceMetodoPago.GetMetodoPagoById(id);
                bool isDelete = _ServiceMetodoPago.Delete(id);


                // Crear el RouteValueDictionary para pasar el parámetro idUsuario a la acción Index
                var routeValues = new RouteValueDictionary
                {
                    { "idUsuario", oMetodoPago.usuarioId }
                };

                // Redirigir a la acción Index con el RouteValueDictionary como parámetro
                return RedirectToAction("Index", "MetodoPago", routeValues);
            }
            catch
            {
                return View();
            }
        }

        // POST: MetodoPago/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
