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
    public class PedidoController : Controller
    {
        // GET: Pedido
        public ActionResult Index()
        {
            IEnumerable<Pedido> lista;

            try
            {
                IServicePedido servicePedido = new ServicePedido();
                Usuario oUsuario = Session["Usuario"] as Usuario;
                lista = servicePedido.GetPedidosByUser(oUsuario.id);

                List<IEnumerable<string>> idFotosList = new List<IEnumerable<string>>();

                foreach (var pedido in lista)
                {
                    int idProducto = pedido.PedidoProducto.FirstOrDefault().Producto.id; // Obtén el idProducto de cada pedido (ajusta esto según tu modelo de datos)

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

        // GET: Pedido/Details/5
        public ActionResult Details(int id)
        {
            Pedido pedido;

            try
            {
                IServicePedido servicePedido = new ServicePedido();
                pedido = servicePedido.GetPedidoById(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            ViewBag.idFoto = FotoProductoList(pedido.PedidoProducto.FirstOrDefault().Producto.id);
            ViewBag.currentPage = "Producto";
            return View(pedido);
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

        // GET: Pedido/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pedido/Create
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

        // GET: Pedido/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pedido/Edit/5
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

        // GET: Pedido/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pedido/Delete/5
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
