using System;
using System.Collections.Generic;
using System.Text;
using Farmacia.COMMON.Entidades;

namespace Farmacia.COMMON.Interfaces
{
    public interface IRepositorio<T> where T:Base
    {
        bool Create(T entidad);
        List<T> Read { get; }
        bool Update(T entidadModificada);
        bool Delete(string id);
    }
}
