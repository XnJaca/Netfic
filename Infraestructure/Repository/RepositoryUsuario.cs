using Infraestructure.Interfaces;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryUsuario : IRepositoryUsuario
    {
        public Usuario GetUsuarioById(int id)
        {
            Usuario oUsuario = null;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = ctx.Usuario.Include("TipoUsuario").Include("Telefono")
                    .Where(p => p.id == id).
                    FirstOrDefault<Usuario>();
                }
                return oUsuario;
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

        public IEnumerable<Usuario> GetUsuarios()
        {
            IEnumerable<Usuario> lista = null;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Usuario.Include("TipoUsuario").Include("Telefono").ToList();
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

        public Usuario Login(string email, string password)
        {
            Usuario oUsuario = null;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = ctx.Usuario.Where(p => p.email == email &&
                        p.password == password).FirstOrDefault<Usuario>();

                    if (oUsuario != null)
                        oUsuario = GetUsuarioById(oUsuario.id);

                    return oUsuario;
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

        public Usuario Logout()
        {
            throw new NotImplementedException();
        }

        public Usuario Save(Usuario usuario, string[] pTipoUsuarios, string[] pTelefonos)
        {
            int retorno = 0;

            Usuario oUsuario;

            using (MyContext ctx = new MyContext())
            {

                ctx.Configuration.LazyLoadingEnabled = false;
                oUsuario = GetUsuarioById((int)usuario.id);

                IRepositoryTipoUsuario _RepositoryTipoUsuario = new RepositoryTipoUsuario();
                IRepositoryTelefono _RepositoryTelefono = new RepositoryTelefono();



                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {

                    try
                    {
                        if (oUsuario == null)
                        {

                            if (pTipoUsuarios != null)
                            {
                                usuario.TipoUsuario = new List<TipoUsuario>();

                                foreach (var tipoUsuario in pTipoUsuarios)
                                {
                                    var tipoUsuarioAdd = _RepositoryTipoUsuario.GetTipoUsuarioById(int.Parse(tipoUsuario));
                                    ctx.TipoUsuario.Attach(tipoUsuarioAdd);
                                    usuario.TipoUsuario.Add(tipoUsuarioAdd);
                                }
                            }

                            ctx.Usuario.Add(usuario);
                            retorno = ctx.SaveChanges();

                            // Obtiene el ID del nuevo usuario.
                            int nuevoUsuarioId = usuario.id;


                            if (pTelefonos != null)
                            {
                                usuario.Telefono = new List<Telefono>();

                                foreach (var telefonoString in pTelefonos)
                                {
                                    var phones = telefonoString.Split(',');
                                    foreach (var phone in phones)
                                    {
                                        var phonePart = phone.Split('-');
                                        var telefono = new Telefono
                                        {
                                            usuarioId = (int)usuario.id,
                                            numero = Int32.Parse(phonePart[0]),
                                            tipoTelefono = phonePart[1]
                                        };
                                        ctx.Telefono.Attach(telefono);
                                        usuario.Telefono.Add(telefono);
                                    }

                                }
                            }

                            //ctx.Usuario.Add(usuario);
                            //ctx.Entry(usuario).State = EntityState.Modified;
                            //retorno = ctx.SaveChanges();

                            //ctx.Usuario.Add(usuario);
                            //ctx.Entry(usuario).State = EntityState.Modified;
                            retorno = ctx.SaveChanges();
                        }
                        else
                        {
                            ctx.Usuario.Add(usuario);
                            ctx.Entry(usuario).State = EntityState.Modified;
                            retorno = ctx.SaveChanges();

                            var tipoUsuariosId = new HashSet<string>(pTipoUsuarios);
                            var telefonos = new HashSet<string>(pTelefonos);

                            if (pTipoUsuarios != null)
                            {
                                ctx.Entry(usuario).Collection(p => p.TipoUsuario).Load();
                                var newTipoUsuarioForUsuario = ctx.TipoUsuario.Where(x => pTipoUsuarios.Contains(x.id.ToString())).ToList();
                                usuario.TipoUsuario = newTipoUsuarioForUsuario;

                                ctx.Entry(usuario).Collection(p => p.Telefono).Load();

                                // Eliminar los registros existentes de "Telefono" asociados al usuario
                                usuario.Telefono.Clear();

                                foreach (var telefonoString in pTelefonos)
                                {
                                    var phones = telefonoString.Split(',');
                                    foreach (var phone in phones)
                                    {
                                        var phonePart = phone.Split('-');
                                        var telefono = new Telefono
                                        {
                                            usuarioId = oUsuario.id,
                                            numero = Int32.Parse(phonePart[0]),
                                            tipoTelefono = phonePart[1]
                                        };
                                        usuario.Telefono.Add(telefono);
                                        ctx.Telefono.Add(telefono);
                                    }
                                }

                                ctx.Entry(usuario).State = EntityState.Modified;
                                retorno = ctx.SaveChanges();
                            }

                            //if (pTelefonos != null)
                            //{
                                
                            //}
                            //retorno = ctx.SaveChanges();

                        }

                        dbContextTransaction.Commit();

                        if (retorno >= 0)
                        {
                            oUsuario = GetUsuarioById((int)usuario.id);
                        }


                        //dbCon
                    }
                    catch (Exception ex) { }

                }

            }
            return oUsuario;
        }

    }
}
