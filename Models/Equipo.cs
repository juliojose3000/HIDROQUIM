using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Models
{
    public class Equipo
    {
        private Producto producto;
        private int cantidadDisponible;
        private int idEquipo;
        public Equipo()
        {
            this.producto = new Producto();
        }

        public Producto Producto { get => producto; set => producto = value; }
        public int CantidadDisponible { get => cantidadDisponible; set => cantidadDisponible = value; }
        public int IdEquipo { get => idEquipo; set => idEquipo = value; }
    }
}