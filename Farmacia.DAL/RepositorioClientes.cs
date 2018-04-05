using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacia.COMMON.Entidades;
using Farmacia.COMMON.Interfaces;
using LiteDB;

namespace Farmacia.DAL
{
    public class RepositorioClientes : IRepositorio<Clientes>
    {
        private string DBName = "Farmacia.db";
        private string TableName = "Clientes";
        public List<Clientes> Read {
            get
            {
                List<Clientes> datos = new List<Clientes>();
                using (var db = new LiteDatabase(DBName))
                {
                    datos = db.GetCollection<Clientes>(TableName).FindAll().ToList();
                }
                return datos;
            }
        }

        public bool Create(Clientes entidad)
        {
            entidad.Id = Guid.NewGuid().ToString();
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Clientes>(TableName);
                    coleccion.Insert(entidad);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                int r;
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Clientes>(TableName);
                    r = coleccion.Delete(e => e.Id == id);
                }
                return r > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Clientes entidadModificada)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Clientes>(TableName);
                    coleccion.Update(entidadModificada);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
