using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;

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

                if (oUsuario.TipoUsuario.FirstOrDefault().id == 2)
                {
                    lista = servicePedido.GetPedidosByUser(oUsuario.id);
                }
                else
                {
                    lista = servicePedido.GetPedidosByVendedor(oUsuario.id);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            ViewBag.currentPage = "Pedidos";
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
                ViewBag.idFoto = FotoProductoList(pedido.PedidoProducto.Select(pp => pp.Producto.id));
                ViewBag.direccionCompleta = pedido.Direccion.ToString();
                ViewBag.currentPage = "Producto";
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }

            return View(pedido);
        }


        private IEnumerable<string> FotoProductoList(IEnumerable<int> idProductos)
        {
            IServiceFoto serviceFoto = new ServiceFoto();
            List<string> imagenes = new List<string>();

            foreach (int idProducto in idProductos)
            {
                byte[] foto = serviceFoto.GetFotosByProducto(idProducto).FirstOrDefault().foto1;

                //foreach (var foto in lista)
                //{
                string base64String = Convert.ToBase64String(foto);
                string imageUrl = string.Format("data:image/jpeg;base64,{0}", base64String);
                imagenes.Add(imageUrl);
                //}
            }

            return imagenes;
        }
        // GET: Pedido/Create
        public ActionResult Create()
        {
            List<PedidoProducto> cart = Session["Cart"] as List<PedidoProducto>;

            Usuario oUsuario = new Usuario();
            //Obtener metodos de pago del usuario.
            if (Session["Usuario"] != null)
            {
                Usuario usuario = Session["Usuario"] as Usuario;
                IServiceUsuario serviceUsuario = new ServiceUsuario();
                oUsuario = serviceUsuario.GetUsuarioById(usuario.id);

            }
            else
            {
                return View(cart);
            }
            ViewBag.MetodosPago = oUsuario.MetodoPago;
            ViewBag.Direccion = oUsuario.Direccion;

            return View(cart);

        }

        [HttpPost]
        public ActionResult CambiarEstado(int pedidoId, int nuevoEstado)
        {
            IServicePedido servicePedido = new ServicePedido();

            if (servicePedido.CambiarEstado(pedidoId, nuevoEstado))
            {

                return RedirectToAction("Details", new { id = pedidoId });
            }

            return RedirectToAction("Details", new { id = pedidoId });

        }

        [HttpPost]
        public ActionResult Save(int metodoPago, int direccion)
        {
            // Aquí debes obtener el usuario actual. Esto es solo un ejemplo, puedes ajustarlo según tu autenticación.
            Usuario usuario = Session["Usuario"] as Usuario;

            // Crear una instancia de Pedido y configurar sus propiedades.
            IServiceEstadoPedido serviceEstadoPedido = new ServiceEstadoPedido();
            EstadoPedido estadoPedidos = serviceEstadoPedido.GetById(1);
            Pedido nuevoPedido = new Pedido
            {
                usuarioId = usuario.id,
                direccionId = direccion,
                estadoPedidoId = estadoPedidos.id,
                metodoPagoId = metodoPago,
                fecha_pedido = DateTime.Now
            };

            List<PedidoProducto> cart = Session["Cart"] as List<PedidoProducto>;
            IServicePedido servicePedido = new ServicePedido();
            if (servicePedido.Save(nuevoPedido, cart))
            {
                return RedirectToAction("Details", new { nuevoPedido.id });
            }

            return View(cart);

        }



        public ActionResult CartPartial()
        {
            List<PedidoProducto> cart = Session["Cart"] as List<PedidoProducto>;
            return PartialView("_CartPartial", cart);
        }


        [HttpPost]
        public ActionResult AddToCart(int productId, int quantity)
        {
            // Obtén el producto por su ID desde la base de datos
            IServiceProducto serviceProducto = new ServiceProducto();
            Producto product = serviceProducto.GetProductoById(productId);

            if (product != null)
            {
                // Crea un objeto PedidoProducto con los detalles del producto y la cantidad
                PedidoProducto pedidoProducto = new PedidoProducto
                {
                    Producto = product,
                    cantidad = quantity
                };

                // Obtiene el carrito de la sesión
                List<PedidoProducto> cart = Session["Cart"] as List<PedidoProducto>;

                if (cart == null)
                {
                    cart = new List<PedidoProducto>();
                    cart.Add(pedidoProducto);
                }
                else
                {
                    // Verifica si el producto ya existe en el carrito
                    PedidoProducto existingItem = cart.FirstOrDefault(item => item.Producto.id == productId);

                    if (existingItem != null)
                    {
                        // Actualiza la cantidad del producto existente en el carrito
                        existingItem.cantidad += quantity;
                    }
                    else
                    {
                        // Agrega el nuevo producto al carrito
                        cart.Add(pedidoProducto);
                    }
                }

                // Actualiza el carrito en sesión
                Session["Cart"] = cart;
                // Retorna una respuesta para indicar que el producto se agregó o actualizó correctamente
                return Json(new { success = true });
            }
            else
            {
                // Retorna una respuesta para indicar que hubo un error al agregar el producto
                return Json(new { success = false, message = "Producto no encontrado." });
            }
        }

        [HttpPost]
        public ActionResult UpdateCart(int productId, int quantity)
        {
            List<PedidoProducto> cart = Session["Cart"] as List<PedidoProducto>;

            if (cart != null)
            {
                PedidoProducto productToUpdate = cart.FirstOrDefault(item => item.Producto.id == productId);

                if (productToUpdate != null)
                {
                    productToUpdate.cantidad = quantity;
                    productToUpdate.subtotal = productToUpdate.Producto.precio * quantity;
                    Session["Cart"] = cart;
                    return Json(new { success = true });
                }
            }

            return Json(new { success = false, message = "Producto no encontrado en el carrito." });
        }

        [HttpPost]
        public ActionResult DeleteFromCart(int productId)
        {
            List<PedidoProducto> cart = Session["Cart"] as List<PedidoProducto>;

            if (cart != null)
            {
                PedidoProducto productToRemove = cart.FirstOrDefault(item => item.Producto.id == productId);

                if (productToRemove != null)
                {
                    cart.Remove(productToRemove);
                    Session["Cart"] = cart;
                }
            }

            return Json(new { success = true });
        }

        //// POST: Pedido/Create
        //[HttpPost]
        //public ActionResult Save(Pedido pedido, PedidoProducto pedidoProducto, Direccion direccion, MetodoPago metodoPago, EstadoPedido estadoPedido)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


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
