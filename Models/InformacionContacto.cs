using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Models
{
    public class InformacionContacto
    {
        private int idInformacionContacto;
        private String correo;
        private String telefonoFijo;
        private String telefonoCelular;

        public int IdInformacionContacto { get => idInformacionContacto; set => idInformacionContacto = value; }
        public string Correo { get => correo; set => correo = value; }
        public string TelefonoFijo { get => telefonoFijo; set => telefonoFijo = value; }
        public string TelefonoCelular { get => telefonoCelular; set => telefonoCelular = value; }

        public InformacionContacto( string correo, string telefonoFijo, string telefonoCelular)
        {
            this.idInformacionContacto = 0;
            this.correo = correo;
            this.telefonoFijo = telefonoFijo;
            this.telefonoCelular = telefonoCelular;
        }

        public InformacionContacto()
        {
        }
    }
}