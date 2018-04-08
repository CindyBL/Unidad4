using System;
using System.Collections.Generic;
using System.Text;

namespace Farmacia.COMMON.Entidades
{
    public class Empleado:Base
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NoEmpleado { get; set; }
        public override string ToString()
        {
            return string.Format("{0} {1}", Nombre, Apellido);
        }

        public static implicit operator string(Empleado v)
        {
            throw new NotImplementedException();
        }
    }
}
