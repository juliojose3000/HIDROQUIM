using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Models
{
    public class Rol
    {
        public int idRol;
        public string nombreRol;

        public Rol()
        {
        }

        public Rol(int idRol, string nombreRol)
        {
            this.IdRol = idRol;
            this.NombreRol = nombreRol;
        }

        public int IdRol { get => idRol; set => idRol = value; }
        public string NombreRol { get => nombreRol; set => nombreRol = value; }
    }
}