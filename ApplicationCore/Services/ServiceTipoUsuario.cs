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
    public class ServiceTipoUsuario : IServiceTipoUsuario
    {
        public TipoUsuario GetTipoUsuarioById(int id)
        {
            IRepositoryTipoUsuario repository = new RepositoryTipoUsuario();
            return repository.GetTipoUsuarioById(id);
        }

        public IEnumerable<TipoUsuario> GetTipoUsuarios()
        {
            IRepositoryTipoUsuario repository = new RepositoryTipoUsuario();
            return repository.GetTipoUsuarios();
        }

        public TipoUsuario Save(string descripcion)
        {
            throw new NotImplementedException();
        }
    }
}
