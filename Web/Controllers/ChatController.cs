using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Index()
        {
            return View();
        }

        // GET: Chat/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Chat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chat/Create
        [HttpPost]
        public ActionResult Save(int? idChat, int idEmisor, int idReceptor, int idProducto, string texto)
        {
            try
            {
                IServiceChat serviceChat = new ServiceChat();

                Chat chat = new Chat();
                if (idChat != null)
                {
                    chat.id = (int)idChat;
                    chat.respuesta = texto;
                }
                else {
                    chat.mensaje = texto;
                }
                chat.fechaEnvio = DateTime.Now;
                chat.productoId = idProducto;
                chat.emisorId = idEmisor;
                chat.receptorId = idReceptor;



                var updatedChats = serviceChat.Save(chat);

                string action = "Details/" + idProducto;
                return RedirectToActionPermanent(action, "Product");
            }
            catch
            {
                string action = "Details/" + idProducto;
                return RedirectToAction(action, "Product");
            }
        }

        // GET: Chat/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Chat/Edit/5
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

        // GET: Chat/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Chat/Delete/5
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
