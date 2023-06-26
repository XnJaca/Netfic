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
            Usuario oUsuario = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                IRepositoryTipoUsuario _RepositoryTipoUsuario = new RepositoryTipoUsuario();

                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        if (usuario.id == 0)
                        {
                            // Nuevo usuario
                            if (pTipoUsuarios != null)
                            {
                                usuario.TipoUsuario = new List<TipoUsuario>();

                                foreach (var tipoUsuarioId in pTipoUsuarios)
                                {
                                    var tipoUsuario = _RepositoryTipoUsuario.GetTipoUsuarioById(int.Parse(tipoUsuarioId));
                                    usuario.TipoUsuario.Add(tipoUsuario);
                                }
                            }

                            ctx.Usuario.Add(usuario);
                            ctx.SaveChanges(); // Guardar el usuario para obtener el ID generado

                            // Agregar los registros de Telefono
                            if (pTelefonos != null)
                            {
                                foreach (var telefonoString in pTelefonos)
                                {
                                    var phones = telefonoString.Split(',');
                                    foreach (var phone in phones)
                                    {
                                        var cleanedPhone = phone.Trim(); // Eliminar espacios en blanco
                                        var commaIndex = cleanedPhone.IndexOf('('); // Obtener índice de la primera aparición de "("
                                        if (commaIndex >= 0)
                                        {
                                            var numeroTelefono = cleanedPhone.Substring(0, commaIndex); // Extraer número de teléfono antes del "("
                                            var tipoTelefono = cleanedPhone.Substring(commaIndex + 1, cleanedPhone.Length - commaIndex - 2); // Extraer palabra entre paréntesis

                                            var telefono = new Telefono
                                            {
                                                numero = Int32.Parse(numeroTelefono),
                                                tipoTelefono = tipoTelefono,
                                                usuarioId = usuario.id // Asignar el valor de usuarioId
                                            };

                                            ctx.Telefono.Add(telefono);
                                        }
                                    }
                                }

                                ctx.SaveChanges(); // Guardar los teléfonos asociados al nuevo usuario
                            }
                        }
                        else
                        {
                            // Usuario existente
                            var existingUsuario = ctx.Usuario
                                .Include(u => u.TipoUsuario)
                                .Include(u => u.Telefono)
                                .FirstOrDefault(u => u.id == usuario.id);

                            if (existingUsuario != null)
                            {
                                existingUsuario.nombre = usuario.nombre;
                                existingUsuario.apellidos = usuario.apellidos;

                                // Eliminar los registros existentes de "Telefono" asociados al usuario
                                ctx.Telefono.RemoveRange(existingUsuario.Telefono);

                                // Asignar nuevos valores de TipoUsuario
                                existingUsuario.TipoUsuario.Clear();
                                if (pTipoUsuarios != null)
                                {
                                    foreach (var tipoUsuarioId in pTipoUsuarios)
                                    {
                                        var tipoUsuario = _RepositoryTipoUsuario.GetTipoUsuarioById(int.Parse(tipoUsuarioId));
                                        existingUsuario.TipoUsuario.Add(tipoUsuario);
                                    }
                                }

                                // Agregar nuevos registros de Telefono
                                if (pTelefonos != null)
                                {
                                    foreach (var telefonoString in pTelefonos)
                                    {
                                        var phones = telefonoString.Split(',');
                                        foreach (var phone in phones)
                                        {
                                            var cleanedPhone = phone.Trim(); // Eliminar espacios en blanco
                                            var commaIndex = cleanedPhone.IndexOf('('); // Obtener índice de la primera aparición de "("
                                            if (commaIndex >= 0)
                                            {
                                                var numeroTelefono = cleanedPhone.Substring(0, commaIndex); // Extraer número de teléfono antes del "("
                                                var tipoTelefono = cleanedPhone.Substring(commaIndex + 1, cleanedPhone.Length - commaIndex - 2); // Extraer palabra entre paréntesis

                                                var telefono = new Telefono
                                                {
                                                    numero = Int32.Parse(numeroTelefono),
                                                    tipoTelefono = tipoTelefono,
                                                    usuarioId = usuario.id // Asignar el valor de usuarioId
                                                };

                                                ctx.Telefono.Add(telefono);
                                            }
                                        }
                                    }
                                }

                                ctx.SaveChanges();
                                retorno = ctx.SaveChanges();
                            }
                        }

                        dbContextTransaction.Commit();

                        if (retorno >= 0)
                        {
                            oUsuario = GetUsuarioById((int)usuario.id);
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
            return oUsuario;
        }


    }
}
