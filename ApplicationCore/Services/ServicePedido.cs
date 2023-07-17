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
    public class ServicePedido : IServicePedido
    {
        public Pedido GetPedidoById(int idPedido)
        {
            IRepositoryPedido repositoryPedido = new RepositoryPedido();
            return repositoryPedido.GetPedidoById(idPedido);
        }

        public IEnumerable<Pedido> GetPedidos()
        {
            IRepositoryPedido repositoryPedido = new RepositoryPedido();
            return repositoryPedido.GetPedidos();
        }

        public IEnumerable<Pedido> GetPedidosByUser(int userId)
        {
            IRepositoryPedido repositoryPedido = new RepositoryPedido();
            return repositoryPedido.GetPedidosByUser(userId);
        }

        public IEnumerable<Pedido> GetPedidosByVendedor(int vendedorId)
        {
            IRepositoryPedido repositoryPedido = new RepositoryPedido();
            return repositoryPedido.GetPedidosByVendedor(vendedorId);
        }
    }
}
