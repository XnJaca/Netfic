using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IServiceUsuario
    {

        IEnumerable<Usuario> GetUsuarios();

        Usuario GetUsuarioById(int id);
        Usuario Save(Usuario pUsuario, string[] pTipoUsuarios, string[] pTelefonos);

        Usuario Login(string username, string password);

        Usuario Logout();


    }
}
