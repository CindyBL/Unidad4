﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Farmacia.COMMON.Entidades;
using Farmacia.COMMON.Interfaces;
using LiteDB;

namespace Farmacia.DAL
{
    public class RepositorioDeCategorias : IRepositorio<Categorias>
    {
        private string DBName = "Farmacia.db";
        private string TableName = "Categorias";

        public List<Categorias> Read
        {
            get
            {
                List<Categorias> datos = new List<Categorias>();
                using (var db = new LiteDatabase(DBName))
                {
                    datos = db.GetCollection<Categorias>(TableName).FindAll().ToList();
                }
                return datos;
            }
        }

        public bool Create(Categorias entidad)
        {
            entidad.Id = Guid.NewGuid().ToString();
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Categorias>(TableName);
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
                    var coleccion = db.GetCollection<Categorias>(TableName);
                    r = coleccion.Delete(e => e.Id == id);
                }
                return r > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Categorias entidadModificada)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Categorias>(TableName);
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
