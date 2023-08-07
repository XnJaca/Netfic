using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infraestructure.Models
{
    public class CantonData
    {
        public string nombre { get; set; }
        public Dictionary<string, string> distritos { get; set; }
    }
}