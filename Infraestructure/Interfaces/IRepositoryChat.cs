using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Interfaces
{
    public interface IRepositoryChat
    {
        IEnumerable<Chat> GetChatsByVendedorProducto(int id, int idProducto);
    }
}
