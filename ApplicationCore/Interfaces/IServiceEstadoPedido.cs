using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IServiceEstadoPedido
    {
        IEnumerable<EstadoPedido> GetAll();
        EstadoPedido GetById(int id);
    }
}
