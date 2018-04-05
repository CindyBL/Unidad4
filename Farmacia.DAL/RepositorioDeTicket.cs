using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacia.COMMON.Entidades;
using Farmacia.COMMON.Interfaces;
using LiteDB;

namespace Farmacia.DAL
{
    public class RepositorioDeTicket : IRepositorio<Ticket>
    {
        private string DBName = "Inventario.db";
        private string TableName = "Ticket";
        public List<Ticket> Read
        {
            get
            {
                List<Ticket> datos = new List<Ticket>();
                using (var db = new LiteDatabase(DBName))
                {
                    datos = db.GetCollection<Ticket>(TableName).FindAll().ToList();
                }
                return datos;
            }
        }

        public bool Create(Ticket entidad)
        {
            entidad.Id = Guid.NewGuid().ToString();
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Ticket>(TableName);
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
                    var coleccion = db.GetCollection<Ticket>(TableName);
                    r = coleccion.Delete(e => e.Id == id);
                }
                return r > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Ticket entidadModificada)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Ticket>(TableName);
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
