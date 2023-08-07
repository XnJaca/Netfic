using ApplicationCore.Interfaces;
using Infraestructure.Interfaces;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        public Usuario GetUsuarioById(int id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();

            return repository.GetUsuarioById(id);
        }

        public IEnumerable<Usuario> GetUsuarioDeshabilitados()
        {
            IRepositoryUsuario repository = new RepositoryUsuario();

            return repository.GetUsuarioDeshabilitados();
        }

        public IEnumerable<Usuario> GetUsuarios()
        {
            IRepositoryUsuario repository = new RepositoryUsuario();

            return repository.GetUsuarios();
        }

        public Usuario Login(string username, string password)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();

            return repository.Login(username, password);
        }


        public Usuario Logout()
        {
            throw new NotImplementedException();
        }

        public Usuario Save(Usuario pUsuario, string[] pTipoUsuarios, string[] pTelefonos)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.Save(pUsuario, pTipoUsuarios, pTelefonos);
        }
    }
}
