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
    public class ServiceFoto : IServiceFoto
    {
        public bool Delete(int idProducto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Foto> GetFotosByProducto(int idProducto)
        {
            IRepositoryFoto repositoryFoto = new RepositoryFoto();

            return repositoryFoto.GetFotosByProducto(idProducto);
        }

        public Foto Save(Foto foto)
        {
            throw new NotImplementedException();
        }
    }
}
