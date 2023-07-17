using ApplicationCore.Interfaces;
using Infraestructure.Interfaces;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ApplicationCore.Services
{
    public class ServiceProducto : IServiceProducto
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Producto GetProductoById(int id)
        {
            IRepositoryProducto repositoryProducto = new RepositoryProducto();
            return repositoryProducto.GetProductoById(id);
        }

        public IEnumerable<Producto> GetProductos()
        {
            IRepositoryProducto repositoryProducto = new RepositoryProducto();
            return repositoryProducto.GetProductos();
        }

        public IEnumerable<Producto> GetProductosByVendedor(int vendedorId)
        {
            IRepositoryProducto repositoryProducto = new RepositoryProducto();
            return repositoryProducto.GetProductosByVendedor(vendedorId);
        }

        public Producto Save(Producto producto, List<HttpPostedFileBase> imageFiles)
        {
            IRepositoryProducto repositoryProducto = new RepositoryProducto();
            return repositoryProducto.Save(producto, imageFiles);
        }
    }
}
