using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infraestructure.Models
{
    public class ProvinciaData
    {
        public string nombre { get; set; }
        public Dictionary<string, CantonData> cantones { get; set; }
    }
}