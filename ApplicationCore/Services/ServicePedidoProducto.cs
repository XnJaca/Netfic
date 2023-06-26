using ApplicationCore.Interfaces;
using Infraestructure.Interfaces;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServicePedidoProducto : IServicePedidoProducto
    {
        public IEnumerable<PedidoProducto> GetPedidosProductoxVendedor(int vendedorId)
        {
            IRepositoryPedidoProducto repositoryPedidoProducto = new RepositoryPedidoProducto();
            return repositoryPedidoProducto.GetPedidosProductoxVendedor(vendedorId);
        }

        public IEnumerable<PedidoProducto> GetPedidosxProducto(int productoId)
        {
            throw new NotImplementedException();
        }
    }
}
