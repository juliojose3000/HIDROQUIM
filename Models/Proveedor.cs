using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Models
{
    public class Proveedor
    {
     private int idProveedor;
    private string identificacion;
    private String nombre;
    private InformacionContacto informacionContacto;

        public Proveedor( string identificacion, string nombre, InformacionContacto informacionContacto)
        {
           
            this.identificacion = identificacion;
            this.nombre = nombre;
            this.informacionContacto = informacionContacto;
        }

        public Proveedor( )
        {
            this.InformacionContacto = new InformacionContacto();
        }

        public int IdProveedor { get => idProveedor; set => idProveedor = value; }
        public string Identificacion { get => identificacion; set => identificacion = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public InformacionContacto InformacionContacto { get => informacionContacto; set => informacionContacto = value; }
    }
}