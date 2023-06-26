using Infraestructure.Interfaces;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryEstadoProducto : IRepositoryEstadoProducto
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public EstadoProducto GetEstadoProductoByID(int id)
        {
            EstadoProducto estadoProducto;

            try
            {
                using (MyContext ctx = new MyContext())
                {

                    ctx.Configuration.LazyLoadingEnabled = false;

                    estadoProducto = ctx.EstadoProducto.Find(id);
                }

                return estadoProducto;
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

        public IEnumerable<EstadoProducto> GetEstadoProductos()
        {
            IEnumerable<EstadoProducto> estadoProductos;

            try
            {
                using (MyContext ctx = new MyContext())
                {

                    ctx.Configuration.LazyLoadingEnabled = false;

                    estadoProductos = ctx.EstadoProducto.ToList();
                }
                return estadoProductos;
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

        public EstadoProducto Save(EstadoProducto estadoProducto)
        {
            throw new NotImplementedException();
        }
    }
}
