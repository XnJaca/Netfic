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
    public class PedidoProductoVendedorController : Controller
    {
        // GET: PedidoProductoVendedor
        public ActionResult Index()
        {
            IEnumerable<PedidoProducto> lista;

            try
            {
                IServicePedidoProducto servicePedido = new ServicePedidoProducto();
                lista = servicePedido.GetPedidosProductoxVendedor(5);

                List<IEnumerable<string>> idFotosList = new List<IEnumerable<string>>();

                foreach (var pedido in lista)
                {
                    int idProducto = pedido.Producto.id; // Obtén el idProducto de cada pedido (ajusta esto según tu modelo de datos)

                    IEnumerable<string> idFotos = FotoProductoList(idProducto); // Obtén las fotos correspondientes al idProducto

                    idFotosList.Add(idFotos); // Agrega las fotos a la lista
                }

                ViewBag.idFoto = idFotosList;
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

        // GET: PedidoProductoVendedor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PedidoProductoVendedor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PedidoProductoVendedor/Create
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

        // GET: PedidoProductoVendedor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PedidoProductoVendedor/Edit/5
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

        // GET: PedidoProductoVendedor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PedidoProductoVendedor/Delete/5
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
