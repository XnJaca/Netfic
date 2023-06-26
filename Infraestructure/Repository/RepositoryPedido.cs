using Infraestructure.Interfaces;
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
    public class RepositoryPedido : IRepositoryPedido
    {
        public Pedido GetPedidoById(int idPedido)
        {
            Pedido oPedido;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;


                    oPedido = ctx.Pedido
                         .Include(p => p.Direccion)
                         .Include(p => p.EstadoPedido)
                         .Include(p => p.MetodoPago)
                         .Include(p => p.Usuario)
                         .Include(p => p.PedidoProducto)
                         .Include("PedidoProducto.Producto")
                         .Include("PedidoProducto.Producto.Categoria")
                        .FirstOrDefault(p => p.id == idPedido);
                }


                return oPedido;
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

        public IEnumerable<Pedido> GetPedidos()
        {
            IEnumerable<Pedido> pedidos = new List<Pedido>();

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    pedidos = ctx.Pedido.Include("Direccion").Include("EstadoPedido").Include("MetodoPago").Include("Usuario").Include("PedidoProducto").ToList();
                }

                return pedidos;
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

        public IEnumerable<Pedido> GetPedidosByUser(int userId)
        {
            IEnumerable<Pedido> oPedido;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    oPedido = ctx.Pedido
                     .Include(p => p.Direccion)
                     .Include(p => p.EstadoPedido)
                     .Include(p => p.MetodoPago)
                     .Include(p => p.Usuario)
                     .Include(p => p.PedidoProducto)
                     .Include("PedidoProducto.Producto")
                     .ToList();


                }

                return oPedido;
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
    }
}
