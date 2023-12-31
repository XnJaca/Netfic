﻿using Infraestructure.Interfaces;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Infraestructure.Repository
{
    public class RepositoryPedidoProducto : IRepositoryPedidoProducto
    {
        public IEnumerable<PedidoProducto> GetPedidosProductoxVendedor(int vendedorId)
        {
            IEnumerable<PedidoProducto> pedidoProductos;

            try
            {
                using (MyContext ctx = new MyContext())
                {

                    ctx.Configuration.LazyLoadingEnabled = false;

                    pedidoProductos = ctx.PedidoProducto
                        .Include(p => p.Producto)
                        .Include(p => p.Pedido)
                        .Include("Pedido.Usuario").ToList();
                }

                return pedidoProductos;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<PedidoProducto> GetPedidosxProducto(int productoId)
        {
            throw new NotImplementedException();
        }
    }
}
