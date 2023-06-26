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
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
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


        private SelectList CategoriaList(int categoriaID = 0)
        {
            IServiceCategoria serviceCategoria = new ServiceCategoria();
            IEnumerable<Categoria> lista = serviceCategoria.GetCategorias();



            return new SelectList(lista, "id", "descripcion", categoriaID);
        }

        private SelectList EstadoProductoList(int estadoProductoID = 0)
        {
            IServiceEstadoProducto serviceEstadoProducto = new ServiceEstadoProducto();
            IEnumerable<EstadoProducto> lista = serviceEstadoProducto.GetEstadoProductos();



            return new SelectList(lista, "id", "descripcion", estadoProductoID);
        }

        private IEnumerable<Chat> ChatsByProduct(int idVendedor, int idProducto)
        {
            IServiceChat serviceChat = new ServiceChat();
            IEnumerable<Chat> lista = serviceChat.GetChatsByVendedorProducto(idVendedor, idProducto);
            return lista;
        }


        private IEnumerable<string> FotoProductoList(int idProducto)
        {
            IServiceFoto serviceFoto = new ServiceFoto();
            IEnumerable<Foto> lista = serviceFoto.GetFotosByProducto(idProducto);

            List<string> imagenes = new List<string>();

            foreach (var foto in lista)
            {
                string base64String = Convert.ToBase64String(foto.foto1);
                string imageUrl = string.Format("data:image/jpeg;base64,{0}", base64String);
                imagenes.Add(imageUrl);
            }

            return imagenes;
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            IServiceProducto serviceProducto = new ServiceProducto();
            Producto oProducto;
            try
            {
                //Si es null el parametro
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                oProducto = serviceProducto.GetProductoById(Convert.ToInt32(id));
                if (oProducto == null)
                {
                    TempData["Message"] = "No existe el producto solicitado";
                    TempData["Redirect"] = "Libro";
                    TempData["Redirect-Action"] = "Index";
                    //Redireccion a la vista del error
                    return RedirectToAction("Default", "Error");

                }
                ViewBag.idCategoria = CategoriaList();
                ViewBag.idEstadoProducto = EstadoProductoList();
                ViewBag.idFoto = FotoProductoList(Convert.ToInt32(id));
                ViewBag.chats = ChatsByProduct(oProducto.vendedorId, oProducto.id);
                ViewBag.currentPage = "Producto: " + oProducto.nombre;
                return View(oProducto);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }

        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.idEstadoProducto = EstadoProductoList();
            ViewBag.idCategoria = CategoriaList();
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {

            IServiceProducto serviceProducto = new ServiceProducto();
            Producto oProducto;

            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                oProducto = serviceProducto.GetProductoById(Convert.ToInt32(id));
                if (oProducto == null)
                {
                    TempData["Message"] = "No existe el producto solicitado o no se encuentra disponible";
                    TempData["Redirect"] = "Usuario";
                    TempData["Redirect-Action"] = "Index";
                    //Redireccion a la vista del error
                    return RedirectToAction("Default", "Error");
                }

                ViewBag.idCategoria = CategoriaList();
                ViewBag.idEstadoProducto = EstadoProductoList();
                ViewBag.idFoto = FotoProductoList(Convert.ToInt32(id));
                return View(oProducto);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");

            }
        }

        // POST: Product/Edit/5
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

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
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
