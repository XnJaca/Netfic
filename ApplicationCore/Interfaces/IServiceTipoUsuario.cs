using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IServiceTipoUsuario
    {
        IEnumerable<TipoUsuario> GetTipoUsuarios();

        TipoUsuario GetTipoUsuarioById(int id);
        TipoUsuario Save(String descripcion);

        //TODO: UPDATE AND DELETE METHOD
    }
}
