using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionWebNomina1.Models
{
    public class Usuario
    {
        public int id { get; set; }
        public string user { get; set; }
        public string passw { get; set; }
        public string documento { get; set; }
        public string fechaNaci { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string gender { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }

    }
}