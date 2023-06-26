using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IServicePedido
    {
        Pedido GetPedidoById(int idPedido);

        IEnumerable<Pedido> GetPedidos();

        IEnumerable<Pedido> GetPedidosByUser(int userId);
    }
}
