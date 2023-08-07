using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IServiceMetodoPago
    {
        IEnumerable<MetodoPago> GetMetodoPagos();
        MetodoPago GetMetodoPagoById(int id);

        IEnumerable<MetodoPago> GetMetodoPagoByIdUsuario(int idUsuario);
        MetodoPago Save(MetodoPago metodoPago);

        Boolean Delete(int id);
    }
}
