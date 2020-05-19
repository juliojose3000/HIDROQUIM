using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Models
{
    public class Reactivo
    {
        private int idReactivo;
        private Producto producto;
        private string unidadMedida ;
        private float cantidadDisponible;
        private string estado;

        public Reactivo()
        {
            this.Producto = new Producto();
        }

        public Producto Producto { get => producto; set => producto = value; }
        public string UnidadMedida { get => unidadMedida; set => unidadMedida = value; }
        public string Estado { get => estado; set => estado = value; }
        public float CantidadDisponible { get => cantidadDisponible; set => cantidadDisponible = value; }
        public int IdReactivo { get => idReactivo; set => idReactivo = value; }
    }
}