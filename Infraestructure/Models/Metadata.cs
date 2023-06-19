using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    internal partial class Metadata
    {
    }


    internal partial class UsuarioMetadata
    {

        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string nombre { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string apellidos { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string email { get; set; }
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string password { get; set; }
        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int estado { get; set; }
    }

    internal partial class TelefonoMetaData
    {
        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Id Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int usuarioId { get; set; }
        [Display(Name = "Numero de Telefono")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int numero { get; set; }

        [Display(Name = "Tipo de Telefono")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string tipoTelefono { get; set; }
    }
}
