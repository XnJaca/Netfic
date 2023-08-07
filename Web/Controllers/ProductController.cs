using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Web.Utils;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            Usuario oUsuario = Session["Usuario"] as Usuario;
            IEnumerable<Producto> lista;

            if (TempData.ContainsKey("mensaje"))
            {
                ViewBag.NotificationMessage = TempData["mensaje"];
            }

            try
            {
                IServiceProducto _ServiceProducto = new ServiceProducto();


                lista = _ServiceProducto.GetProductosByVendedor(oUsuario.id);

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

        public ActionResult IndexSeller()
        {

            Usuario oUsuario = Session["Usuario"] as Usuario;
            IEnumerable<Producto> lista;

            if (TempData.ContainsKey("mensaje"))
            {
                ViewBag.NotificationMessage = TempData["mensaje"];
            }

            try
            {
                IServiceProducto _ServiceProducto = new ServiceProducto();


                lista = _ServiceProducto.GetProductosByVendedor(oUsuario.id);

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

        // POST: Product/Create - Update
        [HttpPost]
        public ActionResult Save(Producto pProducto, List<HttpPostedFileBase> ImageFiles, Categoria categoria, EstadoProducto estadoProducto)
        {
            MemoryStream target = new MemoryStream();
            IServiceProducto serviceProducto = new ServiceProducto();

            Usuario oUsuario = Session["Usuario"] as Usuario;

            if (pProducto.Foto == null)
            {
                if (ImageFiles != null && ImageFiles.Count > 0)
                {
                    foreach (var imageFile in ImageFiles)
                    {
                        if (imageFile != null)
                        {
                            // Procesar el archivo de imagen y guardarlo en la entidad Producto
                            using (var memoryStream = new MemoryStream())
                            {
                                imageFile.InputStream.CopyTo(memoryStream);
                                try
                                {
                                    byte[] imageBytes = memoryStream.ToArray();
                                    // Guardar los bytes de la imagen en la entidad Producto
                                    // Asegúrate de tener una propiedad en la entidad Producto para almacenar la imagen
                                    pProducto.Foto.Add(new Foto { foto1 = imageBytes });
                                }
                                catch (Exception)
                                {

                                    throw;
                                }

                            }
                        }
                    }
                }
            }

            pProducto.vendedorId = oUsuario.id;
            // Guardar el Producto en la base de datos
            serviceProducto.Save(pProducto, ImageFiles);
            if (oUsuario.TipoUsuario.FirstOrDefault().id == 3)
            {
                return RedirectToAction("IndexSeller");
            }
            else
            {
                return RedirectToAction("Index", "Product");
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

        //GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            IServiceProducto serviceProducto = new ServiceProducto();
            try
            {
                // TODO: Add delete logic here

                bool oProducto = serviceProducto.Delete(id);

                if (oProducto)
                {
                    Log.Warn($"Se elimino correctamente el producto con el ID {id}.");
                    TempData["mensaje"] = Util.SweetAlertHelper.Mensaje("Producto",
                        "Se elimino correctamente el producto.", Util.SweetAlertMessageType.success);
                }

                return RedirectToAction("IndexSeller");
            }
            catch
            {
                //Log.Warn($"Sucedio un error al borrar el producto, asegurese que no tiene pedidos asociados a este producto.");

                Log.Warn($"Sucedio un error al borrar el producto, asegurese que no tiene pedidos asociados a este producto.");
                TempData["mensaje"] = Util.SweetAlertHelper.Mensaje("Producto",
                    "Sucedio un error al eliminar el producto, asegurese que no tiene pedidos asociados al mismo.", Util.SweetAlertMessageType.warning);
                return RedirectToAction("IndexSeller");
            }
        }

        // POST: Product/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    IServiceProducto serviceProducto = new ServiceProducto();
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        bool oProducto = serviceProducto.Delete(id);

        //        if (oProducto)
        //        {
        //            Log.Warn($"Se elimino correctamente el Producto.");

        //            ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("Producto",
        //                "Eliminar Producto", Util.SweetAlertMessageType.warning);
        //        }

        //        return RedirectToAction("IndexSeller");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
