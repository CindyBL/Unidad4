using Farmacia.COMMON.Entidades;
using Farmacia.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacia.BIZ
{
    public class ManejadorDeClientes : IManejadorClientes
    {
        IRepositorio<Clientes> repositorio;
        public ManejadorDeClientes(IRepositorio<Clientes> repositorio)
        {
            this.repositorio = repositorio;
        }

        public List<Clientes> Listar => repositorio.Read;

        public bool Agregar(Clientes entidad)
        {
            return repositorio.Create(entidad);
        }

        public Clientes BuscarPorId(string id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(string id)
        {
            return repositorio.Delete(id);
        }

        public bool Modificar(Clientes entidad)
        {
            return repositorio.Update(entidad);
        }
    }
}
