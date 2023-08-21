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
    public class ServiceEstadoPedido : IServiceEstadoPedido
    {
        private IRepositoryEstadoPedido repositoryEstadoPedido = new RepositoryEstadoPedido();
        public IEnumerable<EstadoPedido> GetAll()
        {
            return repositoryEstadoPedido.GetAll();
        }

        public EstadoPedido GetById(int id)
        {
            return repositoryEstadoPedido.GetById(id);
        }
    }
}
