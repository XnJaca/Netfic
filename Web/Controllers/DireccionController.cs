using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Controllers
{
    public class DireccionController : Controller
    {
        private readonly string jsonUrl = "https://gist.githubusercontent.com/josuenoel/80daca657b71bc1cfd95a4e27d547abe/raw/5c615419196ed40a3dbdff69cb3d9719b1d6bb1e/provincias_cantones_distritos_costa_rica.json";

        // GET: Direccion
        public ActionResult Index(int idUsuario)
        {
            IEnumerable<Direccion> lista;
            try
            {
                IServiceDireccion _ServiceDireccion = new ServiceDireccion();
                lista = _ServiceDireccion.GetDireccionByIdUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }

            ViewBag.currentPage = "Direcciones";
            ViewBag.idUsuario = idUsuario;
            return View(lista);
        }


        // GET: Direccion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Direccion/Create

        public async Task<Direccion> getDirecciones()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var json = await httpClient.GetStringAsync(jsonUrl);

                    // Deserializar los datos JSON en las propiedades Provincias y Cantones del modelo
                    var direccion = JsonConvert.DeserializeObject<Direccion>(json);


                    return direccion;
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores, puedes personalizarlo según tus necesidades
                throw;
            }
        }

        public async Task<ActionResult> Create(int idUsuario)
        {
            try
            {
                var direccion = await getDirecciones();

                ViewBag.idUsuario = idUsuario;
                return View(direccion);
            }
            catch (Exception ex)
            {
                // Manejo de errores, puedes personalizarlo según tus necesidades
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<JsonResult> GetCantonesByProvincia(int provincia)
        {
            // Aquí debes implementar la lógica para obtener los cantones de la provincia con el ID proporcionado.
            // Puedes usar la propiedad 'cantones' del objeto 'direccion' que se deserializó anteriormente.

            // Supongamos que 'direccion' es el objeto que contiene los datos de las provincias y cantones
            var direccion = await getDirecciones();
            var cantonesDeProvincia = direccion.Provincias[provincia + ""].cantones;

            // Devuelve los cantones en formato JSON
            return Json(cantonesDeProvincia, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetDistritosByCanton(string provinciaId, string cantonNombre)
        {
            var direccion = await getDirecciones();
            // Obtén el ID del cantón a partir de su nombre usando FirstOrDefault con valor predeterminado
            string cantonId = direccion.Provincias.Values.SelectMany(p => p.cantones)
                                                      .FirstOrDefault(c => c.Value.nombre == cantonNombre).Key;

            // Si cantonId es igual a 0, no se encontró el cantón en el diccionario
            if (cantonId == null)
            {
                return Json(new List<string>(), JsonRequestBehavior.AllowGet);
            }

            // Obtén el cantón seleccionado directamente usando el idCanton
            var cantonSeleccionado = direccion.Provincias[provinciaId].cantones[cantonId.ToString()];

            // Acceder a los distritos del cantón usando el diccionario de distritos del cantón seleccionado
            var distritosDeCanton = cantonSeleccionado.distritos.Values;

            // Devolver los distritos en formato JSON
            return Json(distritosDeCanton, JsonRequestBehavior.AllowGet);
        }

        // POST: Direccion/Create
        [HttpPost]
        public ActionResult Save(Direccion direccion)
        {
            try
            {
                IServiceDireccion serviceDireccion = new ServiceDireccion();
                try
                {
                    Direccion oDirecccion = serviceDireccion.Save(direccion);
                    var routeValues = new RouteValueDictionary
                    {
                        { "idUsuario", oDirecccion.idUsuario }
                    };

                    // Redirigir a la acción Index con el RouteValueDictionary como parámetro
                    return RedirectToAction("Index", "Direccion", routeValues);
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

        // GET: Direccion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Direccion/Edit/5
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

        // GET: Direccion/Delete/5
        public ActionResult Delete(int id, int idUsuario)
        {
            IServiceDireccion serviceDireccion = new ServiceDireccion();
            try
            {
                // TODO: Add delete logic here
                Direccion oDireccion = serviceDireccion.GetDireccionById(id);
                bool oCategoria = serviceDireccion.Delete(id);


                // Crear el RouteValueDictionary para pasar el parámetro idUsuario a la acción Index
                var routeValues = new RouteValueDictionary
                {
                    { "idUsuario", oDireccion.idUsuario }
                };

                // Redirigir a la acción Index con el RouteValueDictionary como parámetro
                return RedirectToAction("Index", "Direccion", routeValues);
            }
            catch
            {
                return View();
            }
        }

        // POST: Direccion/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
            
        //}
    }
}
