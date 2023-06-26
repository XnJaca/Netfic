using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IServiceProducto
    {

        IEnumerable<Producto> GetProductos();

        Producto GetProductoById(int id);

        Producto Save(Producto producto);

        Boolean Delete(int id);
    }
}
