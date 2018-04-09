using System;
using System.Collections.Generic;
using System.Text;

namespace Farmacia.COMMON.Entidades
{
    public class Ticket:Base
    {
        public DateTime FechaHora { get; set; }
        public string Productos { get; set; }
        public string Cliente { get; set; }
        public string Encargado { get; set; }
        public string cantidad { get; set; }
        public string Total { get; set; }
    }
}
