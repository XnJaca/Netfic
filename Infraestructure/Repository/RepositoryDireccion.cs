using Infraestructure.Interfaces;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryDireccion : IRepositoryDireccion
    {
        public bool Delete(int id)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    // Obtener la categoría a eliminar
                    Direccion direccion = ctx.Direccion.Find(id);

                    if (direccion != null)
                    {
                        ctx.Direccion.Remove(direccion);
                        int filasAfectadas = ctx.SaveChanges();

                        // Devuelve true si al menos una fila fue afectada
                        return filasAfectadas > 0;
                    }
                    else
                    {
                        // La categoría no existe
                        return false;
                    }
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

        public Direccion GetDireccionById(int id)
        {
            Direccion oDireccion;

            try
            {
                using (MyContext ctx = new MyContext())
                {

                    ctx.Configuration.LazyLoadingEnabled = false;

                    oDireccion = ctx.Direccion.Find(id);
                }

                return oDireccion;
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

        public IEnumerable<Direccion> GetDireccionByIdUsuario(int idUsuario)
        {
            IEnumerable<Direccion> oDireccion;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;


                    oDireccion = ctx.Direccion.Where(p => p.idUsuario == idUsuario).ToList();
                }


                return oDireccion;
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

        public IEnumerable<Direccion> GetDireccions()
        {
            throw new NotImplementedException();
        }

        public Direccion Save(Direccion direccion)
        {
            int retorno;

            Direccion oDireccion;

            using (MyContext ctx = new MyContext())
            {

                ctx.Configuration.LazyLoadingEnabled = false;

                oDireccion = ctx.Direccion.Find((int)direccion.id);

                if (oDireccion == null)
                {
                    ctx.Direccion.Add(direccion);
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    ctx.Direccion.Add(oDireccion);
                    ctx.Entry(oDireccion).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oDireccion = GetDireccionById((int)direccion.id);


                return oDireccion;



            }
        }
    }
}
