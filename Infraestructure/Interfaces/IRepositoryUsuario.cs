using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Interfaces
{
    public interface IRepositoryUsuario
    {

        IEnumerable<Usuario> GetUsuarios();

        Usuario GetUsuarioById(int id);

        Usuario Save(Usuario usuario);

        Usuario Login(string username, string password);

        Usuario Logout();

    }
}
