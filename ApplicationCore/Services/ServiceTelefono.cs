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
    public class ServiceTelefono : IServiceTelefono
    {
        public Telefono GetTelefonoById(int id)
        {
            IRepositoryTelefono repositoryTelefono = new RepositoryTelefono();
            return repositoryTelefono.GetTelefonoById(id);
        }

        public IEnumerable<Telefono> GetTelefonoByIdUser(int userId)
        {
            IRepositoryTelefono repositoryTelefono = new RepositoryTelefono();
            return repositoryTelefono.GetTelefonoByIdUser(userId);
        }

        public IEnumerable<Telefono> GetTelefonos()
        {
            IRepositoryTelefono repositoryTelefono = new RepositoryTelefono();
            return repositoryTelefono.GetTelefonos();
        }

        public Telefono Save(string idUsuario, string numero, string tipoTelefono)
        {
            throw new NotImplementedException();
        }
    }
}
