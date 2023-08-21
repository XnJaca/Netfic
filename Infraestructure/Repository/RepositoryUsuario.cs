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
using System.Text.RegularExpressions;
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
                    oUsuario = ctx.Usuario.Include("TipoUsuario").Include("Telefono").Include(m => m.MetodoPago).Include(p => p.Direccion)
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

        public IEnumerable<Usuario> GetUsuarioDeshabilitados()
        {
            IEnumerable<Usuario> lista = null;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Usuario
                    .Include("TipoUsuario")
                    .Include("Telefono")
                    .Where(u => !u.estado) // Filtrar solo los usuarios con estado igual a true (1)
                    .ToList();
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

        public IEnumerable<Usuario> GetUsuarios()
        {
            IEnumerable<Usuario> lista = null;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Usuario
                    .Include("TipoUsuario")
                    .Include("Telefono")
                    .Where(u => u.estado) // Filtrar solo los usuarios con estado igual a true (1)
                    .ToList();
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
            Usuario usuarioSaved = null;

            using (var ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                IRepositoryTipoUsuario _RepositoryTipoUsuario = new RepositoryTipoUsuario();

                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {

                    try
                    {
                        if (usuario.id == 0)
                        {
                            // Paso 1: Agregar el nuevo usuario al contexto (si es nuevo, el id será 0)
                            ctx.Usuario.Add(usuario);

                            // Paso 2: Cargar los tipos de usuario asociados al usuario nuevo
                            var tipoUsuariosId = new HashSet<string>(pTipoUsuarios);

                            if (tipoUsuariosId != null)
                            {
                                var newTipoUsuarioForUser = ctx.TipoUsuario
                                    .Where(x => tipoUsuariosId.Contains(x.id.ToString()))
                                    .ToList();
                                usuario.TipoUsuario = newTipoUsuarioForUser;
                            }

                            // Paso 3: Agregar los nuevos teléfonos al usuario nuevo
                            foreach (var item in pTelefonos)
                            {
                                var phones = item.Split(',');
                                var numeroTelefono = int.Parse(phones[0]);
                                var tipoTelefono = phones[1].Trim();

                                var nuevoTelefono = new Telefono
                                {
                                    numero = numeroTelefono,
                                    tipoTelefono = tipoTelefono,
                                };

                                usuario.Telefono.Add(nuevoTelefono);
                            }

                            // Paso 4: Guardar los cambios en el contexto (Usuario, TipoUsuario, Teléfonos)
                            ctx.SaveChanges();

                        }
                        else
                        {

                            ctx.Usuario.Add(usuario);
                            ctx.Entry(usuario).State = EntityState.Modified;
                            ctx.SaveChanges();


                            var tipoUsuariosId = new HashSet<string>(pTipoUsuarios);

                            if (tipoUsuariosId != null)
                            {
                                ctx.Entry(usuario).Collection(p => p.TipoUsuario).Load();
                                var newTipoUsuarioForUser = ctx.TipoUsuario
                                .Where(x => tipoUsuariosId.Contains(x.id.ToString())).ToList();
                                usuario.TipoUsuario = newTipoUsuarioForUser;

                                ctx.Entry(usuario).State = EntityState.Modified;
                                ctx.SaveChanges();
                            }

                            // Eliminar los teléfonos asociados al usuario existente
                            var existingUsuario = ctx.Usuario
                                .Include(p => p.Telefono)
                                .FirstOrDefault(p => p.id == usuario.id);

                            if (existingUsuario != null)
                            {
                                ctx.Telefono.RemoveRange(existingUsuario.Telefono);
                            }

                            // Agregar los nuevos teléfonos al usuario existente
                            //foreach (var item in pTelefonos)
                            //{
                            //    var phones = item.Split(',');
                            //    var numeroTelefono = int.Parse(phones[0]);
                            //    var tipoTelefono = phones[1].Trim();

                            //    var nuevoTelefono = new Telefono
                            //    {
                            //        numero = numeroTelefono,
                            //        tipoTelefono = tipoTelefono,
                            //        usuarioId = usuario.id
                            //    };

                            //    usuario.Telefono.Add(nuevoTelefono);
                            //    ctx.Entry(usuario).State = EntityState.Modified;
                            //    ctx.SaveChanges();
                            //}

                            foreach (var item in pTelefonos)
                            {
                                var phones = item.Split(',');
                                foreach (var phone in phones)
                                {
                                    var trimmedPhone = phone.Trim();

                                    if (!string.IsNullOrWhiteSpace(trimmedPhone))
                                    {
                                        var numeroTelefono = 0;
                                        var tipoTelefono = string.Empty;

                                        // Usar expresiones regulares para obtener el número de teléfono y tipo de teléfono
                                        var regex = new Regex(@"(\d+)\s*\((\w+)\)");
                                        var match = regex.Match(trimmedPhone);

                                        if (match.Success)
                                        {
                                            // Obtener el número de teléfono y tipo de teléfono con paréntesis
                                            if (int.TryParse(match.Groups[1].Value, out int numeroTelefonoParsed))
                                            {
                                                numeroTelefono = numeroTelefonoParsed;
                                            }
                                            tipoTelefono = match.Groups[2].Value;
                                        }
                                        else
                                        {
                                            // Si no coincide con el formato "Número (Tipo)", dividir el item por ,
                                            var phoneParts = trimmedPhone.Split(',');
                                            if (phoneParts.Length >= 2 && int.TryParse(phoneParts[0], out int numeroTelefonoParsed))
                                            {
                                                numeroTelefono = numeroTelefonoParsed;
                                                tipoTelefono = phoneParts[1].Trim();
                                            }
                                        }

                                        // Crear el nuevo teléfono y agregarlo al usuario
                                        var nuevoTelefono = new Telefono
                                        {
                                            numero = numeroTelefono,
                                            tipoTelefono = tipoTelefono,
                                            usuarioId = usuario.id
                                        };

                                        usuario.Telefono.Add(nuevoTelefono);
                                        ctx.Entry(usuario).State = EntityState.Modified;
                                        ctx.SaveChanges();
                                    }
                                }


                            }
                        }

                        dbContextTransaction.Commit();

                        usuarioSaved = GetUsuarioById(usuario.id);
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
                return usuario;
            }
        }

        //public Usuario Save(Usuario usuario, string[] pTipoUsuarios, string[] pTelefonos)
        //{
        //    int retorno = 0;
        //    Usuario oUsuario = null;

        //    using (MyContext ctx = new MyContext())
        //    {
        //        ctx.Configuration.LazyLoadingEnabled = false;

        //        IRepositoryTipoUsuario _RepositoryTipoUsuario = new RepositoryTipoUsuario();
        //        IRepositoryTelefono _RepositoryTelefono = new RepositoryTelefono();

        //        using (var dbContextTransaction = ctx.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                if (usuario.id == 0)
        //                {
        //                    // Nuevo usuario
        //                    if (pTipoUsuarios != null)
        //                    {
        //                        usuario.TipoUsuario = new List<TipoUsuario>();

        //                        foreach (var tipoUsuarioId in pTipoUsuarios)
        //                        {
        //                            var tipoUsuario = _RepositoryTipoUsuario.GetTipoUsuarioById(int.Parse(tipoUsuarioId));
        //                            usuario.TipoUsuario.Add(tipoUsuario);
        //                        }
        //                    }

        //                    ctx.Usuario.Add(usuario);
        //                    ctx.SaveChanges(); // Guardar el usuario para obtener el ID generado

        //                    // Agregar los registros de Telefono
        //                    if (pTelefonos != null)
        //                    {
        //                        foreach (var telefonoString in pTelefonos)
        //                        {
        //                            var phones = telefonoString.Split(',');
        //                            foreach (var phone in phones)
        //                            {
        //                                var cleanedPhone = phone.Trim(); // Eliminar espacios en blanco
        //                                var commaIndex = cleanedPhone.IndexOf('('); // Obtener índice de la primera aparición de "("
        //                                if (commaIndex >= 0)
        //                                {
        //                                    var numeroTelefono = cleanedPhone.Substring(0, commaIndex); // Extraer número de teléfono antes del "("
        //                                    var tipoTelefono = cleanedPhone.Substring(commaIndex + 1, cleanedPhone.Length - commaIndex - 2); // Extraer palabra entre paréntesis

        //                                    var telefono = new Telefono
        //                                    {
        //                                        numero = Int32.Parse(numeroTelefono),
        //                                        tipoTelefono = tipoTelefono,
        //                                        usuarioId = usuario.id // Asignar el valor de usuarioId
        //                                    };

        //                                    ctx.Telefono.Add(telefono);
        //                                }
        //                            }
        //                        }

        //                        ctx.SaveChanges(); // Guardar los teléfonos asociados al nuevo usuario
        //                    }
        //                }
        //                else
        //                {

        //                    ctx.Usuario.Add(usuario);
        //                    ctx.Entry(usuario).State = EntityState.Modified;
        //                    retorno = ctx.SaveChanges();




        //                    if (pTelefonos != null && !pTelefonos[0].Equals(""))
        //                    {
        //                        // Cargamos los números de teléfono existentes del usuario desde la base de datos
        //                        ctx.Entry(usuario).Collection(u => u.Telefono).Load();

        //                        // Creamos un diccionario para mapear el número de teléfono existente con su entidad Telefono correspondiente
        //                        var telefonoExistenteDict = usuario.Telefono.ToDictionary(t => t.numero);

        //                        foreach (var item in pTelefonos)
        //                        {
        //                            var phones = item.Split(',');
        //                            var numeroTelefono = int.Parse(phones[0]);
        //                            var tipoTelefono = phones[1].Trim();

        //                            if (telefonoExistenteDict.ContainsKey(numeroTelefono))
        //                            {
        //                                // Si el número de teléfono ya existe en la base de datos, actualizamos su tipo
        //                                telefonoExistenteDict[numeroTelefono].tipoTelefono = tipoTelefono;
        //                            }
        //                            else
        //                            {
        //                                // Si el número de teléfono es nuevo, lo agregamos al usuario
        //                                var nuevoTelefono = new Telefono
        //                                {
        //                                    numero = numeroTelefono,
        //                                    tipoTelefono = tipoTelefono,
        //                                    usuarioId = usuario.id
        //                                };
        //                                ctx.Telefono.Add(nuevoTelefono);
        //                                usuario.Telefono.Add(nuevoTelefono);
        //                                retorno = ctx.SaveChanges();
        //                            }
        //                        }

        //                    }


        //                    if (retorno >= 0)
        //                    {
        //                        oUsuario = usuario;
        //                    }
        //                }
        //            }
        //            catch (DbUpdateException dbEx)
        //            {
        //                string mensaje = "";
        //                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
        //                throw new Exception(mensaje);
        //            }
        //            catch (Exception ex)
        //            {
        //                string mensaje = "";
        //                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
        //                throw;
        //            }
        //        }
        //    }
        //    return oUsuario;
        //}


    }
}
