﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Interfaces
{
    public interface IRepositoryPedidoProducto
    {
        IEnumerable<PedidoProducto> GetPedidosxProducto(int productoId);

        IEnumerable<PedidoProducto> GetPedidosProductoxVendedor(int vendedorId);
    }
}
