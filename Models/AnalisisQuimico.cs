using HIDROQUIM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIDROQUIM.Models
{
    public class AnalisisQuimico
    {
        private int codigo;
        private DateTime fechaMuestreo;
        private DateTime fechaRegistroSistemaAutomatico;
        private DateTime fechaResultado;
        private string ubicacionGeografica;
        private Cliente cliente;

        public AnalisisQuimico(int codigo, DateTime fechaMuestreo, DateTime fechaRegistroSistemaAutomatico, DateTime fechaResultado,
            string ubicacionGeografica, Cliente cliente) {
            this.codigo = codigo;
            this.fechaMuestreo = fechaMuestreo;
            this.FechaRegistroSistemaAutomatico = FechaRegistroSistemaAutomatico;
            this.fechaResultado = fechaResultado;
            this.ubicacionGeografica = ubicacionGeografica;
            this.cliente = cliente;
        }
        public AnalisisQuimico(DateTime fechaMuestreo, DateTime fechaRegistroSistemaAutomatico, DateTime fechaResultado,
       string ubicacionGeografica, Cliente cliente)
        {
          
            this.fechaMuestreo = fechaMuestreo;
            this.FechaRegistroSistemaAutomatico = FechaRegistroSistemaAutomatico;
            this.fechaResultado = fechaResultado;
            this.ubicacionGeografica = ubicacionGeografica;
            this.cliente = cliente;
        }

        public AnalisisQuimico(DateTime fechaMuestreo, DateTime fechaRegistroSistemaAutomatico,
      string ubicacionGeografica, Cliente cliente)
        {

            this.fechaMuestreo = fechaMuestreo;
            this.FechaRegistroSistemaAutomatico = FechaRegistroSistemaAutomatico;
            this.fechaResultado = fechaResultado;
            this.ubicacionGeografica = ubicacionGeografica;
            this.cliente = cliente;
        }

        public AnalisisQuimico()
        {

        }

        public int Codigo { get => codigo; set => codigo = value; }
        public DateTime FechaMuestreo { get => fechaMuestreo; set => fechaMuestreo = value; }
        public DateTime FechaRegistroSistemaAutomatico { get => fechaRegistroSistemaAutomatico; set => fechaRegistroSistemaAutomatico = value; }
        public DateTime FechaResultado { get => fechaResultado; set => fechaResultado = value; }
        public string UbicacionGeografica { get => ubicacionGeografica; set => ubicacionGeografica = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
    }
}