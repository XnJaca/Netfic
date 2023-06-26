using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Interfaces
{
    public interface IRepositoryProducto
    {
        IEnumerable<Producto> GetProductos();

        Producto GetProductoById(int id);

        Producto Save(Producto producto);

        Boolean Delete(int id);
    }
}
