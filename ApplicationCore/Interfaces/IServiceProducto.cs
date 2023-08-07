using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ApplicationCore.Interfaces
{
    public interface IServiceProducto
    {

        IEnumerable<Producto> GetProductos();

        Producto GetProductoById(int id);

        IEnumerable<Producto> GetProductosByVendedor(int vendedorId);

        IEnumerable<Producto> GetProductoByCategoria(int categoriaId);

        Producto Save(Producto producto, List<HttpPostedFileBase> imageFiles);

        Boolean Delete(int id);
    }
}
