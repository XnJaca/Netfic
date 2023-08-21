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
                     .Where(p => p.Usuario.id == userId)
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

        public IEnumerable<Pedido> GetPedidosByVendedor(int vendedorId)
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
                        .Include("PedidoProducto.Producto.Categoria")
                        .Where(p => p.PedidoProducto.Any(pv => pv.Producto.vendedorId == vendedorId))
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

        public bool Save(Pedido pedido, List<PedidoProducto> products)
        {
            using (MyContext ctx = new MyContext())
            {
                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        double totalPedido = 0;

                        foreach (var producto in products)
                        {
                            // Calcular el subtotal para cada PedidoProducto
                            producto.subtotal = producto.cantidad * producto.Producto.precio;

                            // Agregar el subtotal al total del pedido
                            totalPedido += producto.subtotal;
                        }

                        // Asignar el total calculado al pedido
                        pedido.total = totalPedido;

                        // Guardar el pedido en la base de datos
                        ctx.Pedido.Add(pedido);
                        ctx.SaveChanges();

                        // Crear nuevos PedidoProducto y asociarlos con el Pedido
                        foreach (var producto in products)
                        {
                            var pedidoProducto = new PedidoProducto
                            {
                                productoId = producto.productoId,
                                pedidoId = pedido.id,
                                cantidad = producto.cantidad,
                                subtotal = producto.subtotal
                            };
                            ctx.PedidoProducto.Add(pedidoProducto);
                        }

                        // Guardar los detalles del pedido (PedidoProducto) en la base de datos
                        ctx.SaveChanges();

                        // Descontar la cantidad de productos del inventario
                        foreach (var pedidoProducto in products)
                        {
                            var producto = ctx.Producto.Find(pedidoProducto.productoId);
                            if (producto != null)
                            {
                                producto.cantidad -= pedidoProducto.cantidad;
                            }
                        }

                        // Guardar los cambios en el inventario
                        ctx.SaveChanges();

                        // Confirmar la transacción
                        transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Si hay un error, deshacer la transacción
                        transaction.Rollback();
                        string mensaje = "";
                        Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                        // Puedes lanzar una excepción personalizada aquí si lo deseas.
                        throw new Exception("Error al guardar el pedido y actualizar el inventario.", ex);
                    }
                }
            }
        }

        public bool Delete(Pedido pedido)
        {
            throw new NotImplementedException();
        }

        public bool CambiarEstado(int pedidoId, int nuevoEstadoId)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    var pedido = ctx.Pedido.FirstOrDefault(p => p.id == pedidoId);

                    if (pedido != null)
                    {
                        pedido.estadoPedidoId = nuevoEstadoId;
                        ctx.SaveChanges(); // Guarda los cambios en la base de datos
                        return true;
                    }
                    else
                    {
                        return false; // Pedido no encontrado
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción que pueda ocurrir durante la operación
                // Puedes loggear o lanzar una excepción personalizada si es necesario
                return false;
            }
        }
    }
}
