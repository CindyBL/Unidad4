using System;
using System.Collections.Generic;
using System.Text;
using Farmacia.COMMON.Entidades;

namespace Farmacia.COMMON.Interfaces
{
    public interface IManejadorGenerico<T> where T : Base
    {
        bool Agregar(T entidad);
        List<T> Listar { get; }
        bool Eliminar(string id);
        bool Modificar(T entidad);
        T BuscarPorId(string id);
    }
}
