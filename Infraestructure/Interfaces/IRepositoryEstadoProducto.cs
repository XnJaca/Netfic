using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Interfaces
{
    public interface IRepositoryEstadoProducto
    {

        IEnumerable<EstadoProducto> GetEstadoProductos();

        EstadoProducto GetEstadoProductoByID(int id);

        EstadoProducto Save(EstadoProducto estadoProducto);

        Boolean Delete(int id);

    }
}
