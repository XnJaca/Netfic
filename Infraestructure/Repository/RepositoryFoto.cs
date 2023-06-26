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
    public class RepositoryFoto : IRepositoryFoto
    {
        public bool Delete(int idProducto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Foto> GetFotosByProducto(int idProducto)
        {
            IEnumerable<Foto> fotos;


            try
            {
                using (MyContext ctx = new MyContext())
                {

                    ctx.Configuration.LazyLoadingEnabled = false;

                    fotos = ctx.Foto.Where(f => f.productoId == idProducto).ToList();
                }
                return fotos;
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

        public Foto Save(Foto foto)
        {
            throw new NotImplementedException();
        }
    }
}
