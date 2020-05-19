using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Models
{
    public class Entidad
    {
        private Cliente cliente;
        private ResponsableLegal responsableLegal;
        private InformacionContacto informacionContacto;
        private string cedulaJuridica;
        private int idEntidad;

        public int IdEntidad { get => idEntidad; set => idEntidad = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public ResponsableLegal ResponsableLegal { get => responsableLegal; set => responsableLegal = value; }
        public InformacionContacto InformacionContacto { get => informacionContacto; set => informacionContacto = value; }
        public string CedulaJuridica { get => cedulaJuridica; set => cedulaJuridica = value; }

        public Entidad()
        {
            this.idEntidad = 0;
            this.Cliente = new Cliente();
            this.ResponsableLegal = new ResponsableLegal();
            this.InformacionContacto = new InformacionContacto();
        }
        public Entidad(Cliente cliente, string cedulaJuridica, ResponsableLegal responsableLegal, InformacionContacto informacionContacto)
        {
            this.idEntidad = 0;
            this.Cliente = cliente;
            this.CedulaJuridica = cedulaJuridica;
            this.ResponsableLegal = responsableLegal;
            this.InformacionContacto = informacionContacto;
        }
    }
}