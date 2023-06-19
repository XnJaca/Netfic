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
    public class RepositoryTipoUsuario : IRepositoryTipoUsuario
    {
        public TipoUsuario GetTipoUsuarioById(int id)
        {
            TipoUsuario oTipoUsuario = null;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oTipoUsuario = ctx.TipoUsuario
                    .Where(p => p.id == id).
                    FirstOrDefault<TipoUsuario>();
                }
                return oTipoUsuario;
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

        public IEnumerable<TipoUsuario> GetTipoUsuarios()
        {
            IEnumerable<TipoUsuario> lista = null;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.TipoUsuario.ToList();
                }

                return lista;
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

        public TipoUsuario Save(string descripcion)
        {
            throw new NotImplementedException();
        }
    }
}
