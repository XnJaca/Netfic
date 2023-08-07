using Infraestructure.Interfaces;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infraestructure.Repository
{
    public class RepositoryProducto : IRepositoryProducto
    {
        public bool Delete(int id)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;


                    using (var dbContextTransaction = ctx.Database.BeginTransaction())
                    {
                        try
                        {
                            // Obtener el producto a eliminar
                            Producto producto = ctx.Producto.Find(id);

                            if (producto != null)
                            {
                                // Consultar las fotos asociadas al producto y eliminarlas
                                var fotosAsociadas = ctx.Foto.Where(f => f.productoId == id).ToList();
                                if (fotosAsociadas.Count > 0)
                                {
                                    ctx.Foto.RemoveRange(fotosAsociadas);
                                }

                                // Eliminar el producto
                                ctx.Producto.Remove(producto);

                                // Guardar los cambios en la base de datos
                                int filasAfectadas = ctx.SaveChanges();

                                // Confirmar la transacción
                                dbContextTransaction.Commit();

                                // Devuelve true si al menos una fila fue afectada
                                return filasAfectadas > 0;
                            }
                            else
                            {
                                // El producto no existe
                                return false;
                            }
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
            catch (Exception ex)
            {
                // Manejar el error si ocurre al abrir la conexión con la base de datos o al crear la transacción
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<Producto> GetProductoByCategoria(int categoriaId)
        {
            IEnumerable<Producto> oProducto;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;


                    oProducto = ctx.Producto
                        .Include("EstadoProducto")
                        .Include("Usuario")
                        .Include("Categoria")
                        .Include("Foto")
                        .Where(p => p.categoriaId == categoriaId).ToList();
                }


                return oProducto;
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

        public Producto GetProductoById(int id)
        {
            Producto oProducto;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;


                    oProducto = ctx.Producto
                        .Include("EstadoProducto")
                        .Include("Usuario")
                        .Include("Categoria")
                        .Include("Foto")
                        .FirstOrDefault(p => p.id == id);
                }


                return oProducto;
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

        public IEnumerable<Producto> GetProductos()
        {
            IEnumerable<Producto> products;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    products = ctx.Producto.Include("EstadoProducto").Include("Usuario").Include("Categoria").Include("Foto").ToList();
                }

                return products;
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

        public IEnumerable<Producto> GetProductosByVendedor(int vendedorId)
        {
            IEnumerable<Producto> oProducto;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;


                    oProducto = ctx.Producto
                        .Include("EstadoProducto")
                        .Include("Usuario")
                        .Include("Categoria")
                        .Where(p => p.vendedorId == vendedorId).ToList();
                }


                return oProducto;
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

        public Producto Save(Producto producto, List<HttpPostedFileBase> imageFiles)
        {
            Producto savedProducto = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        if (producto.id == 0)
                        {
                            // Guardar un nuevo producto
                            ctx.Producto.Add(producto);
                        }
                        else
                        {
                            // Editar un producto existente
                            var existingProducto = ctx.Producto
                                .Include(p => p.Foto) // Cargar las fotos asociadas al producto
                                .FirstOrDefault(p => p.id == producto.id);

                            if (existingProducto != null)
                            {
                                // Actualizar los datos del producto
                                existingProducto.nombre = producto.nombre;
                                existingProducto.descripcion = producto.descripcion;
                                existingProducto.precio = producto.precio;
                                existingProducto.cantidad = producto.cantidad;
                                existingProducto.categoriaId = producto.categoriaId;
                                existingProducto.estadoId = producto.estadoId;

                                // Eliminar las fotos existentes asociadas al producto
                                if (imageFiles[0] != null)
                                {
                                    ctx.Foto.RemoveRange(existingProducto.Foto);
                                }

                            }
                            else
                            {
                                // No se encontró el producto existente, se puede manejar el error o tomar otra acción
                            }
                        }

                        ctx.SaveChanges(); // Guardar el producto y obtener el ID generado


                        // Guardar las nuevas fotos asociadas al producto
                        if (imageFiles != null && imageFiles.Count > 0)
                        {
                            foreach (var imageFile in imageFiles)
                            {
                                if (imageFile != null)
                                {
                                    // Procesar el archivo de imagen y guardarla en la entidad Foto
                                    byte[] imageBytes;
                                    using (var memoryStream = new MemoryStream())
                                    {
                                        imageFile.InputStream.CopyTo(memoryStream);
                                        imageBytes = memoryStream.ToArray();
                                    }

                                    var foto = new Foto
                                    {
                                        foto1 = imageBytes,
                                        productoId = producto.id // Asignar el valor de productoId
                                    };

                                    // Guardar la foto en la base de datos
                                    ctx.Foto.Add(foto);
                                }
                            }

                            ctx.SaveChanges(); // Guardar los cambios en la base de datos
                        }

                        dbContextTransaction.Commit();

                        savedProducto = GetProductoById(producto.id); // Obtener el producto guardado con todas las asociaciones actualizadas
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

            return savedProducto;
        }
    }
}
