using System;
using System.Collections.Generic;
using System.Text;

namespace Farmacia.COMMON.Entidades
{
    public class Clientes:Base
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string RFC { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }
}
