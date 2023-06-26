using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IServiceFoto
    {

        IEnumerable<Foto> GetFotosByProducto(int idProducto);

        Foto Save(Foto foto);

        Boolean Delete(int idProducto);

    }
}
