using System;
using System.Collections.Generic;
using System.Text;

namespace Farmacia.COMMON.Entidades
{
    public class Ticket
    {
        public DateTime FechaHora { get; set; }
        public List<Producto> Productos { get; set; }
        public string Usuario { get; set; }
        public string Total { get; set; }
    }
}
