using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Models
{
    public class ResponsableLegal
    {
        private String nombreCompleto;
        private InformacionContacto informacionContacto;
        private int idReponsableLegal;
        private string cedula;

        public ResponsableLegal()
        {
            this.idReponsableLegal = 0;
            this.informacionContacto = new InformacionContacto();
        }

        public ResponsableLegal(string nombreCompleto, InformacionContacto informacionContacto, int idReponsableLegal, string cedula)
        {
            this.nombreCompleto = nombreCompleto;
            this.informacionContacto = informacionContacto;
            this.idReponsableLegal = idReponsableLegal;
            this.cedula = cedula;
        }

        public ResponsableLegal(string nombreCompleto, string cedula, InformacionContacto informacionContacto)
        {
            this.nombreCompleto = nombreCompleto;
            this.informacionContacto = informacionContacto;
            this.cedula = cedula;
        }

        public string NombreCompleto { get => nombreCompleto; set => nombreCompleto = value; }
        public int IdReponsableLegal { get => idReponsableLegal; set => idReponsableLegal = value; }
        public InformacionContacto InformacionContacto { get => informacionContacto; set => informacionContacto = value; }
        public string Cedula { get => cedula; set => cedula = value; }
    }
}