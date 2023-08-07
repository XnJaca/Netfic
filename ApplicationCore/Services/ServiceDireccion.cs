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
    public class ServiceDireccion : IServiceDireccion
    {
        public bool Delete(int id)
        {
            IRepositoryDireccion repository = new RepositoryDireccion();

            return repository.Delete(id);
        }

        public Direccion GetDireccionById(int id)
        {
            IRepositoryDireccion repository = new RepositoryDireccion();

            return repository.GetDireccionById(id);
        }

        public IEnumerable<Direccion> GetDireccionByIdUsuario(int idUsuario)
        {
            IRepositoryDireccion repository = new RepositoryDireccion();

            return repository.GetDireccionByIdUsuario(idUsuario);
        }

        public IEnumerable<Direccion> GetDireccions()
        {
            IRepositoryDireccion repository = new RepositoryDireccion();

            return repository.GetDireccions();
        }

        public Direccion Save(Direccion direccion)
        {
            IRepositoryDireccion repository = new RepositoryDireccion();

            return repository.Save(direccion);
        }
    }
}
