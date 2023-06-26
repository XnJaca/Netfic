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
    public class RepositoryCategoria : IRepositoryCategoria
    {
        public bool Delete(int id)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    // Obtener la categoría a eliminar
                    Categoria categoria = ctx.Categoria.Find(id);

                    if (categoria != null)
                    {
                        ctx.Categoria.Remove(categoria);
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


        public Categoria GetCategoriaById(int id)
        {
            Categoria oCategoria;

            try
            {
                using (MyContext ctx = new MyContext())
                {

                    ctx.Configuration.LazyLoadingEnabled = false;

                    oCategoria = ctx.Categoria.Find(id);
                }

                return oCategoria;
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

        public IEnumerable<Categoria> GetCategorias()
        {
            IEnumerable<Categoria> oCategorias;

            try
            {
                using (MyContext ctx = new MyContext())
                {

                    ctx.Configuration.LazyLoadingEnabled = false;

                    oCategorias = ctx.Categoria.ToList();
                }
                return oCategorias;
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

        public Categoria Save(Categoria categoria)
        {
            int retorno;

            Categoria oCategoria;

            using (MyContext ctx = new MyContext())
            {

                ctx.Configuration.LazyLoadingEnabled = false;

                oCategoria = GetCategoriaById((int)categoria.id);

                if (oCategoria == null)
                {
                    ctx.Categoria.Add(categoria);
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    ctx.Categoria.Add(categoria);
                    ctx.Entry(categoria).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oCategoria = GetCategoriaById((int)categoria.id);


                return oCategoria;



            }
        }
    }
}
