using System;
using System.Collections.Generic;
using System.Text;

namespace Farmacia.COMMON.Entidades
{
    public class Producto:Base
    {
        public string NombreProducto { get; set; }
        public string PrecioVenta { get; set; }
        public string PrecioCompra { get; set; }
        public string Descripcion { get; set; }
        public string Presentacion { get; set; }
        public string Categoria { get; set; }
        public override string ToString()
        {
            return string.Format("{0}", NombreProducto);
        }

        public static implicit operator string(Producto v)
        {
            throw new NotImplementedException();
        }
    }
}
