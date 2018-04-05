using Farmacia.COMMON.Entidades;
using Farmacia.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Farmacia.BIZ
{
    public class ManejadorDeCategorias : IManejadorCategorias
    {
        IRepositorio<Categorias> repositorio;

        public ManejadorDeCategorias(IRepositorio<Categorias> repo)
        {
            repositorio = repo;
        }

        public List<Categorias> Listar => repositorio.Read;

        public bool Agregar(Categorias entidad)
        {
            return repositorio.Create(entidad);
        }

        public Categorias BuscarPorId(string id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(string id)
        {
            return repositorio.Delete(id);
        }

        public bool Modificar(Categorias entidad)
        {
            return repositorio.Update(entidad);
        }
    }
}
