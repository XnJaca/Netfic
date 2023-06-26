using Infraestructure.Interfaces;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryChat : IRepositoryChat
    {
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
    }
}
