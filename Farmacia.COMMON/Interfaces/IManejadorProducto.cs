using Farmacia.COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Farmacia.COMMON.Interfaces
{
    public interface IManejadorProducto : IManejadorGenerico<Producto>
    {
        List<Producto> ProductosDeCategoria(string categoria);
    }
}
