using System;
using System.Collections.Generic;
using System.Text;

namespace Farmacia.COMMON.Entidades
{
    public class Producto
    {
        public string NombreProducto { get; set; }
        public string PrecioVenta { get; set; }
        public string PrecioCompra { get; set; }
        public string Descripcion { get; set; }
        public string Presentacion { get; set; }
        public string Categoria { get; set; }
    }
}
