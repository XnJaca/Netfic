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
using Web.Utils;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.currentPage = "Dashboard";


            if (Util.VerifyAuthentication(this))
            {
                Usuario oUsuario = Session["Usuario"] as Usuario;

                if (oUsuario.TipoUsuario.FirstOrDefault().id == 1)
                {

                    return View();
                }
                else if (oUsuario.TipoUsuario.FirstOrDefault().id == 2)
                {

                    return RedirectToAction("CustomerIndex", "Home");
                }
                else
                {
                    return RedirectToAction("SellerIndex", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Auth");
            }


        }

        public ActionResult CustomerIndex()
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
            ViewBag.currentPage = "Dashboard";
            return View(lista);
        }


        public ActionResult SellerIndex()
        {

            IEnumerable<Pedido> lista;


            try
            {
                IServicePedido _ServicePedido = new ServicePedido();
                Usuario oUsuario = Session["Usuario"] as Usuario;
                lista = _ServicePedido.GetPedidosByVendedor(oUsuario.id);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            ViewBag.currentPage = "Dashboard";
            return View(lista);

        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.currentPage = "Inicio";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}