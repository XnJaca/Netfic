using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Interfaces
{
    public interface IRepositoryDireccion
    {
        IEnumerable<Direccion> GetDireccions();
        Direccion GetDireccionById(int id);

        IEnumerable<Direccion> GetDireccionByIdUsuario(int idUsuario);
        Direccion Save(Direccion Direccion);

        Boolean Delete(int id);
    }
}
