using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Models
{
    public class Producto
    {

        private int idProducto;
        private String nombre;
        private String descripcion;
        private Proveedor proveedor;
        private Categoria categoria;
        private int puntoReorden;
        private float precio;

        public Producto()
        {
            this.proveedor = new Proveedor();
            this.categoria = new Categoria();
        }

        public int IdProducto { get => idProducto; set => idProducto = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public Proveedor Proveedor { get => proveedor; set => proveedor = value; }
        public int PuntoReorden { get => puntoReorden; set => puntoReorden = value; }
        public Categoria Categoria { get => categoria; set => categoria = value; }
        public float Precio { get => precio; set => precio = value; }
    }
}