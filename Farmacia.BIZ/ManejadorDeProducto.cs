using Farmacia.COMMON.Entidades;
using Farmacia.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Farmacia.BIZ
{
    public class ManejadorDeProducto : IManejadorProducto
    {
        IRepositorio<Producto> repositorio;
        public ManejadorDeProducto(IRepositorio<Producto> repositorio)
        {
            this.repositorio = repositorio;
        }

        public List<Producto> Listar => repositorio.Read;

        public bool Agregar(Producto entidad)
        {
            return repositorio.Create(entidad);
        }

        public Producto BuscarPorId(string id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(string id)
        {
            return repositorio.Delete(id);
        }

        public bool Modificar(Producto entidad)
        {
            return repositorio.Update(entidad);
        }

        public List<Producto> ProductosDeCategoria(string categoria)
        {
            return Listar.Where(e => e.Categoria == categoria ).ToList();
        }
    }
}
