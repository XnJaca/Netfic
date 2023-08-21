using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Interfaces
{
    public interface IRepositoryEstadoPedido
    {
        IEnumerable<EstadoPedido> GetAll();
        EstadoPedido GetById(int id);
    }
}
