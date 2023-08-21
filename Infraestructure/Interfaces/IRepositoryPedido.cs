using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Interfaces
{
    public interface IRepositoryPedido
    {
        IEnumerable<Pedido> GetPedidos();

        Pedido GetPedidoById(int idPedido);

        IEnumerable<Pedido> GetPedidosByUser(int userId);

        IEnumerable<Pedido> GetPedidosByVendedor(int vendedorId);

        bool Save(Pedido pedido, List<PedidoProducto> products);

        bool Delete(Pedido pedido);

        bool CambiarEstado(int pedido, int estado);

    }
}
