using System;
using System.Collections.Generic;
using System.Text;

namespace Farmacia.COMMON.Entidades
{
    public class Ticket:Base
    {
        public DateTime FechaHora { get; set; }
        public Producto Productos { get; set; }
        public Clientes Cliente { get; set; }
        public Empleado Encargado { get; set; }
        public string cantidad { get; set; }
        public string Total { get; set; }
    }
}
