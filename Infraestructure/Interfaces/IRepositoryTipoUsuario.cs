using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Interfaces
{
    public interface IRepositoryTipoUsuario
    {
        IEnumerable<TipoUsuario> GetTipoUsuarios();

        TipoUsuario GetTipoUsuarioById(int id);

        TipoUsuario Save(string descripcion);
    }
}
