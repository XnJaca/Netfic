using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Interfaces
{
    public interface IRepositoryMetodoPago
    {
        IEnumerable<MetodoPago> GetMetodoPagos();
        MetodoPago GetMetodoPagoById(int id);

        IEnumerable<MetodoPago> GetMetodoPagoByIdUsuario(int idUsuario);
        MetodoPago Save(MetodoPago metodoPago);

        Boolean Delete(int id);
    }
}
