using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Models
{
    public class Persona

    {
        private Cliente cliente;
        private string cedula;
        private InformacionContacto informacionContacto;

        public Cliente Cliente { get => cliente; set => cliente = value; }
        public string Cedula { get => cedula; set => cedula = value; }
        public InformacionContacto InformacionContacto { get => informacionContacto; set => informacionContacto = value; }

        public Persona()
        {
            this.informacionContacto = new InformacionContacto();
        }
        public Persona(Cliente cliente, string cedula, InformacionContacto informacionContacto)
        {
            this.cliente = cliente;
            this.cedula = cedula;
            this.informacionContacto = informacionContacto;
        }
    }
     
}