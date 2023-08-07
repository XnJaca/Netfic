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
    public class RepositoryMetodoPago : IRepositoryMetodoPago
    {
        public bool Delete(int id)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    // Obtener la categoría a eliminar
                    MetodoPago metodoPago = ctx.MetodoPago.Find(id);

                    if (metodoPago != null)
                    {
                        ctx.MetodoPago.Remove(metodoPago);
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

        public MetodoPago GetMetodoPagoById(int id)
        {
            MetodoPago oMetodoPago;

            try
            {
                using (MyContext ctx = new MyContext())
                {

                    ctx.Configuration.LazyLoadingEnabled = false;

                    oMetodoPago = ctx.MetodoPago.Find(id);
                }

                return oMetodoPago;
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

        public IEnumerable<MetodoPago> GetMetodoPagoByIdUsuario(int idUsuario)
        {
            IEnumerable<MetodoPago> oMetodoPago;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;


                    oMetodoPago = ctx.MetodoPago.Where(p => p.usuarioId == idUsuario).ToList();
                }


                return oMetodoPago;
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

        public IEnumerable<MetodoPago> GetMetodoPagos()
        {
            throw new NotImplementedException();
        }

        public MetodoPago Save(MetodoPago metodoPago)
        {
            int retorno;

            MetodoPago oMetodoPago;

            using (MyContext ctx = new MyContext())
            {

                ctx.Configuration.LazyLoadingEnabled = false;

                oMetodoPago = ctx.MetodoPago.Find((int)metodoPago.id);

                if (oMetodoPago == null)
                {
                    ctx.MetodoPago.Add(metodoPago);
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    ctx.MetodoPago.Add(oMetodoPago);
                    ctx.Entry(oMetodoPago).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oMetodoPago = GetMetodoPagoById((int)metodoPago.id);


                return oMetodoPago;



            }
        }
    }
}
