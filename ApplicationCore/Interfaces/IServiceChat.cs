using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IServiceChat
    {

        IEnumerable<Chat> GetChatsByVendedorProducto(int id, int idProducto);

        Chat Save(Chat chat);
    }
}
