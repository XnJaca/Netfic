using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IServiceTelefono
    {
        IEnumerable<Telefono> GetTelefonos();

        IEnumerable<Telefono> GetTelefonoByIdUser(int userId);
        Telefono GetTelefonoById(int id);
        Telefono Save(String idUsuario, String numero, String tipoTelefono);


    }
}
