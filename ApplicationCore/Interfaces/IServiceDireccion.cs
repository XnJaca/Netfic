using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IServiceDireccion
    {
        IEnumerable<Direccion> GetDireccions();
        Direccion GetDireccionById(int id);

        IEnumerable<Direccion> GetDireccionByIdUsuario(int idUsuario);
        Direccion Save(Direccion direccion);

        Boolean Delete(int id);
    }
}

