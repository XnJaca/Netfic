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
    public class ServiceEstadoProducto : IServiceEstadoProducto
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public EstadoProducto GetEstadoProductoByID(int id)
        {
            IRepositoryEstadoProducto repositoryEstadoProducto = new RepositoryEstadoProducto();

            return repositoryEstadoProducto.GetEstadoProductoByID(id);
        }

        public IEnumerable<EstadoProducto> GetEstadoProductos()
        {
            IRepositoryEstadoProducto repositoryEstadoProducto = new RepositoryEstadoProducto();

            return repositoryEstadoProducto.GetEstadoProductos();
        }

        public EstadoProducto Save(EstadoProducto estadoProducto)
        {
            throw new NotImplementedException();
        }
    }
}
