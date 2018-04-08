using System;
using System.Collections.Generic;
using System.Text;

namespace Farmacia.COMMON.Entidades
{
    public class Categorias:Base
    {
        public string nombreCategoria { get; set; }
        public override string ToString()
        {
            return string.Format("{0}", nombreCategoria);
        }
    }
}
