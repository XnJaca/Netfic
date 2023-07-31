using Infraestructure.Interfaces;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryChat : IRepositoryChat
    {
        public Chat GetChatById(int id)
        {
            Chat oChat;

            try
            {
                using (MyContext ctx = new MyContext())
                {

                    ctx.Configuration.LazyLoadingEnabled = false;

                    oChat = ctx.Chat.Find(id);
                }

                return oChat;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<Chat> GetChatsByVendedorProducto(int id, int idProducto)
        {
            IEnumerable<Chat> chats;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    chats = ctx.Chat
                        .Include("Producto")
                        .Include("Usuario")
                        .Include("Usuario1")
                        .Where(c => c.Usuario1.id == id && c.productoId == idProducto)
                        .ToList();
                }

                return chats;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Chat Save(Chat chat)
        {
            int retorno;

            Chat oChat;

            try
            {
                using (MyContext ctx = new MyContext())
                {

                    ctx.Configuration.LazyLoadingEnabled = false;

                    oChat = GetChatById((int)chat.id);

                    if (oChat == null)
                    {
                        ctx.Chat.Add(chat);
                        retorno = ctx.SaveChanges();
                    }
                    else
                    {
                        chat.mensaje = oChat.mensaje;
                        ctx.Chat.Add(chat);
                        ctx.Entry(chat).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();
                    }

                    if (retorno >= 0)
                        oChat = GetChatById((int)chat.id);


                    return oChat;



                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
