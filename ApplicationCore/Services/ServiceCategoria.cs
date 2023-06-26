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
    public class ServiceCategoria : IServiceCategoria
    {
        public bool Delete(int id)
        {
            IRepositoryCategoria repositoryCategoria = new RepositoryCategoria();

            return repositoryCategoria.Delete(id);
        }

        public Categoria GetCategoriaById(int id)
        {
            IRepositoryCategoria repositoryCategoria = new RepositoryCategoria();

            return repositoryCategoria.GetCategoriaById(id);
        }

        public IEnumerable<Categoria> GetCategorias()
        {
            IRepositoryCategoria repositoryCategoria = new RepositoryCategoria();

            return repositoryCategoria.GetCategorias();

        }

        public Categoria Save(Categoria categoria)
        {
            IRepositoryCategoria repositoryCategoria = new RepositoryCategoria();

            return repositoryCategoria.Save(categoria);
        }
    }
}
