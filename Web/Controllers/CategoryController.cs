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
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            IEnumerable<Categoria> lista;
            try
            {
                IServiceCategoria _ServiceCategoria = new ServiceCategoria();

                lista = _ServiceCategoria.GetCategorias();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }

            ViewBag.currentPage = "Categorias";
            return View(lista);
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create-Update
        [HttpPost]
        public ActionResult Save(Categoria categoria)
        {
            IServiceCategoria serviceCategoria = new ServiceCategoria();
            try
            {
                Categoria oCategoria = serviceCategoria.Save(categoria);


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {


            return View();
        }


        public ActionResult GetCategoryDetails(int id)
        {
            try
            {
                IServiceCategoria _ServiceCategoria = new ServiceCategoria();
                Categoria categoria = _ServiceCategoria.GetCategoriaById(id);

                return Json(categoria, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return HttpNotFound();
            }
        }

        // POST: Category/Edit/5
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

        // GET: Category/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Category/Delete/5
        //[G]
        public ActionResult Delete(int id)
        {
            IServiceCategoria serviceCategoria = new ServiceCategoria();
            try
            {
                // TODO: Add delete logic here

                bool oCategoria = serviceCategoria.Delete(id);


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
