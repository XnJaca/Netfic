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
    public class ServiceMetodoPago : IServiceMetodoPago
    {
        public bool Delete(int id)
        {
            IRepositoryMetodoPago repositoryMetodoPago = new RepositoryMetodoPago();
            return repositoryMetodoPago.Delete(id);
        }

        public MetodoPago GetMetodoPagoById(int id)
        {
            IRepositoryMetodoPago repositoryMetodoPago = new RepositoryMetodoPago();
            return repositoryMetodoPago.GetMetodoPagoById(id);
        }

        public IEnumerable<MetodoPago> GetMetodoPagoByIdUsuario(int idUsuario)
        {
            IRepositoryMetodoPago repositoryMetodoPago = new RepositoryMetodoPago();
            return repositoryMetodoPago.GetMetodoPagoByIdUsuario(idUsuario);
        }

        public IEnumerable<MetodoPago> GetMetodoPagos()
        {
            IRepositoryMetodoPago repositoryMetodoPago = new RepositoryMetodoPago();
            return repositoryMetodoPago.GetMetodoPagos();
        }

        public MetodoPago Save(MetodoPago metodoPago)
        {
            IRepositoryMetodoPago repositoryMetodoPago = new RepositoryMetodoPago();
            return repositoryMetodoPago.Save(metodoPago);
        }
    }
}
