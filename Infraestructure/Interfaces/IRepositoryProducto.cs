using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infraestructure.Interfaces
{
    public interface IRepositoryProducto
    {
        IEnumerable<Producto> GetProductos();

        Producto GetProductoById(int id);

        IEnumerable<Producto> GetProductosByVendedor(int vendedorId);

        Producto Save(Producto producto, List<HttpPostedFileBase> imageFiles);

        Boolean Delete(int id);
    }
}
